using CodeEditor.Domain.Repositories.Base;

namespace CodeEditor.Domain.Services
{
    public class UserService(IRepository<Entities.User> repository) : BaseEntityService<Entities.User>(repository)
    {
    }
}
