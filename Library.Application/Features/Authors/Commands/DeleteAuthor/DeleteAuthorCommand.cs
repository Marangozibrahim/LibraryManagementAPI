using MediatR;

namespace Library.Application.Features.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid Id)
    : IRequest<Unit>;