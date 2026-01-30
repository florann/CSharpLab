namespace CodeEditor.Domain.Specifications.TokenSpecification
{
    public class FindTokenByRefreshTokenSpecification : Specification<Entities.Token>
    {
        public FindTokenByRefreshTokenSpecification(string refreshToken)
        {
            Criteria = entity => entity.RefreshToken == refreshToken;
        }
    }
}
