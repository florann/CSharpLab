using Microsoft.EntityFrameworkCore;

namespace WebApp.Infrastructure.Persistence.Providers
{
    public class PostgreAppDbContext(DbContextOptions<PostgreAppDbContext> options) : AppDbContext(options)
    {
    }
}
