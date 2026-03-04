using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public Token Token { get; set; }
    }
}
