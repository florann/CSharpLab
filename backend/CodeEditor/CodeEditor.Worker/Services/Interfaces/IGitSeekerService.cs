using CodeEditor.Domain.Entities;

namespace CodeEditor.Worker.Services.Interfaces
{
    public interface IGitSeekerService
    {
        Task<bool> HealthCheck();
        Task SeekUpate(GitRepo gitRepo);
        Task<IEnumerable<GitRepo>> GetGitRepoToPull();
    }
}
