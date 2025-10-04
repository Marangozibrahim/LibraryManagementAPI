using MediatR;

namespace Library.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    Guid Id, 
    string? Title, 
    int? TotalCopies,
    int? CopiesAvailable,
    Guid? AuthorId, 
    Guid? CategoryId) 
    : IRequest<Unit>
{
}
