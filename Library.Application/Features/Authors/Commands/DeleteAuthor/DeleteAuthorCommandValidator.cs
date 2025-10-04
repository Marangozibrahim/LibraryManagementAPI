using FluentValidation;

namespace Library.Application.Features.Authors.Commands.DeleteAuthor;

public sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Author id is required.");
    }
}
