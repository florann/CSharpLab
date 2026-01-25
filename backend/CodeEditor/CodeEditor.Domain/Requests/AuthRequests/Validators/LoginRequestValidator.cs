using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using FluentValidation;

namespace CodeEditor.Domain.Requests.AuthRequests.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IBaseEntityService<Entities.User> userService)
        {

            RuleFor(request => request.UserName)
                .NotEmpty()
                .NotNull()
                .DbCheck(
                    userService,
                    username => new FindUserByUserNameSpecification(username)
                    );

            RuleFor(request => request.Password)
               .NotEmpty()
               .NotNull();
        }
    }
}
