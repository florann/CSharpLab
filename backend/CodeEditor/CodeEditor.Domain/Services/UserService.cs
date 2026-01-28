using CodeEditor.Domain.Repositories.Base;

namespace CodeEditor.Domain.Services
{
    public class UserService(IRepository<Entities.User> repository) : EntityService<Entities.User>(repository)
    {
    }
}
