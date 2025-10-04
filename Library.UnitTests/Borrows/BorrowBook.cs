using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Common;
using Library.Application.Features.Borrows.Commands.BorrowBook;
using Library.Application.Features.Borrows.Commands.BorrowBook.Specs;
using Library.Domain.Entities;
using Moq;

namespace Library.UnitTests.Borrows;

public class BorrowBook
{
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly BorrowBookCommandHandler _handler;

    public BorrowBook()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _handler = new BorrowBookCommandHandler(_uowMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenUserIsNotAuthenticated()
    {
        // Arrange
        _currentUserServiceMock.Setup(x => x.GetUserId())
            .Returns(Result<Guid>.Failure("No user"));

        var command = new BorrowBookCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User authentication required", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenBookNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(x => x.GetUserId())
            .Returns(Result<Guid>.Success(userId));

        _uowMock.Setup(x => x.Repository<Book>().GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book)null);

        var command = new BorrowBookCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Book not found", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenNoCopiesAvailable()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(x => x.GetUserId())
            .Returns(Result<Guid>.Success(userId));

        var book = new Book("Test Book", 0, Guid.NewGuid(), Guid.NewGuid());
        _uowMock.Setup(x => x.Repository<Book>().GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        var command = new BorrowBookCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("There is no available copies of that book", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenBorrowAlreadyExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(x => x.GetUserId())
            .Returns(Result<Guid>.Success(userId));

        var book = new Book("Test Book", 1, Guid.NewGuid(), Guid.NewGuid());
        _uowMock.Setup(x => x.Repository<Book>().GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        _uowMock.Setup(x => x.Repository<Borrow>()
            .FirstOrDefaultAsync(It.IsAny<BorrowAlreadyExistsSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Borrow(Guid.NewGuid(), userId, DateTime.UtcNow.AddDays(7)));


        var command = new BorrowBookCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("You already have this book borrowed", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldSucceed_WhenBorrowIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(x => x.GetUserId())
            .Returns(Result<Guid>.Success(userId));

        var book = new Book("Test Book", 1, Guid.NewGuid(), Guid.NewGuid());

        _uowMock.Setup(x => x.Repository<Book>().GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        _uowMock.Setup(x => x.Repository<Borrow>()
            .FirstOrDefaultAsync(It.IsAny<BorrowAlreadyExistsSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Borrow)null);

        _uowMock.Setup(x => x.Repository<Borrow>()
            .AddAsync(It.IsAny<Borrow>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Borrow b, CancellationToken _) => b);

        _uowMock.Setup(x => x.Repository<Book>().Update(It.IsAny<Book>()));

        _uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var command = new BorrowBookCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        _uowMock.Verify(x => x.Repository<Borrow>().AddAsync(It.IsAny<Borrow>(), It.IsAny<CancellationToken>()), Times.Once);
        _uowMock.Verify(x => x.Repository<Book>().Update(It.IsAny<Book>()), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

}
