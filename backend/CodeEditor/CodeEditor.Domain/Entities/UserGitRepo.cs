using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.Entities
{
    public class UserGitRepo : Entity
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long GitRepoId { get; set; }
        public GitRepo GitRepo { get; set; }
    }
}
