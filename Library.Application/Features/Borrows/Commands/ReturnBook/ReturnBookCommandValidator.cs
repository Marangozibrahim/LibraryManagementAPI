using FluentValidation;

namespace Library.Application.Features.Borrows.Commands.ReturnBook;

public sealed class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
{
    public ReturnBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Borrow ID is required");
    }
}
