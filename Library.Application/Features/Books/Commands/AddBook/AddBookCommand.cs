using MediatR;

namespace Library.Application.Features.Books.Commands.AddBook;

public sealed record AddBookCommand(string Title, int TotalCopies, Guid AuthorId, Guid CategoryId) : IRequest<Guid>
{
}
