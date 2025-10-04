using Library.Application.Dtos.Category;
using MediatR;

namespace Library.Application.Features.Categories.Queries.GetCategories;
public sealed record GetCategoriesQuery() : IRequest<IReadOnlyList<CategoryDto>>
{
}
