using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Borrows.Queries.GetBorrowsByUser.Specs;
public sealed class GetBorrowsByAdminSpec : BaseSpecification<Borrow>
{
    public GetBorrowsByAdminSpec()
    {
        AddInclude(bw => bw.Book);

        AsNoTracking = true;
    }
}
