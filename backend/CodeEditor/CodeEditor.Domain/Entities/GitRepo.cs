namespace CodeEditor.Domain.Entities
{
    public class GitRepo
    {
        public long Id { get; set; }

        public string OwnerName { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
