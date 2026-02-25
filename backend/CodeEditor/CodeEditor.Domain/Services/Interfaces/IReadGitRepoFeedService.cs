namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IReadGitRepoFeedService
    {
        Task<string?> GetGitRepoFeedAsync(string gitRepoFeedId);
        Task<bool> SaveGitRepoFeedAsync(string gitRepoFeedId, string content);
        Task<bool> DeleteGitRepoFeedAsync(string gitRepoFeedId);
    }
}
