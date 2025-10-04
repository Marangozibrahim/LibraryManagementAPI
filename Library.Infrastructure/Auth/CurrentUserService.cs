using Library.Application.Abstractions.Auth;
using Library.Application.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Library.Infrastructure.Auth;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId => GetClaimValue(ClaimTypes.NameIdentifier) is string userIdString
        && Guid.TryParse(userIdString, out var userId)
        ? userId
        : null;

    public string? UserName => GetClaimValue(ClaimTypes.Name);

    public string? Email => GetClaimValue(ClaimTypes.Email);

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public Result<Guid> GetUserId()
    {
        if (!IsAuthenticated)
            return Result<Guid>.Failure("User is not authenticated");

        if (UserId.HasValue)
            return Result<Guid>.Success(UserId.Value);

        return Result<Guid>.Failure("User ID not found in claims");
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(claimType)?.Value;
    }
}