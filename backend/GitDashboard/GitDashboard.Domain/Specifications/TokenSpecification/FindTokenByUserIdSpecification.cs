namespace CodeEditor.Domain.Specifications.TokenSpecification
{
    public class FindTokenByUserIdSpecification : Specification<Entities.Token>
    {
        public FindTokenByUserIdSpecification(long userId)
        {
            Criteria = entity => entity.UserId == userId;
        }
    }
}
