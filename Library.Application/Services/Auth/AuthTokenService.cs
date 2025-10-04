using Library.Application.Abstractions.Auth;
using Library.Application.Dtos.Auth;

namespace Library.Application.Services.Auth
{
    public sealed class AuthTokenService(
        IJwtTokenGenerator jwt,
        IIdentityService identityService)
    : IAuthTokenService
    {
        public async Task<AuthResultDto> GenerateTokensForUserAsync(Guid userId)
        {
            var profile = await identityService.GetUserProfile(userId);
            var roles = await identityService.GetUserRoles(userId);

            var accessToken = jwt.GenerateAccessToken(profile.Id, profile.UserName, profile.Email, roles);
            var refreshToken = await jwt.GenerateRefreshTokenAsync(userId);

            return new AuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
