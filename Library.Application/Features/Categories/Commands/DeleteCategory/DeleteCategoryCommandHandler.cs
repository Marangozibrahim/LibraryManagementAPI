using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Categories.Commands.DeleteCategory;
public sealed class DeleteCategoryCommandHandler(
    IUnitOfWork _uow
    ) 
    : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepo = _uow.Repository<Category>();
        var bookRepo = _uow.Repository<Book>();

        var category = await categoryRepo.GetByIdAsync(request.Id);
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        var hasBooks = await bookRepo.AnyAsync(b => b.CategoryId == request.Id);
        if (hasBooks)
            throw new InvalidOperationException("Cannot delete category with existing books.");

        categoryRepo.Delete(category);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
