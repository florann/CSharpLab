using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                GetType().Assembly,
                type => typeof(IBaseEntityConfiguration).IsAssignableFrom(type));
        }
    }
}
