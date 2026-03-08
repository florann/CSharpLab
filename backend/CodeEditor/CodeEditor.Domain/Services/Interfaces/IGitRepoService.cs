using CodeEditor.Domain.Records;
using CodeEditor.Domain.Requests.GitRepoRequests;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IGitRepoService : IService<GitRepo>
    {
        Task<GitRepo> HandleGitRepoCreation(AddGitRepoRequest request);
        Task<List<GitRepo>?> GetAllGitRepoAsync(GetUserGitRepoRequest request);
        Task<List<GitRepoSummary>?> GetAllGitRepoNameAsync();
        Task<List<GitRepo>> SetUserAllGitRepo(long userId);
    }
}
