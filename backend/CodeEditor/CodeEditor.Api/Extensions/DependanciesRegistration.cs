using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Infrastructure;
using CodeEditor.Infrastructure.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace CodeEditor.Api.Extensions
{
    public static class DependanciesRegistration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddServices()
            {
                services.AddSignalR(options =>
                {
                    options.EnableDetailedErrors = true;
                });

                services.AddSingleton<IConnectionMultiplexer>(sp =>
                {
                    var configuration = ConfigurationOptions.Parse("localhost:6379");
                    configuration.AbortOnConnectFail = false;
                    return ConnectionMultiplexer.Connect(configuration);
                });

                // Services
                services.AddScoped<IDocumentService, DocumentService>();
                services.AddScoped<IStoreDocumentService, RedisStoreDocumentService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IBaseEntityService<Domain.Entities.User>, UserService>();
                services.AddScoped<IBaseEntityService<Domain.Entities.Token>, TokenService>();

                // Repositories
                services.AddScoped<IRepository<Domain.Entities.User>, UserRepository>();
                services.AddScoped<IRepository<Domain.Entities.Token>, TokenRepository>();

                return services;
            }
        }
    }
}
