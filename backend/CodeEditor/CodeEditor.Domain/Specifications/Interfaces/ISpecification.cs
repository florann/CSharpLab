using System.Linq.Expressions;

namespace CodeEditor.Domain.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        int Take { get; set; }
        bool IsDesc { get; set; }
        Expression<Func<T, object>> OrderBy { get; set; }
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
        void AddInclude(Expression<Func<T, object>> include);
    }
}
