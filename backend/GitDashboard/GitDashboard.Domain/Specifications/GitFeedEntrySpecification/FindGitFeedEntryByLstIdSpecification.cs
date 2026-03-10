namespace GitDashboard.Domain.Specifications.GitFeedEntrySpecification
{
    public class FindGitFeedEntryByLstIdSpecification : Specification<GitFeedEntry>
    {
        public FindGitFeedEntryByLstIdSpecification(List<long> ids)
        {
            Criteria = entity => ids.Contains(entity.Id);
        }
    }
}
