using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Borrows.Commands.ReturnBook;

public sealed class ReturnBookCommandHandler(
    IUnitOfWork _uow,
    ICurrentUserService _currentUserService
    )
    : IRequestHandler<ReturnBookCommand, Unit>
{
    public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var borrow = await _uow.Repository<Borrow>().GetByIdAsync(request.Id);
        if (borrow == null)
            throw new KeyNotFoundException("Borrow record not found");

        if (borrow.UserId != _currentUserService.UserId)
            throw new UnauthorizedAccessException("You cannot return a book you did not borrow.");

        if (borrow.Status != Domain.Enums.BorrowStatus.Borrowed)
            throw new InvalidOperationException("This book has already been returned.");

        var book = await _uow.Repository<Book>().GetByIdAsync(borrow.BookId);
        if (book == null)
            throw new KeyNotFoundException("The book on the borrow's record isn't correct");

        borrow.ReturnBook();
        book.IncreaseCopiesAvailableByOne();
        _uow.Repository<Book>().Update(book);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}