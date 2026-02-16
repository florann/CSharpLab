using CodeEditor.Domain.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;

namespace CodeEditor.Domain.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public int Take { get; set; } = 0;
        public bool IsDesc { get; set; } = false;
        Expression<Func<T, object>> ISpecification<T>.OrderBy { get; set; }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public IEnumerable<Expression<Func<T, object>>> Includes { get; set; }
        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes = Includes.Append(include);
        }
    }
}
