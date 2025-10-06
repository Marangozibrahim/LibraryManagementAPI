using MediatR;

namespace Library.Application.Features.Borrows.Commands.DeleteBorrow;
public sealed record DeleteBorrowCommand(Guid Id) : IRequest<Unit>
{
}
