namespace Library.Application.Dtos.Book;
public sealed record UpdateBookRequest(
    string? Title,
    int? TotalCopies,
    int? CopiesAvailable,
    Guid? AuthorId,
    Guid? CategoryId
);
