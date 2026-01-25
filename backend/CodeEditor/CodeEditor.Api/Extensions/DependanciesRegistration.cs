using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Infrastructure.Services;
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

                services.AddScoped<IDocumentService, DocumentService>();
                services.AddScoped<IStoreDocumentService, RedisStoreDocumentService>();

                return services;
            }
        }
    }
}
