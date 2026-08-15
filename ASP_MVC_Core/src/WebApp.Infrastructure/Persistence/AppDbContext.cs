using Microsoft.EntityFrameworkCore;

namespace WebApp.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
