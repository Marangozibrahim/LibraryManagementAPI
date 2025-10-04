using MediatR;

namespace Library.Application.Features.Auth.RegisterUser
{
    public sealed record RegisterUserCommand(string UserName, string Email, string Password, bool asAdmin) : IRequest<Guid>
    {
    }
}
