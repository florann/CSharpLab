using CodeEditor.Domain.Entities.Base;
using CodeEditor.Domain.Specifications.GitFeedEntrySpecification;
using GitDashboard.Domain.Specifications.GitFeedEntrySpecification;

namespace CodeEditor.Domain.Services
{
    public class GitFeedEntryService(
        IRepository<Entities.GitFeedEntry> repository,
        IMultiLayerDataAccessService<GitFeedEntry> multiLayerDataAccessService) : Service<Entities.GitFeedEntry>(repository), IGitFeedEntryService
    {
        public async Task<List<GitFeedEntry>?> GetListEntriesByGitIdFeed(long gitIdFeed)
        {
            var spec = new FindGitEntryByIdGitFeedSpecification(gitIdFeed);
            var ids = await _repository.FindAllAsync(spec, entity => entity.Id);

            if (ids == null || !ids.Any())
                return null;

            return await multiLayerDataAccessService
                .GetMultipleEntityValue(
                    "GitFeedEntry",
                    [.. ids],
                    (lst) => new FindGitFeedEntryByLstIdSpecification(lst)
                    );
        }
    }
}
