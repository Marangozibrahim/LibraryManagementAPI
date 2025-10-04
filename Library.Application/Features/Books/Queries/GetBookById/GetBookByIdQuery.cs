using Library.Application.Dtos.Book;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(Guid Id) : IRequest<BookDto>
{
}
