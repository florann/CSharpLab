using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.Entities
{
    public class GitRepo : Entity
    {
        public long IdGitFeed { get; set; }

        public string OwnerName { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public DateTimeOffset LastUpdateDate { get; set; }

        public GitFeed GitFeed { get; set; }

        public List<UserGitRepo> UserGitRepos { get; set; }
    }
}
