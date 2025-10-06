using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Author;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthorById;
public sealed class GetAuthorByIdQueryHandler(
    IRepository<Author> _authorRepo,
    IMapper _mapper)
    : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepo.GetByIdAsync(request.Id);
        if (author == null)
        {
            throw new KeyNotFoundException("Author not found with the given Id");
        }

        return _mapper.Map<AuthorDto>(author);
    }
}
