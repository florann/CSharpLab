using CodeEditor.Domain.Entities;

namespace CodeEditor.Domain.Specifications.GitRepoSpecification
{
    public class FindGitRepoByIdSpecification : Specification<GitRepo>
    {
        public FindGitRepoByIdSpecification(long id)
        {
            Criteria = entity => entity.Id == id;
        }
    }
}
