using Library.Application.Dtos.Category;
using MediatR;

namespace Library.Application.Features.Categories.Queries.GetCategoryById;
public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>
{
}
