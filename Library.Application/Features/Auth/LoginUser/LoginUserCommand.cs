using Library.Application.Dtos.Auth;
using MediatR;

namespace Library.Application.Features.Auth.LoginUser
{
    public sealed record LoginUserCommand(string UserName, string  Password) : IRequest<AuthResultDto>
    {
    }
}
