using CodeEditor.Infrastructure;
using CodeEditor.Worker.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Worker.Extensions
{
    public static class ConfigurationExtension
    {
        extension(HostApplicationBuilder builder)
        {
            public HostApplicationBuilder AddConfiguration()
            {
                builder.Configuration.GetSection("GitSeekerConfiguration").Get<GitSeekerConfiguration>();

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
