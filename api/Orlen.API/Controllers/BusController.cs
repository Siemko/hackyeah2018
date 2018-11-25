using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.BusService;
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
    }
}
