using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.RouteService;
using Orlen.Services.RouteService.Models;
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

        [HttpGet]
        public async Task<JContainer> Get([FromQuery] GetRouteRequest request)
        {
            return await routeService.GetRoute(request);
        }
    }
}
