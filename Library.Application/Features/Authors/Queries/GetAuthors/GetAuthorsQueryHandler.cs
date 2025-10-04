using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Author;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler(
    IUnitOfWork _uow,
    IMapper _mapper
    )
    : IRequestHandler<GetAuthorsQuery, IReadOnlyList<AuthorDto>>
{
    public async Task<IReadOnlyList<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _uow.Repository<Author>().ListAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<AuthorDto>>(authors);
    }
}
