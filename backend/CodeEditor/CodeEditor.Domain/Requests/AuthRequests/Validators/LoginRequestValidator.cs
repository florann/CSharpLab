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
                .MustAsync(async (userName, cancellation) =>
                {
                    var user = await userService.GetAsync(new FindUserByUserNameSpecification(userName));
                    return user != null;
                })
                .WithMessage("User does not exist.");

            RuleFor(request => request.Password)
               .NotEmpty()
               .NotNull();
        }
    }
}
