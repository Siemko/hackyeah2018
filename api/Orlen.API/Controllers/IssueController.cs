using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.IssueService;
using Orlen.Services.IssueService.Models;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class IssueController : BaseController
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpGet]
        public async Task<JContainer> GetAll()
        {
            return await issueService.GetAll();
        }
        [HttpPost]
        public async Task Post([FromBody] AddIssueRequest request)
        {
            await issueService.Add(request);
        }

        [HttpPut]
        public async Task Put([FromBody] UpdateIssueRequest request)
        {
            await issueService.Update(request);
        }

        [HttpDelete, Route("{id}")]
        public async Task Delete(int id)
        {
            await issueService.Delete(id);
        }
    }
}
