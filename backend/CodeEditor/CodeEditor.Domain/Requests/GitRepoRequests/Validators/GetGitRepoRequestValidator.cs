using FluentValidation;

namespace CodeEditor.Domain.Requests.GitRepoRequests.Validators
{
    public class GetGitRepoRequestValidator : AbstractValidator<long>
    {
        public GetGitRepoRequestValidator()
        {
            RuleFor(gitRepoId => gitRepoId)
                .NotNull()
                .NotEmpty();
        }
    }
}
