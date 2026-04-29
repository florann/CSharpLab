using CodeEditor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitDashboard.Domain.Specifications.UserSpecification
{
    public class FindUserByGuidSpecification : Specification<User>
    {
        public FindUserByGuidSpecification(Guid userGuid)
        {
            Criteria = entity => entity.Guid == userGuid;
        }
    }
}
