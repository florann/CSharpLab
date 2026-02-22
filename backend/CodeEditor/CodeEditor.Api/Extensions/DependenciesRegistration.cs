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
    public static class DependenciesRegistration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddServices()
            {
                services.AddSingleton<IConnectionMultiplexer>(sp =>
                {
                    var configuration = ConfigurationOptions.Parse("localhost:6379");
                    configuration.AbortOnConnectFail = false;
                    return ConnectionMultiplexer.Connect(configuration);
                });

                // Services
                services.AddScoped<IDocumentService, DocumentService>();
                services.AddScoped<IStoreDocumentService, RedisStoreDocumentService>();
                
                services.AddScoped<IService<Domain.Entities.Token>, TokenService>();
                
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IGitRepoService, GitRepoService>();
                services.AddScoped<IGitFeedService, GitFeedService>();
                services.AddScoped<IGitFeedEntryService, GitFeedEntryService>();

                // Repositories
                services.AddScoped<IRepository<Domain.Entities.User>, UserRepository>();
                services.AddScoped<IRepository<Domain.Entities.Token>, TokenRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitFeed>, GitFeedRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitFeedEntry>, GitFeedEntryRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitRepo>, GitRepoRepository>();

                return services;
            }
        }
    }
}
