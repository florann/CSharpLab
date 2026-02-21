using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Responses.GitRepoResponses;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IGitRepoService : IService<Entities.GitRepo>
    {
        Task<GitRepoResponse> HandleGitRepoCreation(GitRepoRequest request);
    }
}
