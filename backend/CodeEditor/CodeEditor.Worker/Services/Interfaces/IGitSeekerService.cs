using CodeEditor.Domain.Entities;

namespace CodeEditor.Worker.Services.Interfaces
{
    public interface IGitSeekerService
    {
        Task<bool> HealthCheck();
        Task<bool> Seek(GitRepo gitRepository);
        Task<bool> GetGitRepoToPull();
    }
}
