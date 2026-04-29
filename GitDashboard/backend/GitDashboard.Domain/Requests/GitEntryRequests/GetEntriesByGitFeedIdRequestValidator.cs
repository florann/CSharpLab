using FluentValidation;

namespace GitDashboard.Domain.Requests.GitEntryRequests
{
    public class GetEntriesByGitFeedIdRequestValidator : AbstractValidator<long>
    {
        public GetEntriesByGitFeedIdRequestValidator()
        {
            RuleFor(gitFeedId => gitFeedId)
                .NotNull()
                .NotEmpty();
        }
    }
}
