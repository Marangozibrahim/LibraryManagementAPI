using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Commands.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(
    IUnitOfWork _uow)
    : IRequestHandler<DeleteAuthorCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorRepo = _uow.Repository<Author>();
        var bookRepo = _uow.Repository<Book>();

        var author = await authorRepo.GetByIdAsync(request.Id);
        if (author == null)
            throw new KeyNotFoundException("Author not found");

        var hasBooks = await bookRepo.AnyAsync(b => b.AuthorId == request.Id);
        if (hasBooks)
            throw new InvalidOperationException("Cannot delete author with existing books.");

        authorRepo.Delete(author);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
