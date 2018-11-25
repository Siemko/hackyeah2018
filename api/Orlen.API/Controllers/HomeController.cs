using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    [Route("")]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : BaseController
    {

        [HttpGet, Route("")]
        public async Task<ContentResult> Hello()
        {
            return await Task.FromResult(HelloApiMessage());
        }
    }
}
