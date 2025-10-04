using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Application.Features.Borrows.Commands.ReturnBook;
using Moq;
using MediatR;

namespace Library.UnitTests.Borrows;

public class ReturnBook
{
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly ReturnBookCommandHandler _handler;

    public ReturnBook()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _handler = new ReturnBookCommandHandler(_uowMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task ReturnBook_ShouldThrow_KeyNotFound_WhenBorrowDoesNotExist()
    {
        var command = new ReturnBookCommand(Guid.NewGuid());
        _uowMock.Setup(x => x.Repository<Borrow>()
            .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Borrow)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ReturnBook_ShouldThrow_Unauthorized_WhenBorrowBelongsToAnotherUser()
    {
        var borrow = new Borrow(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(7));
        _uowMock.Setup(x => x.Repository<Borrow>()
            .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(borrow);

        _currentUserServiceMock.Setup(x => x.UserId)
            .Returns(Guid.NewGuid());

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(new ReturnBookCommand(Guid.NewGuid()), CancellationToken.None));
    }

    [Fact]
    public async Task ReturnBook_ShouldThrow_InvalidOperation_WhenAlreadyReturned()
    {
        var userId = Guid.NewGuid();
        var borrow = new Borrow(Guid.NewGuid(), userId, DateTime.UtcNow.AddDays(7));
        borrow.ReturnBook();

        _uowMock.Setup(x => x.Repository<Borrow>()
            .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(borrow);

        _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(new ReturnBookCommand(Guid.NewGuid()), CancellationToken.None));
    }

    [Fact]
    public async Task ReturnBook_ShouldSucceed_WhenValid()
    {
        var userId = Guid.NewGuid();
        var borrow = new Borrow(Guid.NewGuid(), userId, DateTime.UtcNow.AddDays(7));
        var book = new Book("Title", 1, Guid.NewGuid(), Guid.NewGuid());

        _uowMock.Setup(x => x.Repository<Borrow>()
            .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(borrow);

        _uowMock.Setup(x => x.Repository<Book>()
            .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);

        _uowMock.Setup(x => x.Repository<Book>().Update(It.IsAny<Book>()));
        _uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var command = new ReturnBookCommand(Guid.NewGuid());
        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Unit.Value, result);
        _uowMock.Verify(x => x.Repository<Book>().Update(It.IsAny<Book>()), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(BorrowStatus.Returned, borrow.Status);
        Assert.Equal(2, book.CopiesAvailable);
    }
}
