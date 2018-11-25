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
                    Points = r.RoutePoints.Select(rp => rp.Point).Select(p => new
                    {
                        p.Latitude,
                        p.Longitude
                    })
                }).FirstOrDefaultAsync();

            if (route == null)
                throw new ResourceNotFoundException($"There is no route with id {id}");

            return route.AsJContainer();
        }

        public async Task Generate(GenerateRouteRequest request)
        {

            var result = await GetRoute(request.StartPointId, request.EndPointId);

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

            var routePoints = result.Select(r => new RoutePoints()
            {
                RouteId = route.Id,
                PointId = r.Id,
                Order = r.Order
            }).ToList();

            DataContext.RoutePoints.AddRange(routePoints);
            await DataContext.SaveChangesAsync();
        }

        private async Task<List<GeneratedPointModel>> GetRoute(int startPointId, int endPointId)
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

            var result = new List<GeneratedPointModel>();

            if (graph.Root.DistanceDict.ContainsKey(endPointId))
            {
                var pointsInRouteId = graph.Root.DistanceDict[endPointId].ToArray();
                var pointsInRoute = await DataContext.Points
                    .Where(p => pointsInRouteId.Contains(p.Id)).ToListAsync();

                var counter = 0;
                foreach (var pointId in pointsInRouteId)
                {
                    counter++;
                    var point = pointsInRoute.First(pir => pir.Id == pointId);
                    result.Add(new GeneratedPointModel
                    {
                        Id = point.Id,
                        Latitude = point.Latitude,
                        Longitude = point.Longitude,
                        Order = counter
                    });
                }
            }

            return result;
        }

        public async Task<JContainer> GenerateRouteFromPoints(GenerateRouteFromPointsRequest request)
        {
            var result = new List<GeneratedPointModel>();
            for (var i = 1; i < request.Points.Count; i++)
            {
                var route = await GetRoute(request.Points[i - 1], request.Points[i]);
                result.AddRange(route);
            }

            return result.AsJContainer();
        }
    }
}
