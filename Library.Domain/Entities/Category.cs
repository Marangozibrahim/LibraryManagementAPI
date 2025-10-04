using Library.Domain.Entities.Common;

namespace Library.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }

        public void Update(string name)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name;
        }
    }
}
