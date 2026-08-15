using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure.Persistence;

namespace WebApp.Extensions
{
    public static class WebApplicationExtension
    {
        extension(WebApplication webApp)
        {
            public WebApplication UpdateDatabase()
            {
                var scopeServices = webApp.Services.CreateScope();
                var db = scopeServices.ServiceProvider.GetRequiredService<AppDbContext>() ?? throw new ApplicationException("Unable to instanciate db service");
                db.Database.Migrate();

                return webApp;
            }
        }
    }
}
