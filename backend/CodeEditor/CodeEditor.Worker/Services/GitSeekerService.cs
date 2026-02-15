using CodeEditor.Domain.Entities;
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

            var GitFeed = new GitFeed
            {
                Title = root.Element("title")?.Value
            };

            throw new NotImplementedException();
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
