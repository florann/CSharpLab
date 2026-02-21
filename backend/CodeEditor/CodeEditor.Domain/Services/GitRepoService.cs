using AutoMapper;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Services.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class GitRepoService(
        IRepository<GitRepo> gitRepoRepository,
        IRepository<GitFeed> gitFeedRepository,
        IMapper mapper) : Service<GitRepo>(gitRepoRepository, mapper), IGitRepoService
    {
        public async Task<GitRepoResponse> HandleGitRepoCreation(GitRepoRequest request)
        {
            var gitRepo = new GitRepo
            {
                Url = request.Url,
                OwnerName = request.OwnerName,
                Name = request.Name,
                LastUpdateDate = DateTime.UtcNow
            };

            var gitFeed = new GitFeed
            {
                GitRepository = gitRepo,
                IdGitRepo = gitRepo.Id,
                LastUpdateDate = DateTime.MinValue,
                Title = "",
            };

            gitRepo.GitFeed = gitFeed;
            gitRepo.IdGitFeed = gitFeed.Id;

            gitRepoRepository.Add(gitRepo);
            gitFeedRepository.Add(gitFeed);

            await gitRepoRepository.SaveChangesAsync();

            return _mapper.Map<GitRepoResponse>(gitRepo);
        }
    }
}
