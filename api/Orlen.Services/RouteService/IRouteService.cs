using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.RouteService
{
    public interface IRouteService
    {
        Task<JContainer> GetRoute(int fromPointId, int toPointId);
    }
}
