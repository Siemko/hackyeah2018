using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Core;
using Orlen.Services.UserService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orlen.Services.UserService
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(DataContext db, ILogger<UserService> logger) : base(db, logger)
        {
        }

        public async Task<UserModel> GetUser(string email)
        {
            var user = await DataContext.Users.Select(u => new UserModel()
            {
                Email = u.Email,
                Id = u.Id,
                Role = u.Role.Name
            }).FirstOrDefaultAsync();

            if (user == null)
                throw new ResourceNotFoundException($"There is no user with email {email}");

            return user;
        }

        public async Task<bool> ValidateUser(LoginUserModel model)
        {
            model.Password = model.Password.HashSha256();
            return await DataContext.Users.AnyAsync(u => u.Email == model.Email && u.Password == model.Password);
        }
    }
}
