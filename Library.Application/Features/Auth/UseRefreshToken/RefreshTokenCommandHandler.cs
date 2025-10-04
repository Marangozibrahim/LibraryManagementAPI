using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Auth;
using Library.Application.Features.Auth.UseRefreshToken.Specs;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Auth.UseRefreshToken
{
    public sealed class RefreshTokenCommandHandler(
        IAuthTokenService _authTokenService,
        IRepository<RefreshToken> _refreshTokenRepo)
        : IRequestHandler<RefreshTokenCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = new RefreshTokenByUserAndValueSpec(request.UserId, request.Token);
            var storedToken = await _refreshTokenRepo.SingleOrDefaultAsync(spec);
            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var authResult = await _authTokenService.GenerateTokensForUserAsync(request.UserId);
            return authResult;
        }
    }
}
