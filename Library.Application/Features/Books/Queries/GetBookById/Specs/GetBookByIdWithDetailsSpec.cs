using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries.GetBookById.Specs;

public sealed class GetBookByIdWithDetailsSpec : BaseSpecification<Book>
{
    public GetBookByIdWithDetailsSpec(Guid id)
    {
        Criteria = b => b.Id == id;
        AddInclude(b => b.Author);
        AddInclude(b => b.Category);
    }
}