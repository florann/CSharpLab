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
        public Expression<Func<T, bool>> Criteria { get; set; }
    }
}
