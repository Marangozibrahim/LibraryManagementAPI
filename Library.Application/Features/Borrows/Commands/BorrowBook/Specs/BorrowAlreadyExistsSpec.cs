using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Borrows.Commands.BorrowBook.Specs;

public sealed class BorrowAlreadyExistsSpec : BaseSpecification<Borrow>
{
    public BorrowAlreadyExistsSpec(Guid userId, Guid bookId)
    {
        Criteria = bw => bw.UserId == userId &&
                           bw.BookId == bookId &&
                           bw.Status == Domain.Enums.BorrowStatus.Borrowed;

        AsNoTracking = true;
    }
}
