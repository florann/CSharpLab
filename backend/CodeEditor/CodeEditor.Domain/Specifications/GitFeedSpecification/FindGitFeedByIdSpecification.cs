using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Specifications.GitFeedSpecification
{
    public class FindGitFeedByIdSpecification : Specification<Entities.GitFeed>
    {
        public FindGitFeedByIdSpecification(long id)
        {
            Criteria = entity => entity.Id == id;
        }
    }
}
