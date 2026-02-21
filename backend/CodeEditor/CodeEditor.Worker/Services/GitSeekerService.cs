using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Helpers;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Specifications.GitFeedEntrySpecification;
using CodeEditor.Domain.Specifications.GitRepoSpecification;
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

        private readonly IRepository<GitRepo> _gitRepoRepository;
        private readonly IRepository<GitFeed> _gitFeedRepository;
        private readonly IRepository<GitFeedEntry> _gitFeedEntryRepository;

        public GitSeekerService(
            IOptions<GitSeekerConfiguration> configuration,
            ILogger<GitSeekerService> logger,
            IRepository<GitRepo> gitRepoRepository,
            IRepository<GitFeed> gitFeedRepository,
            IRepository<GitFeedEntry> gitFeedEntryRepository)
        {
            _configuration = configuration.Value;
            _logger = logger;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GitUrl)
            };

            _gitFeedRepository = gitFeedRepository;
            _gitFeedEntryRepository = gitFeedEntryRepository;
        }

        public async Task<bool> HealthCheck()
        {
            var result = await _httpClient.GetAsync("");
            if (!result.IsSuccessStatusCode)
                _logger.LogWarning("HealthCheck return a none success status code");

            return result.IsSuccessStatusCode;
        }

        public async Task SeekUpate(GitRepo gitRepo)
        {
            var root = await FetchAtomGitReleaseNote(gitRepo);

            gitRepo.GitFeed.Title = root.Element("title")?.Value ?? string.Empty;
            gitRepo.GitFeed.LastUpdateDate = root.Element("updated")?.Value != null ? DateTimeOffset.Parse(root.Element("updated")!.Value) : DateTimeOffset.MinValue;
            _gitFeedRepository.Update(gitRepo.GitFeed);

            var documentGitFeedEntries = root.Descendants("entry");
            if (documentGitFeedEntries == null)
            {
                throw new EnhancedException("git feed entries are null", [gitRepo.ToString() ?? "cannot convert"]);
            }

            await RefreshFeedEntries(documentGitFeedEntries, gitRepo.GitFeed);

            gitRepo.LastUpdateDate = DateTime.UtcNow;

            var saveResult = await _gitFeedRepository.SaveChangesAsync();

            if (saveResult <= 0)
            {
                throw new EnhancedException("No entities were saved", [gitRepo.ToString() ?? "cannot convert"]);
            }
        }

        public async Task<IEnumerable<GitRepo>> GetGitRepoToPull()
        {
            var spec = new FindLast10GitRepoOrderByUpdateDateDescSpecification();
            return await _gitRepoRepository.FindAllAsync(spec) ?? [];
        }

        private async Task<XElement> FetchAtomGitReleaseNote(GitRepo gitRepo)
        {
            var url = gitRepo.PrepareAtomReleaseNoteUrl(_configuration.GitAtomReleaseNoteUrl);
            var result = await _httpClient.GetAsync(url);

            if (!result.IsSuccessStatusCode)
            {
                _logger.LogWarning("IsSuccessStatusCode false Seek method - {GitRepository}", gitRepo.ToString());
                throw new EnhancedException("Error fetching atom release note", [gitRepo.ToString() ?? "cannot convert"]);
            }

            using var stream = await result.Content.ReadAsStreamAsync();
            var document = await XDocument.LoadAsync(stream, LoadOptions.None, default);

            if (document?.Root == null)
            {
                _logger.LogWarning("Invalid XML document for {GitRepository}", gitRepo);
                throw new EnhancedException("Invalid XML document", [gitRepo.ToString() ?? "cannot convert"]);
            }

            return document!.Root!;
        }

        private async Task RefreshFeedEntries(IEnumerable<XElement> documentGitFeedEntries, GitFeed gitFeed)
        {
            var spec = new FindGitEntryByIdGitFeedSpecification(gitFeed.Id);
            var feedEntries = await _gitFeedEntryRepository.FindAllAsync(spec);

            feedEntries?.ToList().ForEach(el =>
            {
                _gitFeedEntryRepository.Delete(el);
            });

            foreach (var documentGitFeedEntry in documentGitFeedEntries)
            {
                _gitFeedEntryRepository.Add(new GitFeedEntry
                {
                    IdTag = documentGitFeedEntry.Element("id")?.Value ?? string.Empty,
                    AuthorName = documentGitFeedEntry.Element("author")?.Element("name")?.Value ?? string.Empty,
                    LastUpdateDate = documentGitFeedEntry.Element("updated") != null ? DateTimeOffset.Parse(documentGitFeedEntry.Element("updated")!.Value) : DateTimeOffset.MinValue,
                    Title = documentGitFeedEntry.Element("title")?.Value ?? string.Empty,
                    Link = documentGitFeedEntry.Element("link")?.Value ?? string.Empty,
                    Content = documentGitFeedEntry.Element("content")?.Value ?? string.Empty,
                    GitFeed = gitFeed,
                    GitFeedId = gitFeed.Id
                });
            }
        }
    }
}
