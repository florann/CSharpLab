using CodeEditor.Domain.Entities;
using CodeEditor.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CodeEditor.Api.Extensions
{
    public static class ConfigurationExtension
    {
        extension(WebApplicationBuilder builder)
        {
            public WebApplicationBuilder AddConfiguration()
            {
                var jwtSettings = builder.Configuration
                    .GetSection("JwtSettings")
                    .Get<JwtSettings>()! ?? throw new ApplicationException("JwtSettings is null");

                builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                    );

                builder.Services.AddScoped<DbContext>(provider =>
                    provider.GetRequiredService<ApplicationContext>()
                );

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)
                        ),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });


                return builder;
            }
        }
    }
}
