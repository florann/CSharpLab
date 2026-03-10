namespace GitDashboard.Domain.Responses.GitEntryResponses
{
    public class GitFeedEntryResponse
    {
        public long GitFeedId { get; set; }

        public string IdTag { get; set; }

        public DateTimeOffset LastUpdateDate { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }
    }
}
