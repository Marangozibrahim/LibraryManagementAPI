using FluentAssertions;
using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Common.Specifications;
using Library.Application.Dtos.Auth;
using Library.Application.Features.Auth.UseRefreshToken;
using Moq;
using RefreshTokenEntity = Library.Domain.Entities.RefreshToken;

namespace Library.UnitTests.Auth;

public class RefreshToken
{
    [Fact]
    public async Task Handle_ShouldReturnAuthResult_WhenRefreshTokenIsValid()
    {
        // Arrange
        var authTokenServiceMock = new Mock<IAuthTokenService>();
        var refreshTokenRepoMock = new Mock<IRepository<Domain.Entities.RefreshToken>>();

        var handler = new RefreshTokenCommandHandler(authTokenServiceMock.Object, refreshTokenRepoMock.Object);

        var userId = Guid.NewGuid();
        var request = new RefreshTokenCommand (userId, "token");
        var storedToken = new Domain.Entities.RefreshToken { Token = "token", ExpiresAt = DateTime.UtcNow.AddMinutes(5) };
        var authResult = new AuthResultDto { AccessToken = "access", RefreshToken = "refresh" };

        refreshTokenRepoMock.Setup(x => x.SingleOrDefaultAsync(It.IsAny<ISpecification<RefreshTokenEntity>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(storedToken);

        authTokenServiceMock.Setup(x => x.GenerateTokensForUserAsync(userId))
            .ReturnsAsync(authResult);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(authResult);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenRefreshTokenIsInvalid()
    {
        // Arrange
        var authTokenServiceMock = new Mock<IAuthTokenService>();
        var refreshTokenRepoMock = new Mock<IRepository<Domain.Entities.RefreshToken>>();

        var handler = new RefreshTokenCommandHandler(authTokenServiceMock.Object, refreshTokenRepoMock.Object);

        var userId = Guid.NewGuid();
        var request = new RefreshTokenCommand(userId, "token");
        var storedToken = new Domain.Entities.RefreshToken { Token = "token", ExpiresAt = DateTime.UtcNow.AddMinutes(-5) };

        refreshTokenRepoMock.Setup(x => x.SingleOrDefaultAsync(It.IsAny<ISpecification<RefreshTokenEntity>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(storedToken);

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
