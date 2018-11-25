using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Orlen.Services.RouteService.Models;
using System.Threading.Tasks;
using Orlen.Core.Entities;

namespace Orlen.Services.RouteService
{
    public interface IRouteService
    {
        Task<JContainer> Get(int id);
        Task<JContainer> Generate(GenerateRouteRequest request);
        Task<JContainer> GenerateRouteFromPoints(GenerateRouteFromPointsRequest request);
        Task<JContainer> GetBusRoute(int busId);
    }
}
