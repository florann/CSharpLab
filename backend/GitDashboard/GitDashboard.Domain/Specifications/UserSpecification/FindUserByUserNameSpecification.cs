namespace CodeEditor.Domain.Specifications.UserSpecification
{
    public class FindUserByUserNameSpecification : Specification<Entities.User>
    {
        public FindUserByUserNameSpecification(string userName)
        {
            Criteria = entity => entity.UserName == userName;
        }
    }
}
