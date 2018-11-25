using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Core.Entities;
using Orlen.Services.RouteService.Models;
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
                    StartId = route.Points.ElementAt(i-1).Id,
                    EndId = route.Points.ElementAt(i).Id
                };
                sections.Add(section);
            }


            if (route == null)
                throw new ResourceNotFoundException($"There is no route with id {id}");

            return (new
            {
                route = route,
                sections = sections
            }).AsJContainer();
        }

        private async Task<List<Point>> ValidateRoutes(List<Point> routes)
        {
            for (var i = 1; i < routes.Count; i++)
            {
                var routeExist =  await DataContext.Sections
                    .AnyAsync(s => s.StartId == routes[i - 1].Id && s.EndId == routes[i].Id);

                if (!routeExist)
                {
                    var result = await GetRoute(routes[i - 1].Id, routes[i].Id);
                    routes.Remove(routes[i]);
                    routes.InsertRange(i, result);
                    i = 1;
                }
            }

            return routes;
        }

        public async Task Generate(GenerateRouteRequest request)
        {

            var result = await GetRoute(request.StartPointId, request.EndPointId);

            result = await ValidateRoutes(result);

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
        }

        private async Task<List<Point>> GetRoute(int startPointId, int endPointId)
        {
            var intersections = new List<Node>();

            var groups = await DataContext.Sections
                //TODO: .Where wykluczenie nieczynnych na podstawie dancyh wejscicyjh
                .GroupBy(g => g.StartId)
                .ToListAsync();

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

            var startPoint = await DataContext.Points.FirstOrDefaultAsync(p => p.Id == startPointId);
            if (startPoint == null)
                throw new ResourceNotFoundException($"There is no point with id {startPointId}");

            var graph = new Graph(intersections, intersections.FindIndex(v => v.Id == startPoint.Id));

            graph.InitializeNeighbors();
            graph.TransverNode(graph.Root);

            var result = new List<Point>();

            if (graph.Root.DistanceDict.ContainsKey(endPointId))
            {
                var pointsInRouteId = graph.Root.DistanceDict[endPointId].ToArray();
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
                var route = await GetRoute(request.Points[i - 1], request.Points[i]);
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
    }
}
