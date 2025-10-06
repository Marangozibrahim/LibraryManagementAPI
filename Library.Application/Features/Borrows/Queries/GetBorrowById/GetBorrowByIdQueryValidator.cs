using FluentValidation;

namespace Library.Application.Features.Borrows.Queries.GetBorrowById;
public sealed class GetBorrowByIdQueryValidator : AbstractValidator<GetBorrowByIdQuery>
{
    public GetBorrowByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
