namespace CodeEditor.Domain.Entities
{
    public class Document
    {
        long Id { get; set; }
        long UserId { get; set; }
        string Name { get; set; }
        string Path { get; set; }
    }
}
