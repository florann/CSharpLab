using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Mapper;
using CodeEditor.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CodeEditor.Api.Extensions
{
    public static class ConfigurationExtension
    {
        extension(WebApplicationBuilder builder)
        {
            public WebApplicationBuilder AddConfiguration()
            {
                builder.Services.Configure<JwtSettings>(
                    builder.Configuration.GetSection("JwtSettings"));

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
                        ValidAudience = "CodeEditorFrontEnd",
                        ValidIssuer = "CodeEditorApi",
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["accessToken"];
                            Console.WriteLine($"Cookie 'accessToken' found: {!string.IsNullOrEmpty(token)}");
                            Console.WriteLine($"Token value: {token?.Substring(0, Math.Min(20, token?.Length ?? 0))}...");
                            context.Token = token;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

                builder.Services.AddAutoMapper(typeof(ProfileConfiguration).Assembly);

                return builder;
            }
        }
    }
}
