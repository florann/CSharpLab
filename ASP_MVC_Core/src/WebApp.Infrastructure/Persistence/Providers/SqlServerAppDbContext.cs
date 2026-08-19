using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure.Persistence.Interfaces;

namespace WebApp.Infrastructure.Persistence.Providers
{
    public class SqlServerAppDbContext(DbContextOptions<SqlServerAppDbContext> options) : AppDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                 typeof(SqlServerAppDbContext).Assembly,
                 type => typeof(ISqlServerEntityConfiguration).IsAssignableFrom(type));
        }
    }
}
