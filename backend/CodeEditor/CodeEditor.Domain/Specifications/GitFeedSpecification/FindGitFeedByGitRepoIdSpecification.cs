namespace CodeEditor.Domain.Specifications.GitFeedSpecification
{
    public class FindGitFeedByGitRepoIdSpecification : Specification<Entities.GitFeed>
    {
        public FindGitFeedByGitRepoIdSpecification(long idGitRepo)
        {
            Criteria = entity => entity.IdGitRepo == idGitRepo;
        }
    }
}
