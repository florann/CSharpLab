using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class GitRepoRepository(DbContext dbContext) : Repository<Entities.GitRepo>(dbContext)
    {

    }
}
