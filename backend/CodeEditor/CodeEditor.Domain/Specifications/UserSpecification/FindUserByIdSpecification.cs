using CodeEditor.Domain.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Specifications.UserSpecification
{
    public class FindUserByIdSpecification : Specification<Entities.User>
    {
        public FindUserByIdSpecification(long id)
        {
            Criteria = entity => entity.Id == id;
        }
    }
}
