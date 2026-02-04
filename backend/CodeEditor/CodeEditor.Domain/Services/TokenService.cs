using AutoMapper;
using CodeEditor.Domain.Repositories.Base;

namespace CodeEditor.Domain.Services
{
    public class TokenService(IRepository<Entities.Token> repository, IMapper mapper) : Service<Entities.Token>(repository, mapper)
    {
    }
}
