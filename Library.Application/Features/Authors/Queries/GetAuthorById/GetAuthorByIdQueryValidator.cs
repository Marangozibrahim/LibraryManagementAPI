using FluentValidation;

namespace Library.Application.Features.Authors.Queries.GetAuthorById;
public sealed class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
