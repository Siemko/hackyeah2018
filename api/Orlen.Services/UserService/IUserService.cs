using System.Threading.Tasks;
using Orlen.Services.UserService.Models;

namespace Orlen.Services.UserService
{
    public interface IUserService
    {
        Task<bool> ValidateUser(LoginUserModel model);
        Task<UserModel> GetUser(string email);
    }
}
