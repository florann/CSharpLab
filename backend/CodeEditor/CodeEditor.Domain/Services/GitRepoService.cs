using AutoMapper;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class GitRepoService(
        IRepository<Entities.GitRepo> repository,
        IMapper mapper) : Service<Entities.GitRepo>(repository, mapper), IGitRepoService
    {
    }
}
