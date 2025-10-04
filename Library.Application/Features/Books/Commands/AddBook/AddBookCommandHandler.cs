using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Commands.AddBook;

public sealed class AddBookCommandHandler(
    IRepository<Book> _bookRepo,
    IRepository<Author> _authorRepo,
    IRepository<Category> _categoryRepo,
    IUnitOfWork _uow) 
    : IRequestHandler<AddBookCommand, Guid>
{
    public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        if(!await _authorRepo.AnyAsync(a => a.Id == request.AuthorId))
            throw new KeyNotFoundException("Author not found");

        if(!await _categoryRepo.AnyAsync(a => a.Id == request.CategoryId))
            throw new KeyNotFoundException("Category not found");

        var book = new Book(request.Title, request.TotalCopies, request.AuthorId, request.CategoryId);
        await _bookRepo.AddAsync(book, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
