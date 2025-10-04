using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Book;
using Library.Application.Features.Books.Queries.GetBookById.Specs;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryHandler(
    IRepository<Book> _bookRepo,
    IMapper _mapper)
    : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepo.FirstOrDefaultAsync(new GetBookByIdWithDetailsSpec(request.Id));
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found with the given Id");
        }

        return _mapper.Map<BookDto>(book);
    }
}