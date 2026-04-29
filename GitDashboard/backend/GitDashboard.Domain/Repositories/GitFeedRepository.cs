using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class GitFeedRepository(DbContext dbContext) : Repository<Entities.GitFeed>(dbContext)
    {
    }
}
