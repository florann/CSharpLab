namespace CodeEditor.Domain.Specifications.GitRepoSpecification
{
    public class FindGitRepoByListIdSpecification : Specification<Entities.GitRepo>
    {
        public FindGitRepoByListIdSpecification(List<long> gitRepoIds)
        {
            Criteria = entity => gitRepoIds.Contains(entity.Id);
        }
    }
}
