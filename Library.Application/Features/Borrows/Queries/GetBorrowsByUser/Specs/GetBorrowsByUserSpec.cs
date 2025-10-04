using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Borrows.Queries.GetBorrowsByUser.Specs;

public sealed class GetBorrowsByUserSpec : BaseSpecification<Borrow>
{
    public GetBorrowsByUserSpec(Guid userId)
    {
        Criteria = bw => bw.UserId == userId;

        AddInclude(bw => bw.Book);

        ApplyOrderBy(bw => bw.UpdatedAt);

        AsNoTracking = true;
    }
}
