namespace CodeEditor.Domain.Entities
{
    public class GitFeed
    {
        public long Id { get; set; }

        public long IdGitRepo { get; set; }

        public string Title { get; set; }

        public DateTimeOffset Date { get; set; }

        public IEnumerable<GitFeedEntry> GitFeedEntries { get; set; }

        public GitRepo GitRepository { get; set; }
    }
}
