using FluentValidation;

namespace Library.Application.Features.Categories.Queries.GetCategories;
public sealed class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
{
    public GetCategoriesQueryValidator()
    {
    }
}
