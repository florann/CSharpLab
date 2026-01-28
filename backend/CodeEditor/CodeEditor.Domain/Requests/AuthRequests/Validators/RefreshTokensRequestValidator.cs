using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using FluentValidation;

namespace CodeEditor.Domain.Requests.AuthRequests.Validators
{
    public class RefreshTokensRequestValidator : AbstractValidator<RefreshTokensRequest>
    {
        public RefreshTokensRequestValidator(
            IEntityService<Entities.User> userService,
            IEntityService<Entities.Token> tokenService
            )
        {
            RuleFor(request => request.RefreshToken)
                .NotEmpty()
                .NotNull();

            RuleFor(request => request.UserId)
                .DbCheck(userService,
                    (userId) => new FindUserByIdSpecification(userId)
                )
                .WithMessage("User does not exist");
        }
    }
}
