using Microsoft.EntityFrameworkCore;
using WebApp.Enum;
using WebApp.Infrastructure.Persistence;

public static class AppDbContextOptionsFactory
{
    public static DbContextOptions<AppDbContext> Create(Providers providerEnum, string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        switch (providerEnum)
        {
            case Providers.Postgres:
                optionsBuilder.UseNpgsql(connectionString);
                break;
            case Providers.SqlServer:
                optionsBuilder.UseSqlServer(connectionString);
                break;
            default:
                throw new ApplicationException("Provider not found");
        }

        return optionsBuilder.Options;
    }
}