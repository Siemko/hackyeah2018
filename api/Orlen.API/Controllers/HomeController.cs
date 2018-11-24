using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Orlen.API.Controllers
{
    [Route("")]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : BaseController
    {

        [HttpGet, Route("")]
        public ContentResult Hello() => HelloApiMessage();
    }
}
