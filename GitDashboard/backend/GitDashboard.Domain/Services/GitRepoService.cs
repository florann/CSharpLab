using CodeEditor.Domain.Records;
using CodeEditor.Domain.Repositories;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Specifications.GitRepoSpecification;
using CodeEditor.Domain.Specifications.UserSpecification;
using GitDashboard.Domain.Specifications.UserSpecification;

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
                GitRepoId = gitRepo.Id
            };

            gitRepo.GitFeed = gitFeed;

            gitFeedRepository.Add(gitFeed);
            gitRepoRepository.Add(gitRepo);

            await gitRepoRepository.SaveChangesAsync();

            return gitRepo;
        }

        public async Task<List<GitRepo>?> GetAllGitRepoAsync(Guid userGuid)
        {
            var spec = new FindUserByGuidSpecification(userGuid);
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
                (ids) 
                    =>
                    {
                        var s = new FindGitRepoByListIdSpecification(ids);
                        s.AddInclude(gitRepo => gitRepo.GitFeed);
                        return s;
                    }
                );

            return gitRepos;
        }

        public async Task<List<GitRepo>> SetUserAllGitRepo(Guid userGuid)
        {
            var spec = new FindUserByGuidSpecification(userGuid);
            spec.AddInclude(entity => entity.UserGitRepos);
            var user = await userRepository.FindOneAsync(spec) ?? throw new EnhancedException("user is null");

            var userGitRepos = user.UserGitRepos;

            userGitRepos.ForEach(userGitRepoRepository.Delete);

            var gitRepos = await gitRepoRepository.GetAllAsync() ?? throw new EnhancedException("list git repos is null");

            gitRepos.ToList().ForEach(gitRepo =>
            {
                userGitRepoRepository.Add(new UserGitRepo
                {
                    UserId = user.Id,
                    User = user,
                    GitRepoId = gitRepo.Id,
                    GitRepo = gitRepo
                });
            });

            await userGitRepoRepository.SaveChangesAsync();

            return [.. gitRepos];
        }

        public async Task<List<GitRepoSummary>?> GetAllGitRepSummaryAsync()
        {
            var result = await _repository.GetAllTransformedAsync(e => new GitRepoSummary
            {
                Id = e.Id,
                Name = e.Name
            });
            return (result != null) ? [..result] : null;
        }

        public async Task<GitRepo?> GetGitRepo(long gitRepoId)
        {
            return await multiLayerDataAccessService.GetEntityValue(
                "GitRepo",
                gitRepoId,
                new FindGitRepoByIdSpecification(gitRepoId));
        }
    }
}
