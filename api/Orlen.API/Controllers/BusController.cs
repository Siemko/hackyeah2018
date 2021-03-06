﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.BusService;
using Orlen.Services.BusService.Models;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class BusController : BaseController
    {
        private readonly IBusService busService;

        public BusController(IBusService busService)
        {
            this.busService = busService;
        }

        [HttpGet]
        public async Task<JContainer> Get()
        {
            return await busService.Get();
        }

        [HttpGet, Route("stops/{id}")]
        public async Task<JContainer> GetBusStopes(int id)
        {
            return await busService.GetBusStopes(id);
        }

        [HttpPost]
        public async Task GetBusStopes([FromBody] AddBusRequest request)
        {
            await busService.Add(request);
        }
    }
}
