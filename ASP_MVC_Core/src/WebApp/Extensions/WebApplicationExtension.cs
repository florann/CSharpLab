using Microsoft.EntityFrameworkCore;
using WebApp.Enum;
using WebApp.Infrastructure.Persistence;
using WebApp.Infrastructure.Persistence.Providers;

namespace WebApp.Extensions
{
    public static class WebApplicationExtension
    {
        extension(WebApplication webApp)
        {
            private Providers GetProvider()
            {
                var providerName = webApp.Configuration["DatabaseProvider"];

                return providerName switch
                {
                    "Postgres" => Providers.Postgres,
                    "SqlServer" => Providers.SqlServer,
                    _ => throw new ApplicationException("No provider was configured")
                };
            }

            public WebApplication UpdateDatabase()
            {
                var scopeServices = webApp.Services.CreateScope();
                var provider = GetProvider(webApp);

                AppDbContext db = provider switch
                {
                    Providers.SqlServer => scopeServices.ServiceProvider.GetRequiredService<SqlServerAppDbContext>() ?? throw new ApplicationException("Unable to instanciate db service"),
                    Providers.Postgres => scopeServices.ServiceProvider.GetRequiredService<PostgresAppDbContext>() ?? throw new ApplicationException("Unable to instanciate db service"),
                    _ => throw new ApplicationException("No provider found for UpdateDatabase"),
                };
                db.Database.Migrate();

                return webApp;
            }
        }
    }
}
