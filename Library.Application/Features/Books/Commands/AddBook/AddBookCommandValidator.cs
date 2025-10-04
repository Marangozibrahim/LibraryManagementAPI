using FluentValidation;

namespace Library.Application.Features.Books.Commands.AddBook;

public sealed class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.TotalCopies).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.AuthorId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
