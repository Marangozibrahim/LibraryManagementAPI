using Library.Application.Abstractions.Auth;
using MediatR;

namespace Library.Application.Features.Auth.RegisterUser
{
    public sealed class RegisterUserCommandHandler(IIdentityService _identityService) : IRequestHandler<RegisterUserCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, request.Email, request.Password);
            if (!result.IsSuccess) throw new FluentValidation.ValidationException(result.Error ?? "Registration failed");

            var userId = result.Value;
            await _identityService.AssignRole(userId, request.asAdmin ? "Admin" : "User");
            return userId;
        }
    }
}
