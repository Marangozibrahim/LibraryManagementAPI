using Library.Application.Abstractions.Auth;
using Library.Application.Common;
using Library.Application.Dtos.Auth;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Auth
{
    public class IdentityService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager) : IIdentityService
    {
        public async Task<Result<Guid>> CreateUserAsync(string userName, string email, string password)
        {
            AppUser appUser = new() { Id = Guid.NewGuid(), UserName = userName, Email = email };
            IdentityResult result = await _userManager.CreateAsync(appUser, password);

            return result.Succeeded
                ? Result<Guid>.Success(appUser.Id)
                : Result<Guid>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task AssignRole(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new InvalidOperationException("User not found");

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new AppRole() { Name = role});

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<Guid?> GetUserIdByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user?.Id;
        }

        public async Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user is not null && await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetUserRoles(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new InvalidOperationException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<UserProfileDto> GetUserProfile(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new InvalidOperationException("User not found");

            UserProfileDto userProfileDto = new()
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty
            };

            return userProfileDto;
        }
    }
}
