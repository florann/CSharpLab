using CodeEditor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeEditor.Api.Extensions
{
    public static class ConfigurationExtension
    {
        extension(WebApplicationBuilder builder)
        {
            public WebApplicationBuilder AddConfiguration()
            {
                builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                    );

                return builder;
            }
        }
    }
}
