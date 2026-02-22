namespace CodeEditor.Domain.Entities
{
    public class GitFeed
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset LastUpdateDate { get; set; }

        public IEnumerable<GitFeedEntry> GitFeedEntries { get; set; }

        public GitRepo GitRepository { get; set; }
    }
}
