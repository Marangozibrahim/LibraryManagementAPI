using FluentValidation;

namespace Library.Application.Features.Authors.Queries.GetAuthors;

public sealed class GetAuthorsQueryValidator : AbstractValidator<GetAuthorsQuery>
{
    public GetAuthorsQueryValidator()
    {
    }
}
