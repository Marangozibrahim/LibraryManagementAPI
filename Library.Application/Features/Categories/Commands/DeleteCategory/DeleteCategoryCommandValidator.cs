using FluentValidation;

namespace Library.Application.Features.Categories.Commands.DeleteCategory;
public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category id is required");
    }
}
