using Library.Application.Dtos.Auth;
using MediatR;

namespace Library.Application.Features.Auth.UseRefreshToken
{
    public sealed record RefreshTokenCommand(Guid UserId, string Token) : IRequest<AuthResultDto>
    {
    }
}
