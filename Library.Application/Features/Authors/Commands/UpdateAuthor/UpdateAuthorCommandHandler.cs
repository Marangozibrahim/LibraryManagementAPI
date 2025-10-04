using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler(
    IUnitOfWork _uow)
    : IRequestHandler<UpdateAuthorCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorRepo = _uow.Repository<Author>();

        var author = await authorRepo.GetByIdAsync(request.Id, cancellationToken);
        if (author is null)
            throw new KeyNotFoundException($"Author with Id {request.Id} not found.");

        author.Update(request.Name);

        authorRepo.Update(author);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
