using System.Linq.Expressions;

namespace Library.Application.Common.Specifications
{
    public sealed class AllSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria => null;
        public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? OrderBy => null;
        public Expression<Func<T, object>>? OrderByDescending => null;
        public int? Skip => null;
        public int? Take => null;
        public bool AsNoTracking => false;
    }
}
