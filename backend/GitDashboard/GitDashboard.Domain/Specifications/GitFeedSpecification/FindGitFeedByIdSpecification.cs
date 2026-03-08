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
