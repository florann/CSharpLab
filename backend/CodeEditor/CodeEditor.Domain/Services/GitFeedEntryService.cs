using AutoMapper;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class GitFeedEntryService(
        IRepository<Entities.GitFeedEntry> repository,
        IMapper mapper) : Service<Entities.GitFeedEntry>(repository, mapper), IGitFeedEntryService
    {

    }
}
