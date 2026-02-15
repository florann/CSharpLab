using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class GitFeedEntryRepository(DbContext dbContext) : Repository<Entities.GitFeedEntry>(dbContext)
    {
    }
}
