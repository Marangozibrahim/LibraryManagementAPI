using System.Linq.Expressions;

namespace Library.Application.Common.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        int? Skip { get; }
        int? Take { get; }
        bool AsNoTracking { get; }
    }
}
