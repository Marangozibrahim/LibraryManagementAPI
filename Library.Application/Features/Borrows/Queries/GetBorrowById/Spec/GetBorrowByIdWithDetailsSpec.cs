using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Borrows.Queries.GetBorrowById.Spec;
public sealed class GetBorrowByIdWithDetailsSpec : BaseSpecification<Borrow>
{
    public GetBorrowByIdWithDetailsSpec(Guid id)
    {
        Criteria = b => b.Id == id;
        AddInclude(b => b.Book);
    }
}