using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeEditor.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; set; }
    }
}
