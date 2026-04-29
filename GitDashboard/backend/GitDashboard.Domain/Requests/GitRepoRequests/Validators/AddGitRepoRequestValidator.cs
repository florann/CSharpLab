using FluentValidation;
using System.Text.RegularExpressions;

namespace CodeEditor.Domain.Requests.GitRepoRequests.Validators
{
    public class AddGitRepoRequestValidator : AbstractValidator<AddGitRepoRequest>
    {
        private const string GitHubUrlPattern = @"^https://github\.com/([\w-]+)/([\w.-]+)$";

        public AddGitRepoRequestValidator()
        {
            RuleFor(request => request.Url)
                .NotEmpty()
                .NotNull()
                .Matches(GitHubUrlPattern)
                .WithMessage("The URL format is not valid");

            RuleFor(request => request.OwnerName)
                .NotNull()
                .NotEmpty();

            RuleFor(request => request.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(request => request)
           .Custom((request, context) =>
           {
               var match = Regex.Match(request.Url ?? string.Empty, GitHubUrlPattern);

               var urlOwner = match.Groups[1].Value;
               var urlRepo = match.Groups[2].Value;

               if (!urlOwner.Equals(request.OwnerName, StringComparison.OrdinalIgnoreCase))
               {
                   context.AddFailure(
                       nameof(request.OwnerName),
                       $"OwnerName '{request.OwnerName}' does not match the owner in URL '{urlOwner}'");
               }

               if (!urlRepo.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
               {
                   context.AddFailure(
                       nameof(request.Name),
                       $"Name '{request.Name}' does not match the repository in URL '{urlRepo}'");
               }
           });
        }
    }
}
