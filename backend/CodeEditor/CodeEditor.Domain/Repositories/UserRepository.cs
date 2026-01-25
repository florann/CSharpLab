using CodeEditor.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Repositories
{
    public class UserRepository : Repository<Entities.User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
