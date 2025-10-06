using FluentValidation;

namespace Library.Application.Features.Categories.Queries.GetCategoryById;
public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
