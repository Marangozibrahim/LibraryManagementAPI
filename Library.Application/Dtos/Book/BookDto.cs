namespace Library.Application.Dtos.Book
{
    public sealed record BookDto(
        Guid Id,
        string Title, 
        int TotalCopies,
        int CopiesAvailable,
        string AuthorName,
        string CategoryName
        )
    {
    }
}