using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CodeEditor.Domain.Requests.AuthRequests.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(request => request.Password)
               .NotEmpty()
               .NotNull();
        }
    }
}
