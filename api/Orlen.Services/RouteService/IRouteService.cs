using Newtonsoft.Json.Linq;
using Orlen.Services.RouteService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.RouteService
{
    public interface IRouteService
    {
        Task<JContainer> GetRoute(GetRouteRequest request);
    }
}
