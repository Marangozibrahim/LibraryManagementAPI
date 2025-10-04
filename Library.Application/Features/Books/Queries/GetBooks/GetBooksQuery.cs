using Library.Application.Common;
using Library.Application.Dtos.Book;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed record GetBooksQuery(
    int PageIndex = 1,
    int PageSize = 10,
    string? Title = null,
    Guid? AuthorId = null,
    Guid? CategoryId = null,
    string? OrderBy = null,
    string? OrderDir = null
    ) 
    : IRequest<PaginatedResult<BookDto>>;
