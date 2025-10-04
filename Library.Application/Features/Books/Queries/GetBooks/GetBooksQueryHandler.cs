using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Common;
using Library.Application.Dtos.Book;
using Library.Application.Features.Books.Queries.GetBooks.Specs;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler(
    IRepository<Book> _bookRepo,
    IMapper _mapper
    )
    : IRequestHandler<GetBooksQuery, PaginatedResult<BookDto>>
{
    public async Task<PaginatedResult<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetBooksQueryFilterSpec(
            request.Title,
            request.AuthorId,
            request.CategoryId,
            request.OrderBy,
            request.OrderDir,
            (request.PageIndex - 1) * request.PageSize,
            request.PageSize
            );

        var countSpec = new GetBooksQueryFilterSpec(
            request.Title,
            request.AuthorId,
            request.CategoryId
            );

        var books = await _bookRepo.ListAsync(spec, cancellationToken);
        var totalCount = await _bookRepo.CountAsync(countSpec, cancellationToken);

        var items = _mapper.Map<IReadOnlyList<BookDto>>(books);

        return new PaginatedResult<BookDto>(items, totalCount, request.PageIndex, request.PageSize);
    }
}