using Library.Application.Abstractions.Auth;
using Library.Application.Dtos.Auth;
using MediatR;

namespace Library.Application.Features.Auth.LoginUser
{
    public sealed class LoginUserCommandHandler(
        IIdentityService _identityService,
        IAuthTokenService _authTokenService) 
        : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetUserIdByUserNameAsync(request.UserName)
                     ?? throw new UnauthorizedAccessException("Invalid credentials");

            var ok = await _identityService.CheckPasswordAsync(userId, request.Password);
            if(!ok) throw new UnauthorizedAccessException("Invalid credentials");

            return await _authTokenService.GenerateTokensForUserAsync(userId);
        }
    }
}
