using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Categories.Commands.UpdateCategory;
public sealed class UpdateCategoryCommandHandler(
    IUnitOfWork _uow
    ) : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepo = _uow.Repository<Category>();

        var category = await categoryRepo.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            throw new KeyNotFoundException($"Category with Id {request.Id} not found.");

        category.Update(request.Name);

        categoryRepo.Update(category);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
