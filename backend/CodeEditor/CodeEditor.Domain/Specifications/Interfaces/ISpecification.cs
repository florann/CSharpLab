using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeEditor.Domain.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; set; }
    }
}
