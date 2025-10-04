using Library.Application.Common;
using Library.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Abstractions.Auth
{
    public interface IIdentityService
    {
        Task<Result<Guid>> CreateUserAsync(string userName, string email, string password);
        Task<Guid?> GetUserIdByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(Guid userId, string password);
        Task AssignRole(Guid userId, string role);
        Task<IList<string>> GetUserRoles(Guid userId);
        Task<UserProfileDto> GetUserProfile(Guid userId);
    }
}
