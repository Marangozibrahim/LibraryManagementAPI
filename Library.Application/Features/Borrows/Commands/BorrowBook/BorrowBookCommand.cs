using Library.Application.Common;
using MediatR;

namespace Library.Application.Features.Borrows.Commands.BorrowBook;

public sealed record BorrowBookCommand(
    Guid BookId
    ) 
    : IRequest<Result<Guid>>
{
}
