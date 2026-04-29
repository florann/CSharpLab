namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IGitFeedEntryService : IService<Entities.GitFeedEntry>
    {
        public Task<List<GitFeedEntry>?> GetListEntriesByGitIdFeed(long gitIdFeed);
    }
}
