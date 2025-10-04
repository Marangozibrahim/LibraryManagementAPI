using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Category;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Categories.Queries.GetCategories;
public sealed class GetCategoriesQueryHandler(
    IUnitOfWork _uow,
    IMapper _mapper
    ) : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    public async Task<IReadOnlyList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _uow.Repository<Category>().ListAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
    }
}
