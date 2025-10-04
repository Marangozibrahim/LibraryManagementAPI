using Library.Domain.Entities.Common;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class Borrow : BaseEntity
    {
        public DateTime BorrowDate { get; private set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; } = null;
        public BorrowStatus Status { get; private set; } = BorrowStatus.Borrowed;

        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }

        public Book Book { get; private set; } = default!;

        public Borrow(Guid bookId, Guid userId, DateTime? dueDate = null)
        {
            //Book = book ?? throw new ArgumentNullException(nameof(book));
            BookId = bookId;
            UserId = userId;
            DueDate = dueDate;
            BorrowDate = DateTime.UtcNow;
            Status = BorrowStatus.Borrowed;
        }

        public void ReturnBook()
        {
            ReturnDate = DateTime.UtcNow;
            Status = BorrowStatus.Returned;
        }

        private Borrow() { }
    }
}