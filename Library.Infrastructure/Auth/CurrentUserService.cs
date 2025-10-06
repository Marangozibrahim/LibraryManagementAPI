using Library.Application.Abstractions.Auth;
using Library.Application.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Library.Infrastructure.Auth;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? UserId => GetClaimValue(ClaimTypes.NameIdentifier) is string userIdString
        && Guid.TryParse(userIdString, out var userId)
        ? userId
        : null;

    public string? UserName => GetClaimValue(ClaimTypes.Name);

    public string? Email => GetClaimValue(ClaimTypes.Email);

    public IEnumerable<string> Roles => GetClaimValues(ClaimTypes.Role);

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public Result<Guid> GetUserId()
    {
        if (!IsAuthenticated)
            return Result<Guid>.Failure("User is not authenticated");

        if (UserId.HasValue)
            return Result<Guid>.Success(UserId.Value);

        return Result<Guid>.Failure("User ID not found in claims");
    }

    public bool IsInRole(string role)
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
    }

    public bool HasAnyRole(params string[] roles)
    {
        return roles.Any(role => IsInRole(role));
    }

    public bool HasAllRoles(params string[] roles)
    {
        return roles.All(role => IsInRole(role));
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(claimType)?.Value;
    }

    private IEnumerable<string> GetClaimValues(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?
            .FindAll(claimType)
            .Select(c => c.Value)
            ?? Enumerable.Empty<string>();
    }
}