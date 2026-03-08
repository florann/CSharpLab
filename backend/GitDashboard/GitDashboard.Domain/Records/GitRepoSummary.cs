namespace CodeEditor.Domain.Records
{
    public record GitRepoSummary
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
