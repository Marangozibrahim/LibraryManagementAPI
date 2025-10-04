using Library.Application.Dtos.Borrow;
using MediatR;

namespace Library.Application.Features.Borrows.Queries.GetBorrowsByUser;

public sealed record GetBorrowsByUserQuery() : IRequest<IReadOnlyList<BorrowDto>>
{
}
