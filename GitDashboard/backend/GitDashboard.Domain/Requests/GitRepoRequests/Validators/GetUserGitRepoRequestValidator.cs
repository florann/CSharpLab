using FluentValidation;

namespace CodeEditor.Domain.Requests.GitRepoRequests.Validators
{
    public class GetUserGitRepoRequestValidator : AbstractValidator<Guid>
    {
        public GetUserGitRepoRequestValidator()
        {
            RuleFor(userGuid => userGuid)
                .NotEmpty()
                .NotNull();
        }
    }
}
