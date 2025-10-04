using FluentValidation;

namespace Library.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CopiesAvailable)
            .Must((x, copies) => !copies.HasValue || !x.TotalCopies.HasValue || copies.Value <= x.TotalCopies.Value)
            .WithMessage("Copies available cannot exceed total copies.");
    }
}
