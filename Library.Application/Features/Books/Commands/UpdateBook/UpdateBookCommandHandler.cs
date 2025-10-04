using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler(
    IUnitOfWork _uow) 
    : IRequestHandler<UpdateBookCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _uow.Repository<Book>().GetByIdAsync(request.Id);
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        book.Update(request.Title, request.TotalCopies, request.CopiesAvailable, request.AuthorId, request.CategoryId);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
