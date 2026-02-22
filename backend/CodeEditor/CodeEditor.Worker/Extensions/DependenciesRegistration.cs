using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Infrastructure.Migrations;
using CodeEditor.Worker.Services;
using CodeEditor.Worker.Services.Interfaces;

namespace CodeEditor.Worker.Extensions
{
    public static class DependenciesRegistration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddDependencies()
            {
                services.AddScoped<IGitSeekerService, GitSeekerService>();

                services.AddScoped<IGitRepoService, GitRepoService>();

                services.AddScoped<IRepository<GitRepo>, GitRepoRepository>();
                services.AddScoped<IRepository<GitFeed>, GitFeedRepository>();
                services.AddScoped<IRepository<GitFeedEntry>, GitFeedEntryRepository>();

                return services;
            }
        }
    }
}
