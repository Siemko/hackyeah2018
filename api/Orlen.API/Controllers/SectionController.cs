using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orlen.Services.SectionService;
using Orlen.Services.SectionService.Models;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class SectionController : BaseController
    {
        private readonly ISectionService sectionService;

        public SectionController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }

        [HttpGet]
        public async Task<JContainer> Get()
        {
            return await sectionService.GetAll();
        }
        [HttpPost]
        public async Task Post([FromBody] AddSectionRequest request)
        {
            await sectionService.Add(request);
        }
        [HttpDelete, Route("{id}")]
        public async Task Delete (int id)
        {
            await sectionService.Delete(id);
        }
    }
}
