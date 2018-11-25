using Newtonsoft.Json.Linq;
using Orlen.Services.RouteService.Models;
using System.Threading.Tasks;

namespace Orlen.Services.RouteService
{
    public interface IRouteService
    {
        Task<JContainer> Get(int id);
        Task Generate(GenerateRouteRequest request);
        //Task GenerateRouteFromPoints(GenerateRouteFromPointsRequest request);
    }
}
