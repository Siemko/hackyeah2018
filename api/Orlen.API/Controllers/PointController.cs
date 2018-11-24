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
        [HttpPost, Route("issue")]
        public async Task PostIssue([FromBody] AddPointIssueRequest request)
        {
            await pointService.AddIssue(request);
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
        [HttpDelete, Route("issue/{id}")]
        public async Task PostIssue(int issueId)
        {
            await pointService.DeleteIssue(issueId);
        }
    }
}
