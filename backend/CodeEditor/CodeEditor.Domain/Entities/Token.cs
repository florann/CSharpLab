namespace CodeEditor.Domain.Entities
{
    public class Token
    {
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; }
    }
}
