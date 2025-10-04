using Library.Application.Common.Specifications;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries.GetBooks.Specs;

public sealed class GetBooksQueryFilterSpec : BaseSpecification<Book>
{
    public GetBooksQueryFilterSpec(
        string? title, 
        Guid? authorId, 
        Guid? categoryId, 
        string? sortBy = null,
        string? sortDir = null,
        int? skip = null, 
        int? take = null
        )
    {
        Criteria = b =>
            (string.IsNullOrWhiteSpace(title) || b.Title.Contains(title)) &&
            (!authorId.HasValue || b.AuthorId == authorId.Value) &&
            (!categoryId.HasValue || b.CategoryId == categoryId.Value);

        AddInclude(b => b.Author);
        AddInclude(b => b.Category);

        var by = (sortBy ?? "updatedAt").ToLowerInvariant();
        var dir = (sortDir ?? "desc").ToLowerInvariant();
        bool isDescending = dir == "desc";

        switch (by)
        {
            case "title":
                if (isDescending) ApplyOrderByDescending(b => b.Title); else ApplyOrderBy(b => b.Title);
                break;
            case "totalcopies":
                if (isDescending) ApplyOrderByDescending(b => b.TotalCopies); else ApplyOrderBy(b => b.TotalCopies);
                break;
            case "copiesavailable":
                if (isDescending) ApplyOrderByDescending(b => b.CopiesAvailable); else ApplyOrderBy(b => b.CopiesAvailable);
                break;
            default: //updatedAt
                if (isDescending) ApplyOrderByDescending(b => b.UpdatedAt); else ApplyOrderBy(b => b.UpdatedAt);
                break;
        }

        if (skip.HasValue && take.HasValue)
            ApplyPaging(skip.Value, take.Value);
    }
}