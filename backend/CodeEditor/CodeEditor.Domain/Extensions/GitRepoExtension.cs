using CodeEditor.Domain.Entities;

namespace CodeEditor.Domain.Extensions
{
    public static class GitRepoExtension
    {
        extension(GitRepo gitRepo)
        {
            public string PrepareAtomReleaseNoteUrl(string urlTemplate)
            {
                if (string.IsNullOrEmpty(urlTemplate))
                    throw new ApplicationException("UrlTemplate is null or empty");

                var url = urlTemplate.Replace("{GIT_OWNER}", gitRepo.OwnerName);
                url = url.Replace("{GIT_REPO}", gitRepo.Name);

                return url;
            }
        }
    }
}
