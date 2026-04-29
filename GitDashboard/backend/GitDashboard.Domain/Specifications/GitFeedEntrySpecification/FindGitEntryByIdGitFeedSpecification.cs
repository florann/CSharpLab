namespace CodeEditor.Domain.Specifications.GitFeedEntrySpecification
{
    public class FindGitEntryByIdGitFeedSpecification : Specification<Entities.GitFeedEntry>
    {
        public FindGitEntryByIdGitFeedSpecification(long idGitFeed)
        {
            Criteria = entity => entity.GitFeedId == idGitFeed;
        }
    }
}
