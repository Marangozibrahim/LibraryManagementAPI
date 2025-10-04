using Library.Domain.Entities.Common;

namespace Library.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public int TotalCopies { get; private set; }
        public int CopiesAvailable { get; private set; }

        public Guid AuthorId { get; private set; }
        public Guid CategoryId { get; private set; }

        public Author Author { get; private set; } = default!;
        public Category Category { get; private set; } = default!;
        public ICollection<Borrow> Borrows { get; private set; } = new List<Borrow>();

        public Book(string title, int totalCopies, Guid authorId, Guid categoryId)
        {
            Title = title;
            TotalCopies = totalCopies;
            CopiesAvailable = totalCopies;

            AuthorId = authorId;
            CategoryId = categoryId;
        }

        public void Update(string? title, int? totalCopies, int? copiesAvailable, Guid? authorId, Guid? categoryId)
        {
            if (title != null) Title = title;

            if (totalCopies.HasValue)
            {
                if (totalCopies.Value < 0)
                    throw new InvalidOperationException("Total copies cannot be negative.");

                if (CopiesAvailable > totalCopies.Value)
                    CopiesAvailable = totalCopies.Value;

                TotalCopies = totalCopies.Value;
            }

            if (copiesAvailable.HasValue)
            {
                if (copiesAvailable.Value < 0 || copiesAvailable.Value > TotalCopies)
                    throw new InvalidOperationException("Copies available cannot exceed total copies.");

                CopiesAvailable = copiesAvailable.Value;
            }

            if (authorId.HasValue) AuthorId = authorId.Value;
            if (categoryId.HasValue) CategoryId = categoryId.Value;
        }

        public void DecreaseCopiesAvailableByOne()
        {
            CopiesAvailable -= 1;
        }

        public void IncreaseCopiesAvailableByOne()
        {
            CopiesAvailable += 1;
        }

        private Book() { }
    }
}