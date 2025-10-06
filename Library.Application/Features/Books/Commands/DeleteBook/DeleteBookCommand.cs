using MediatR;

namespace Library.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid Id) : IRequest<Unit>
{
}
