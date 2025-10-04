using FluentValidation;

namespace Library.Application.Features.Borrows.Commands.BorrowBook;

public sealed class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
{
    public BorrowBookCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required");
    }
}
