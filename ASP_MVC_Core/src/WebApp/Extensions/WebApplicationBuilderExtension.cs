using Microsoft.EntityFrameworkCore;
using WebApp.Enum;
using WebApp.Infrastructure.Persistence;

namespace WebApp.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        extension(WebApplicationBuilder webApplicationBuilder) {
            public WebApplicationBuilder ConfigureDatabaseContext()
            {
                var provider = webApplicationBuilder.Configuration["DatabaseProvider"];
                if (string.IsNullOrEmpty(provider))
                    throw new ApplicationException("No provider was configured");

                var connectionString = webApplicationBuilder.Configuration.GetSection("ConnectionStrings")[provider];
                if (string.IsNullOrEmpty(connectionString))
                    throw new ApplicationException("No connection string for provider : " + provider );

                var providerEnum = GetProvider(provider);

                webApplicationBuilder.Services.AddDbContext<AppDbContext>(options =>
                {
                    switch (providerEnum)
                    {
                        case Providers.Postgre:
                            options.UseNpgsql(connectionString);
                            return;
                        case Providers.SqlServer:
                            options.UseSqlServer(connectionString);
                            return;
                        default:
                            throw new ApplicationException("Provider not found");
                    }
                });

                return webApplicationBuilder;
            }

            private static Providers GetProvider(string provider) => provider switch
            {
                "Postgre" => Providers.Postgre,
                "SqlServer" => Providers.SqlServer,
                _ => throw new ApplicationException("Could not deduce provider enum value")
            };
        }
    }
}
