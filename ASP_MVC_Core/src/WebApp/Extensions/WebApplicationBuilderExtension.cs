using Microsoft.EntityFrameworkCore;
using WebApp.Enum;
using WebApp.Infrastructure.Persistence.Providers;

namespace WebApp.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        extension(WebApplicationBuilder webApplicationBuilder)
        {

            private Providers GetProvider()
            {
                var providerName = webApplicationBuilder.Configuration["DatabaseProvider"];

                return providerName switch
                {
                    "Postgre" => Providers.Postgre,
                    "SqlServer" => Providers.SqlServer,
                    _ => throw new ApplicationException("No provider was configured")
                };
            }

            private string GetConnectionString(Providers provider)
            {
                var connectionString = webApplicationBuilder.Configuration.GetSection("ConnectionStrings")[provider.ToString()];

                if (string.IsNullOrEmpty(connectionString))
                    throw new ApplicationException("Connection string is invalid");

                return connectionString;
            }

            public WebApplicationBuilder ConfigureDatabaseContext()
            {
                var provider = GetProvider(webApplicationBuilder);
                var connectionString = GetConnectionString(webApplicationBuilder, provider);

                switch (provider)
                {
                    case Providers.SqlServer:
                        webApplicationBuilder.Services.AddDbContext<SqlServerAppDbContext>(options =>
                        {
                            options.UseSqlServer(connectionString);
                        });
                        break;
                    case Providers.Postgre:
                        webApplicationBuilder.Services.AddDbContext<PostgresAppDbContext>(options =>
                        {
                            options.UseNpgsql(connectionString);
                        });
                        break;
                    default:
                        throw new ApplicationException("Provider not found");
                }

                return webApplicationBuilder;
            }
        }
    }
}
