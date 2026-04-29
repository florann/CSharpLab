namespace CodeEditor.Domain.Requests.GitRepoRequests
{
    public class AddGitRepoRequest
    {
        public string Url { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
    }
}
