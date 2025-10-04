using FluentAssertions;
using Library.Application.Abstractions.Auth;
using Library.Application.Dtos.Auth;
using Library.Application.Features.Auth.LoginUser;
using Moq;

namespace Library.UnitTests.Auth;

public class LoginUser
{
    [Fact]
    public async Task Handle_ShouldReturnAuthResult_WhenCredentialsAreValid()
    {
        // Arrange
        var identityServiceMock = new Mock<IIdentityService>();
        var authTokenServiceMock = new Mock<IAuthTokenService>();
        var handler = new LoginUserCommandHandler(identityServiceMock.Object, authTokenServiceMock.Object);

        var userId = Guid.NewGuid();
        var request = new LoginUserCommand("test", "password");
        var authResult = new AuthResultDto { AccessToken = "token", RefreshToken = "refresh" };

        identityServiceMock.Setup(x => x.GetUserIdByUserNameAsync(request.UserName))
            .ReturnsAsync(userId);
        identityServiceMock.Setup(x => x.CheckPasswordAsync(userId, request.Password))
            .ReturnsAsync(true);
        authTokenServiceMock.Setup(x => x.GenerateTokensForUserAsync(userId))
            .ReturnsAsync(authResult);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(authResult);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenInvalidCredentials()
    {
        // Arrange
        var identityServiceMock = new Mock<IIdentityService>();
        var authTokenServiceMock = new Mock<IAuthTokenService>();
        var handler = new LoginUserCommandHandler(identityServiceMock.Object, authTokenServiceMock.Object);

        var request = new LoginUserCommand("test", "password");
        identityServiceMock.Setup(x => x.GetUserIdByUserNameAsync(request.UserName))
            .ReturnsAsync((Guid?)null);

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
