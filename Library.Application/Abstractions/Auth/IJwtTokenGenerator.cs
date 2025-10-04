namespace Library.Application.Abstractions.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(Guid userId, string userName, string email, IEnumerable<string> roles);
        Task<string> GenerateRefreshTokenAsync(Guid userId);
    }
}
