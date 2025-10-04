using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries.GetAllBooks.Specs
{
    public sealed class GetAllBooksSpec : BaseSpecification<Book>
    {
        public GetAllBooksSpec()
        {
            AddInclude(b => b.Author);
            AddInclude(b => b.Category);
        }
    }
}
