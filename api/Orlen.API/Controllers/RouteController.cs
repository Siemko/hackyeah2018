﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet, Route("{id}")]
        public async Task<JContainer> Get(int id)
        {
            return await routeService.Get(id);
        }

        [HttpPost]
        public async Task<JContainer> Generate([FromBody] GenerateRouteRequest request)
        {
            return await routeService.Generate(request);
        }

        [HttpPost, Route("from-points")]
        public async Task<JContainer> GenerateRouteFromPoints([FromBody] GenerateRouteFromPointsRequest request)
        {
            return await routeService.GenerateRouteFromPoints(request);
        }

        [HttpGet, Route("bus-route/{id}")]
        public async Task<JContainer> GenerateRouteFromPoints(int id)
        {
            return await routeService.GetBusRoute(id);
        }
    }
}
