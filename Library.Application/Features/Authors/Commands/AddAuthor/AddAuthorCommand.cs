using MediatR;

namespace Library.Application.Features.Authors.Commands.AddAuthor;

public sealed record AddAuthorCommand(string Name)
    : IRequest<Guid>;