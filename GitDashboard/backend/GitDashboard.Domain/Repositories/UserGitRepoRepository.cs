using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class UserGitRepoRepository(DbContext dbContext) : Repository<Entities.UserGitRepo>(dbContext)
    {
    }
}
