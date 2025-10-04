using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Book;
using Library.Application.Features.Books.Queries.GetAllBooks.Specs;
using Library.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Books.Queries.GetAllBooks
{
    public sealed class GetAllBooksQueryHandler(
        IRepository<Book> _bookRepository,
        ICacheService _cacheService,
        IMapper _mapper,
        ILogger<GetAllBooksQueryHandler> _logger) 
        : IRequestHandler<GetAllBooksQuery, IReadOnlyList<BookDto>>
    {
        public async Task<IReadOnlyList<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "books:all";
            try
            {
                _logger.LogInformation("Fetching all books from cache with key {CacheKey}", cacheKey);

                var cachedBooks = await _cacheService.GetAsync<IReadOnlyList<BookDto>>(cacheKey);
                if (cachedBooks is not null)
                {
                    _logger.LogInformation("Cache hit for {CacheKey}, returning cached books", cacheKey);
                    return cachedBooks;
                }

                _logger.LogInformation("Cache miss for {CacheKey}, fetching from database", cacheKey);

                var spec = new GetAllBooksSpec();
                var books = await _bookRepository.ListAsync(spec, cancellationToken);
                var mappedBooks = _mapper.Map<IReadOnlyList<BookDto>>(books);
                await _cacheService.SetAsync(cacheKey, mappedBooks, TimeSpan.FromMinutes(10));

                _logger.LogInformation("Books fetched from database and cached with key {CacheKey}", cacheKey);

                return mappedBooks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all books");
                throw;
            }
        }
    }
}