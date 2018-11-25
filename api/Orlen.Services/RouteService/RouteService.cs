using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Services.RouteService.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orlen.Services.RouteService.Models;

namespace Orlen.Services.RouteService
{
    public class RouteService : BaseService<RouteService>, IRouteService
    {
        public RouteService(DataContext db, ILogger<RouteService> logger) : base(db, logger)
        {
        }

        public async Task<JContainer> GetRoute(GetRouteRequest request)
        {
            var intersections = new List<Node>();

            var groups = await DataContext.Sections
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

            var gate1 = await DataContext.Points.FirstAsync(p => p.Name == "G1");
            var graph = new Graph(intersections, intersections.FindIndex(v => v.Id == gate1.Id));

            graph.InitializeNeighbors();
            graph.TransverNode(graph.Root);

            var result = new List<object>();

            if (graph.Root.DistanceDict.ContainsKey(toPointId))
            {
                var pointsInRouteId = graph.Root.DistanceDict[toPointId].ToArray();
                var pointsInRoute = await DataContext.Points
                    .Where(p => pointsInRouteId.Contains(p.Id)).ToListAsync();

                foreach (var pointId in pointsInRouteId)
                {
                    var point = pointsInRoute.First(pir => pir.Id == pointId);
                    result.Add(new
                    {
                        id = point.Id,
                        lat = point.Lat,
                        lon = point.Lon
                    });
                }
            }

            return await Task.FromResult(result.AsJContainer());
        }
    }
}
