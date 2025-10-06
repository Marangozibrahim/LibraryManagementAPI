using Library.Domain.Enums;

namespace Library.Application.Dtos.Borrow;

public sealed record BorrowDto
{
    public Guid Id { get; init; }
    public Guid BookId { get; init; }
    public string BookName { get; init; } = string.Empty;
    public DateTime BorrowDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime? ReturnDate { get; init; }
    public BorrowStatus BorrowStatus { get; init; }
}

