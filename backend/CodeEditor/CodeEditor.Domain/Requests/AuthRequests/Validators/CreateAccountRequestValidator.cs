using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Requests.AuthRequests.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator(IBaseEntityService<Entities.User> userService)
        {
            RuleFor(request => request.UserName)
                .NotEmpty()
                .NotNull()
                .DbCheck(
                    userService,
                    (userName) => 
                        {
                            return new FindUserByUserNameSpecification(userName);
                        },
                    false
                );

            RuleFor(request => request.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
