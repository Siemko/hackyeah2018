using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.IssueTypeService;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class IssueTypeController : BaseController
    {
        private readonly IIssueTypeService issueTypeService;

        public IssueTypeController(IIssueTypeService issueTypeService)
        {
            this.issueTypeService = issueTypeService;
        }

        [HttpGet]
        public async Task<JContainer> GetAll()
        {
            return await issueTypeService.Get();
        }
    }
}
