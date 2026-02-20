namespace CodeEditor.Domain.Entities
{
    public class GitRepo
    {
        public long Id { get; set; }

        public long IdGitFeed { get; set; }

        public string OwnerName { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public DateTimeOffset LastUpdateDate { get; set; }

        public GitFeed GitFeed { get; set; }
    }
}
