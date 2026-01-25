namespace CodeEditor.Domain.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
