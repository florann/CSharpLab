using CodeEditor.Domain.DataAccess;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.GitRepoSpecification;
using CodeEditor.Domain.Specifications.UserSpecification;

namespace CodeEditor.Domain.Services
{
    public class GitRepoService(
        IRepository<GitRepo> gitRepoRepository,
        IRepository<GitFeed> gitFeedRepository,
        IMultiLayerDataAccessService<GitRepo> multiLayerDataAccessService,
        IRepository<User> userRepository
        ) : Service<GitRepo>(gitRepoRepository), IGitRepoService
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

        public async Task<List<GitRepo>?> GetAllGitRepoAsync(GetUserGitRepoRequest request)
        {
            var spec = new FindUserByIdSpecification(request.UserId);
            spec.AddInclude(entity => entity.UserGitRepos);
            var user = await userRepository.FindOneAsync(spec);

            if (user == null)
                return null;

            var userGitRepoIds = user.UserGitRepos.Select(userGitRepo => userGitRepo.GitRepoId).ToList();
            if (userGitRepoIds == null)
                return null;

            var gitRepos = await multiLayerDataAccessService
                .GetMultipleEntityValue("GitRepo", 
                userGitRepoIds, 
                (userGitRepoIds) 
                    => new FindGitRepoByListIdSpecification(userGitRepoIds)
                );

            return gitRepos;
        }
    }
}
