using Library.Application.Dtos.Author;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthors;

public sealed record GetAuthorsQuery : IRequest<IReadOnlyList<AuthorDto>>
{
}
