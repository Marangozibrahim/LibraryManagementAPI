using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Commands.DeleteBook;

public sealed class DeleteBookCommandHandler(
    IUnitOfWork _uow) 
    : IRequestHandler<DeleteBookCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var bookRepo = _uow.Repository<Book>();
        var borrowRepo = _uow.Repository<Borrow>();

        var book = await bookRepo.GetByIdAsync(request.Id);
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        var hasBorrows = await borrowRepo.AnyAsync(bw => bw.BookId == book.Id);
        if (hasBorrows)
        {
            throw new InvalidOperationException("Cannot delete a book that has borrow records.");
        }

        bookRepo.Delete(book);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
