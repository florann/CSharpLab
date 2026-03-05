using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Specifications.GitRepoSpecification;
using CodeEditor.Domain.Specifications.UserSpecification;

namespace CodeEditor.Domain.Services
{
    public class GitRepoService(
        IRepository<GitRepo> gitRepoRepository,
        IRepository<GitFeed> gitFeedRepository,
        IMultiLayerDataAccessService<GitRepo> multiLayerDataAccessService,
        IRepository<User> userRepository,
        IRepository<UserGitRepo> userGitRepoRepository 
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

        public async Task SetUserAllGitRepo(long userId)
        {
            var spec = new FindUserByIdSpecification(userId);
            spec.AddInclude(entity => entity.UserGitRepos);
            var user = await userRepository.FindOneAsync(spec) ?? throw new EnhancedException("user is null");

            var userGitRepos = user.UserGitRepos;

            userGitRepos.ForEach(userGitRepoRepository.Delete);

            var gitRepos = await gitRepoRepository.GetAllAsync() ?? throw new EnhancedException("list git repos is null");

            gitRepos.ToList().ForEach(gitRepo =>
            {
                userGitRepoRepository.Add(new UserGitRepo
                {
                    UserId = userId,
                    User = user,
                    GitRepoId = gitRepo.Id,
                    GitRepo = gitRepo
                });
            });

            await userGitRepoRepository.SaveChangesAsync();
        }
    }
}
