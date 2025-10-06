using Library.Application.Common;

namespace Library.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
    IEnumerable<string> Roles { get; }
    bool IsAuthenticated { get; }

    Result<Guid> GetUserId();
    bool IsInRole(string role);
    bool HasAnyRole(params string[] roles);
    bool HasAllRoles(params string[] roles);
}
