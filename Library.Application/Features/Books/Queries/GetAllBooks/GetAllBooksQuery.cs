using Library.Application.Common;
using Library.Application.Dtos.Book;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetAllBooks
{
    public sealed record GetAllBooksQuery() : IRequest<IReadOnlyList<BookDto>>
    {
    }
}
