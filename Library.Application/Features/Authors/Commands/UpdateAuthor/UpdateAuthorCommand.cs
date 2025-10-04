using MediatR;

namespace Library.Application.Features.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(Guid Id, string Name)
    : IRequest<Unit>;
