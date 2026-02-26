using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class GitRepoService(
        IRepository<GitRepo> gitRepoRepository,
        IRepository<GitFeed> gitFeedRepository) : Service<GitRepo>(gitRepoRepository), IGitRepoService
    {
        public async Task<GitRepo> HandleGitRepoCreation(AddGitRepoRequest request)
        {
            var gitRepo = new GitRepo
            {
                Url = request.Url,
                OwnerName = request.OwnerName,
                Name = request.Name,
                LastUpdateDate = DateTime.UtcNow,
            };

            var gitFeed = new GitFeed
            {
                LastUpdateDate = DateTime.UtcNow,
                Title = "",
                GitRepository = gitRepo
            };

            gitRepo.GitFeed = gitFeed;

            gitFeedRepository.Add(gitFeed);
            gitRepoRepository.Add(gitRepo);

            await gitRepoRepository.SaveChangesAsync();

            return gitRepo;
        }
    }
}
