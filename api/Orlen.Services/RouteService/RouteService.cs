using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Const;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.RouteService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.RouteService
{
    public class RouteService : BaseService<RouteService>, IRouteService
    {
        public RouteService(DataContext db, ILogger<RouteService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> Get(int id)
        {
            var route = await DataContext.Routes
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    r.Id,
                    r.Length,
                    r.Width,
                    r.Height,
                    r.Weight,
                    Points = r.RoutePoints.OrderBy(o => o.Order).Select(rp => rp.Point)
                }).FirstOrDefaultAsync();

            var sections = new List<Section>();
            for (var i = 1; i < route.Points.Count(); i++)
            {
                var section = new Section
                {
                    StartId = route.Points.ElementAt(i - 1).Id,
                    EndId = route.Points.ElementAt(i).Id
                };
                sections.Add(section);
            }


            if (route == null)
                throw new ResourceNotFoundException($"There is no route with id {id}");

            return (new
            {
                route,
                sections
            }).AsJContainer();
        }



        public async Task<JContainer> Generate(GenerateRouteRequest request)
        {
            request.StartPointId = await DataContext.Points
                                .Where(p => p.Name.Equals(request.StartPointName, StringComparison.CurrentCultureIgnoreCase))
                                .Select(p => p.Id)
                                .FirstOrDefaultAsync();

            request.EndPointId = await DataContext.Points
                                 .Where(p => p.Name.Equals(request.EndPointName, StringComparison.CurrentCultureIgnoreCase))
                                 .Select(p => p.Id)
                                 .FirstOrDefaultAsync();

            var result = await GetRoute(request);

            result.Insert(0, new Point
            {
                Id = request.StartPointId
            });

            result.Add(new Point
            {
                Id = request.EndPointId
            });

            if (result.Count() <= 1)
                throw new InvalidParameterException("Ilość wygenerowanych punktów mniejsza lub równa 1. Trasa nie wygenerowna");

            var route = new Route()
            {
                Weight = request.Weight,
                Height = request.Height,
                Length = request.Length,
                Width = request.Width,
            };
            DataContext.Routes.Add(route);

            await DataContext.SaveChangesAsync();

            var routePoints = new List<RoutePoints>();
            var counter = 0;
            foreach (var r in result)
            {
                routePoints.Add(new RoutePoints
                {

                    RouteId = route.Id,
                    PointId = r.Id,
                    Order = counter++
                });
            }

            DataContext.RoutePoints.AddRange(routePoints);
            await DataContext.SaveChangesAsync();
            return new
            {
                route.Id
            }.AsJContainer();
        }

        private async Task<List<Point>> GetRoute(GenerateRouteRequest request)
        {
            var intersections = new List<Node>();

            var rr = await DataContext.Sections.Where(s => s.Issues.Any(i => i.IssueType.Name == IssueTypeName.Disabled)).ToListAsync();

            var sections = (await DataContext.Sections
                .Where(s => !s.Issues.Any(i => i.IssueType.Name == IssueTypeName.MaxHeight && i.Value <= request.Height))
                .Where(s => !s.Issues.Any(i => i.IssueType.Name == IssueTypeName.MaxLength && i.Value <= request.Length))
                .Where(s => !s.Issues.Any(i => i.IssueType.Name == IssueTypeName.MaxWeight && i.Value <= request.Weight))
                .Where(s => !s.Issues.Any(i => i.IssueType.Name == IssueTypeName.MaxWidth && i.Value <= request.Width))
                .Where(s => !s.Issues.Any(i => i.IssueType.Name == IssueTypeName.Disabled))
                .ToListAsync());

            var groups = sections
                            .GroupBy(g => g.StartId)
                            .ToList();

            foreach (var group in groups)
            {
                var node = new Node
                {
                    Id = group.Key,
                    DistanceDict = new Dictionary<int, List<int>>()
                };

                foreach (var g in group)
                {
                    node.DistanceDict.Add(g.EndId, new List<int>(g.EndId));
                }

                intersections.Add(node);
            }

            var startPoint = await DataContext.Points.FirstOrDefaultAsync(p => p.Id == request.StartPointId);
            if (startPoint == null)
                throw new ResourceNotFoundException($"There is no point with id {request.StartPointId}");

            var graph = new Graph(intersections, intersections.FindIndex(v => v.Id == startPoint.Id));

            graph.InitializeNeighbors();
            graph.TransverNode(graph.Root);

            var result = new List<Point>();

            if (graph.Root.DistanceDict.ContainsKey(request.EndPointId))
            {
                var pointsInRouteId = graph.Root.DistanceDict[request.EndPointId].ToArray();
                var pointsInRoute = await DataContext.Points
                    .Where(p => pointsInRouteId.Contains(p.Id)).ToListAsync();

                foreach (var pointId in pointsInRouteId)
                {
                    var point = pointsInRoute.First(pir => pir.Id == pointId);
                    result.Add(point);
                }
            }

            return result;
        }

        public async Task<JContainer> GenerateRouteFromPoints(GenerateRouteFromPointsRequest request)
        {
            var result = new List<Point>();
            for (var i = 1; i < request.Points.Count; i++)
            {
                var generateRouteRequest = new GenerateRouteRequest() { StartPointId = request.Points[i - 1], EndPointId = request.Points[i] };

                var route = await GetRoute(generateRouteRequest);
                result.AddRange(route);
            }

            result.Insert(0, new Point
            {
                Id = request.Points[0]
            });

            result.Add(new Point
            {
                Id = request.Points[request.Points.Count - 1]
            });

            return result.AsJContainer();
        }

        public async Task<JContainer> GetBusRoute(int busId)
        {
            var route = await GetRoute(new GenerateRouteRequest
            {
                StartPointId = 1,
                EndPointId = 42,
            });

            var bus = await DataContext.Buses
                .Include(v => v.Stops)
                .FirstOrDefaultAsync(v => v.Id == busId);

            if (bus == null)
                return route.AsJContainer();

            var points = bus.Stops.Select(v => v.PointId).ToList();

            if (points.Count() < 2)
                return route.AsJContainer();

            route = await GetRoute(new GenerateRouteRequest
            {
                StartPointId = points[0],
                EndPointId = points[points.Count() - 1],
            });

            return route.AsJContainer();
        }
    }
}
