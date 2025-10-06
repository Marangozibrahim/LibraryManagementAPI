using AutoMapper;
using Library.Application.Abstractions.Persistence;
using Library.Application.Dtos.Category;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Categories.Queries.GetCategoryById;
public sealed class GetCategoryByIdQueryHandler(
    IRepository<Category> _categoryRepo,
    IMapper _mapper)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepo.GetByIdAsync(request.Id);
        if (category == null)
        {
            throw new KeyNotFoundException("Category not found with the given Id");
        }

        return _mapper.Map<CategoryDto>(category);
    }
}
