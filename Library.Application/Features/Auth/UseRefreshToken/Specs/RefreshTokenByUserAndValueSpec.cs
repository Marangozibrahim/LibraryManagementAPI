using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Auth.UseRefreshToken.Specs
{
    public sealed class RefreshTokenByUserAndValueSpec : BaseSpecification<RefreshToken>
    {
        public RefreshTokenByUserAndValueSpec(Guid userId, string token) { 
            Criteria = t => t.UserId == userId && t.Token == token;
        }
    }
}
