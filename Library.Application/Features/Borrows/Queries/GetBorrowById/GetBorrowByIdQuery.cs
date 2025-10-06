using Library.Application.Dtos.Borrow;
using MediatR;

namespace Library.Application.Features.Borrows.Queries.GetBorrowById;
public sealed record GetBorrowByIdQuery(Guid Id) : IRequest<BorrowDto>
{
}
