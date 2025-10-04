using MediatR;

namespace Library.Application.Features.Borrows.Commands.ReturnBook;

public sealed record ReturnBookCommand(Guid Id) : IRequest<Unit>
{
}
