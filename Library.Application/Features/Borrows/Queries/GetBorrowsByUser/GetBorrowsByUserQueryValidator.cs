using FluentValidation;

namespace Library.Application.Features.Borrows.Queries.GetBorrowsByUser;

public sealed class GetBorrowsByUserQueryValidator : AbstractValidator<GetBorrowsByUserQuery>
{
    public GetBorrowsByUserQueryValidator()
    {

    }
}
