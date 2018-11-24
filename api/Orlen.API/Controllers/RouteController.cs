using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.RouteService;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class RouteController : BaseController
    {
        private readonly IRouteService routeService;

        public RouteController(IRouteService routeService)
        {
            this.routeService = routeService;
        }

        [HttpGet, Route("{fromPointId}/{toPointId}")]
        public async Task<JContainer> Get(int fromPointId, int toPointId)
        {
            return await routeService.GetRoute(fromPointId, toPointId);
        }
    }
}
