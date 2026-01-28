using CodeEditor.Domain.Repositories.Base;

namespace CodeEditor.Domain.Services
{
    public class TokenService(IRepository<Entities.Token> repository) : EntityService<Entities.Token>(repository)
    {
    }
}
