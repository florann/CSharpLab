using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.Entities
{
    public class GitFeed : Entity
    {
        public string Title { get; set; }

        public DateTimeOffset LastUpdateDate { get; set; }

        public IEnumerable<GitFeedEntry> GitFeedEntries { get; set; }

        public long GitRepoId { get; set; }
    }
}
