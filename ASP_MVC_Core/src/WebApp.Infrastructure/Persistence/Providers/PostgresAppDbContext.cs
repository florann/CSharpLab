using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure.Persistence.Interfaces;

namespace WebApp.Infrastructure.Persistence.Providers
{
    public class PostgresAppDbContext(DbContextOptions<PostgresAppDbContext> options) : AppDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(PostgresAppDbContext).Assembly,
                type => typeof(IPostgresEntityConfiguration).IsAssignableFrom(type));
        }
    }
}
