namespace CodeEditor.Domain.Specifications.GitRepoSpecification
{
    public class FindLast10GitRepoOrderByUpdateDateDescSpecification : Specification<Entities.GitRepo>
    {
        public FindLast10GitRepoOrderByUpdateDateDescSpecification()
        {
            OrderBy = entity => entity.LastUpdateDate;
            Take = 10;
        }
    }
}
