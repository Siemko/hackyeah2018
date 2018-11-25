using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orlen.API.Attributes;
using System.Text;

namespace Orlen.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ExceptionHandler]
    [AllowAnonymous]
    public class BaseController : Controller
    {
        protected ContentResult HelloApiMessage()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"<h1 style='font-family: Verdana'>Orlen Transportation Managment API</h1>");
            return new ContentResult { Content = stringBuilder.ToString(), ContentType = "text/html", StatusCode = 200 };
        }
    }
}
