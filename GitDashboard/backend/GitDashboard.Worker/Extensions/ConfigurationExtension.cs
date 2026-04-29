using CodeEditor.Infrastructure;
using CodeEditor.Worker.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Worker.Extensions
{
    public static class ConfigurationExtension
    {
        extension(HostApplicationBuilder builder)
        {
            public HostApplicationBuilder AddConfiguration()
            {
                builder.Services.Configure<GitSeekerConfiguration>(
                    builder.Configuration.GetSection("GitSeekerConfiguration")
                );


                builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

                builder.Services.AddScoped<DbContext>(provider =>
                    provider.GetRequiredService<ApplicationContext>()
                );


                return builder;
            }
        }
    }
}
