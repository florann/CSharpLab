namespace CodeEditor.Domain.Specifications.UserSpecification
{
    public class FindUserByIdSpecification : Specification<Entities.User>
    {
        public FindUserByIdSpecification(long id)
        {
            Criteria = entity => entity.Id == id;
        }
    }
}
