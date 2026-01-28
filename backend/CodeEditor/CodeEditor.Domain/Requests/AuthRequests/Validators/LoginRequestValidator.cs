using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using FluentValidation;

namespace CodeEditor.Domain.Requests.AuthRequests.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IEntityService<Entities.User> userService)
        {
            RuleFor(request => request.UserName)
                .NotEmpty()
                .NotNull()
                .DbCheck(
                    userService,
                    username => new FindUserByUserNameSpecification(username)
                    )
                .WithMessage("User does not exist in db");

            RuleFor(request => request.Password)
               .NotEmpty()
               .NotNull();
        }
    }
}
