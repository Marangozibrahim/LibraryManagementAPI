using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Common;
using Library.Application.Dtos.Book;
using Library.Application.Features.Books.Queries.GetBooks.Specs;
using Library.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler(
    IRepository<Book> _bookRepo,
    ICacheService _cacheService,
    IMapper _mapper,
    ILogger<GetBooksQueryHandler> _logger
    )
    : IRequestHandler<GetBooksQuery, PaginatedResult<BookDto>>
{
    public async Task<PaginatedResult<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheKey(request);

        var cachedResult = await _cacheService.GetAsync<PaginatedResult<BookDto>>(cacheKey);
        if (cachedResult != null)
        {
            _logger.LogInformation("Books retrieved from cache with key: {CacheKey}", cacheKey);
            return cachedResult;
        }

        _logger.LogInformation("Cache miss for key: {CacheKey}. Fetching from database", cacheKey);

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

        var result = new PaginatedResult<BookDto>(items, totalCount, request.PageIndex, request.PageSize);

        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromSeconds(30));
        _logger.LogInformation("Books cached with key: {CacheKey}", cacheKey);

        return result;
    }

    private static string GenerateCacheKey(GetBooksQuery request)
    {
        return $"books:title={request.Title ?? "null"}:" +
               $"authorId={request.AuthorId?.ToString() ?? "null"}:" +
               $"categoryId={request.CategoryId?.ToString() ?? "null"}:" +
               $"orderBy={request.OrderBy ?? "null"}:" +
               $"orderDir={request.OrderDir ?? "null"}:" +
               $"page={request.PageIndex}:" +
               $"size={request.PageSize}";
    }
}