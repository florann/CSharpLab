using CodeEditor.Domain.Specifications.Interfaces;
using System.Linq.Expressions;

namespace CodeEditor.Domain.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public int Take { get; set; } = 0;
        public bool IsDesc { get; set; } = false;
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];
        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
    }
}
