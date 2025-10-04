using Library.Application.Abstractions.Auth;
using Library.Application.Abstractions.Persistence;
using Library.Application.Common;
using Library.Application.Features.Borrows.Commands.BorrowBook.Specs;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Borrows.Commands.BorrowBook;

public sealed class BorrowBookCommandHandler(
    IUnitOfWork _uow,
    ICurrentUserService _currentUserService
    ) 
    : IRequestHandler<BorrowBookCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var userIdResult = _currentUserService.GetUserId();
        if (!userIdResult.IsSuccess)
            return Result<Guid>.Failure("User authentication required");

        var userId = userIdResult.Value;

        var book = await _uow.Repository<Book>().GetByIdAsync(request.BookId, cancellationToken);
        if (book == null)
            return Result<Guid>.Failure("Book not found");

        if (book.CopiesAvailable <= 0)
            return Result<Guid>.Failure("There is no available copies of that book");

        var borrowExistsSpec = new BorrowAlreadyExistsSpec(userId, request.BookId);
        var existingBorrow = await _uow.Repository<Borrow>().FirstOrDefaultAsync(borrowExistsSpec);

        if (existingBorrow != null)
        {
            await _uow.RollbackTransactionAsync(cancellationToken);
            return Result<Guid>.Failure("You already have this book borrowed");
        }

        var borrow = new Borrow(
            request.BookId, 
            userId, 
            DateTime.UtcNow.AddDays(14)
            );

        await _uow.Repository<Borrow>().AddAsync(borrow, cancellationToken);

        book.DecreaseCopiesAvailableByOne();
        _uow.Repository<Book>().Update(book);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(borrow.Id);
    }
}
