using FluentValidation;

namespace GitDashboard.Domain.Requests.GitRepoRequests.Validators
{
    public class SetUserAllGitRepoRequestValidator : AbstractValidator<Guid>
    {
        public SetUserAllGitRepoRequestValidator()
        {
            RuleFor(userGuid => userGuid)
                .NotNull()
                .NotEmpty();
        }
    }
}
