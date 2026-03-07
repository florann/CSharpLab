using CodeEditor.Domain.DataAccess;
using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Infrastructure.DataAccess;
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
                services.AddScoped(sp =>
                    sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

                services.AddMemoryCache();


                // Services
                services.AddScoped<IService<Domain.Entities.Token>, TokenService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IGitRepoService, GitRepoService>();
                services.AddScoped<IGitFeedService, GitFeedService>();
                services.AddScoped<IGitFeedEntryService, GitFeedEntryService>();
                services.AddScoped(typeof(IMultiLayerDataAccessService<>), typeof(MultiLayerDataAccessService<>));
                // Repositories
                services.AddScoped<IRepository<Domain.Entities.User>, UserRepository>();
                services.AddScoped<IRepository<Domain.Entities.Token>, TokenRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitFeed>, GitFeedRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitFeedEntry>, GitFeedEntryRepository>();
                services.AddScoped<IRepository<Domain.Entities.GitRepo>, GitRepoRepository>();
                services.AddScoped<IRepository<Domain.Entities.UserGitRepo>, UserGitRepoRepository>();

                return services;
            }
        }
    }
}
