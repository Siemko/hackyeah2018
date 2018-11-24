using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orlen.API.Authorization;
using Orlen.Common.Exceptions;
using Orlen.Services.UserService;
using Orlen.Services.UserService.Models;
using System.Threading.Tasks;

namespace Orlen.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<TokenModel> Auth([FromBody] LoginUserModel model)
        {
            if (!await userService.ValidateUser(model))
                throw new InvalidParameterException("Błędny login lub hasło");

            var user = await userService.GetUser(model.Email);
            return TokenHelper.GenerateTokenForUser(user);
        }
    }
}
