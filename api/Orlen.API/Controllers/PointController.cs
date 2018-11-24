using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.PointService;
using Orlen.Services.PointService.Models;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class PointController : BaseController
    {
        private readonly IPointService pointService;

        public PointController(IPointService pointService)
        {
            this.pointService = pointService;
        }

        [HttpGet]
        public async Task<JContainer> Get()
        {
            return await pointService.GetAll();
        }
        [HttpPost]
        public async Task Post([FromBody] AddPointRequest request)
        {
            await pointService.Add(request);
        }
        [HttpPut]
        public async Task Put([FromBody] UpdatePointRequest request)
        {
            await pointService.Update(request);
        }
        [HttpDelete, Route("{id}")]
        public async Task Delete(int id)
        {
            await pointService.Delete(id);
        }
    }
}
