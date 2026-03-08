using FluentValidation;

namespace CodeEditor.Domain.Requests.GitRepoRequests.Validators
{
    public class SearchGitRepoByNameValidator : AbstractValidator<string>
    {
        public SearchGitRepoByNameValidator()
        {
            RuleFor(searchString => searchString)
                .NotNull()
                .NotEmpty();
        }
    }
}
