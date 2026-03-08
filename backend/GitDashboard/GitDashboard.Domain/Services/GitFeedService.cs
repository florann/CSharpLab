using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class GitFeedService(
        IRepository<Entities.GitFeed> repository)
        : Service<Entities.GitFeed>(repository), IGitFeedService
    {
    }
}
