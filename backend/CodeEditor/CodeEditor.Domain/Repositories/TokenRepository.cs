using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories
{
    public class TokenRepository : Repository<Entities.Token>
    {
        public TokenRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
