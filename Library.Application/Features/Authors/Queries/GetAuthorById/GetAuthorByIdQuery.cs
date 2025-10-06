using Library.Application.Dtos.Author;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthorById;
public sealed record GetAuthorByIdQuery(Guid Id) : IRequest<AuthorDto> 
{
}
