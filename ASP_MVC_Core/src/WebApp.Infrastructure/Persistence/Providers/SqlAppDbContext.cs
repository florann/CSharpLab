using Microsoft.EntityFrameworkCore;

namespace WebApp.Infrastructure.Persistence.Providers
{
    public class SqlAppDbContext(DbContextOptions<SqlAppDbContext> options) : AppDbContext(options)
    {
    }
}
