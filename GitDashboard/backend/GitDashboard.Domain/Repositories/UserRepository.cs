using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class UserRepository : Repository<Entities.User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
