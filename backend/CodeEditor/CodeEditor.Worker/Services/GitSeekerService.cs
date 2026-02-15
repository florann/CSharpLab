using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Worker.Configuration;
using CodeEditor.Worker.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace CodeEditor.Worker.Services
{
    public class GitSeekerService : IGitSeekerService
    {
        private readonly ILogger<GitSeekerService> _logger;
        private readonly GitSeekerConfiguration _configuration;

        private readonly HttpClient _httpClient;

        private readonly IRepository<Domain.Entities.GitFeed> _gitFeedRepository;
        private readonly IRepository<Domain.Entities.GitFeedEntry> _gitFeedEntryRepository;

        public GitSeekerService(
            IOptions<GitSeekerConfiguration> configuration,
            ILogger<GitSeekerService> logger)
        {
            _configuration = configuration.Value;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GitUrl)
            };
            _logger = logger;
        }

        public async Task<bool> HealthCheck()
        {
            var result = await _httpClient.GetAsync("");
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Seek(GitRepo gitRepository)
        {
            var url = PrepareAtomReleaseNoteUrl(gitRepository);
            var result = await _httpClient.GetAsync(url);

            if (!result.IsSuccessStatusCode)
            {
                _logger.LogWarning("IsSuccessStatusCode false Seek method - {GitRepository}", gitRepository.ToString());
                return false;
            }

            using var stream = await result.Content.ReadAsStreamAsync();
            var document = await XDocument.LoadAsync(stream, LoadOptions.None, default);

            if (document?.Root == null)
            {
                _logger.LogWarning("Invalid XML document for {GitRepository}", gitRepository);
                return false;
            }

            var root = document.Root;

            var gitFeed = new GitFeed
            {
                IdGitRepo = gitRepository.Id,
                Title = root.Element("title")?.Value ?? string.Empty,
                Date = root.Element("updated")?.Value != null ? DateTimeOffset.Parse(root.Element("updated")!.Value) : DateTimeOffset.MinValue,
            };

            _gitFeedRepository.Add(gitFeed);

            var documentGitFeedEntries = root.Descendants("entry");
            if (documentGitFeedEntries == null)
            {
                _logger.LogWarning("gitFeedEntries is null {GitRepository}", gitRepository);
                return false;
            }

            var feedEntries = new List<GitFeedEntry>();

            foreach (var documentGitFeedEntry in documentGitFeedEntries)
            {
                _gitFeedEntryRepository.Add(new GitFeedEntry
                {
                    IdTag = documentGitFeedEntry.Element("id")?.Value ?? string.Empty,
                    AuthorName = documentGitFeedEntry.Element("author")?.Element("name")?.Value ?? string.Empty,
                    Date = documentGitFeedEntry.Element("updated") != null ? DateTimeOffset.Parse(documentGitFeedEntry.Element("updated")!.Value) : DateTimeOffset.MinValue,
                    Title = documentGitFeedEntry.Element("title")?.Value ?? string.Empty,
                    Link = documentGitFeedEntry.Element("link")?.Value ?? string.Empty,
                    Content = documentGitFeedEntry.Element("content")?.Value ?? string.Empty,
                    GitFeed = gitFeed,
                    GitFeedId = gitFeed.Id
                }); 
            }

            var saveResult = await _gitFeedRepository.SaveChangesAsync();

            if (saveResult <= 0)
            {
                _logger.LogWarning("Not entities were saved");
                return false;
            }

            return true;
        }

        private string PrepareAtomReleaseNoteUrl(GitRepo gitRepository)
        {
            var urlTemplate = _configuration.GitAtomReleaseNoteUrl;
            if (string.IsNullOrEmpty(urlTemplate))
                throw new ApplicationException("UrlTemplate is null or empty");

            var url = urlTemplate.Replace("{GIT_OWNER}", gitRepository.OwnerName);
            url = url.Replace("{GIT_REPO}", gitRepository.Name);

            return url;
        }
    }
}
