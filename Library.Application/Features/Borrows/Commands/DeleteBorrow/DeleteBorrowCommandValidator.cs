using FluentValidation;

namespace Library.Application.Features.Borrows.Commands.DeleteBorrow;
public sealed class DeleteBorrowCommandValidator : AbstractValidator<DeleteBorrowCommand>
{
    public DeleteBorrowCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Borrow Id is required");
    }
}
