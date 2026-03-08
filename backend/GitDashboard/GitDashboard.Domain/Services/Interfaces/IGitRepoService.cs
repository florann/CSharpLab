using CodeEditor.Domain.Records;
using CodeEditor.Domain.Requests.GitRepoRequests;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IGitRepoService : IService<GitRepo>
    {
        Task<GitRepo> HandleGitRepoCreation(AddGitRepoRequest request);
        Task<GitRepo?> GetGitRepo(long gitRepoId);
        Task<List<GitRepo>?> GetAllGitRepoAsync(GetUserGitRepoRequest request);
        Task<List<GitRepoSummary>?> GetAllGitRepSummaryAsync();
        Task<List<GitRepo>> SetUserAllGitRepo(long userId);
    }
}
