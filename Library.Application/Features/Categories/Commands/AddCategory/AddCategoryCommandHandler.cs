using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Categories.Commands.AddCategory;
public sealed class AddCategoryCommandHandler(
    IUnitOfWork _uow,
    IMapper _mapper
    ) : IRequestHandler<AddCategoryCommand, Guid>
{
    public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);

        await _uow.Repository<Category>().AddAsync(category, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
