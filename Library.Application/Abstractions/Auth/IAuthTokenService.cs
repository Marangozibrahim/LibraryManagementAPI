using Library.Application.Dtos.Auth;

namespace Library.Application.Abstractions.Auth
{
    public interface IAuthTokenService
    {
        Task<AuthResultDto> GenerateTokensForUserAsync(Guid userId);    
    }
}
