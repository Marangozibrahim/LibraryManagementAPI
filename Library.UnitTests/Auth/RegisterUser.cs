using FluentAssertions;
using Library.Application.Abstractions.Auth;
using Library.Application.Common;
using Library.Application.Features.Auth.RegisterUser;
using Moq;

namespace Library.UnitTests.Auth;

public class RegisterUser
{
    [Fact]
    public async Task Handle_ShouldRegisterUserAndAssignRole()
    {
        // Arrange
        var identityServiceMock = new Mock<IIdentityService>();
        var handler = new RegisterUserCommandHandler(identityServiceMock.Object);

        var request = new RegisterUserCommand("newuser", "user@example.com", "Password123", false);

        var userId = Guid.NewGuid();
        identityServiceMock.Setup(x => x.CreateUserAsync(request.UserName, request.Email, request.Password))
            .ReturnsAsync(Result<Guid>.Success(userId));
        identityServiceMock.Setup(x => x.AssignRole(userId, "User"))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(userId);
        identityServiceMock.Verify(x => x.AssignRole(userId, "User"), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenRegistrationFails()
    {
        // Arrange
        var identityServiceMock = new Mock<IIdentityService>();
        var handler = new RegisterUserCommandHandler(identityServiceMock.Object);

        var request = new RegisterUserCommand("newuser", "user@example.com", "Password123", false);

        identityServiceMock.Setup(x => x.CreateUserAsync(request.UserName, request.Email, request.Password))
            .ReturnsAsync(Result<Guid>.Failure("Error"));

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
