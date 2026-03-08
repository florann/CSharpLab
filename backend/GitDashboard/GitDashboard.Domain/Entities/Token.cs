using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.Entities
{
    public class Token : Entity
    {
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; }
    }
}
