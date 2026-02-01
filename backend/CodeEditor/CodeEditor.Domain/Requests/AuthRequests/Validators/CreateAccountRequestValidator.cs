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
        public CreateAccountRequestValidator(IService<Entities.User> userService)
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
                )
                .WithMessage("The username is already used");

            RuleFor(request => request.Password)
                .NotNull()
                .NotEmpty()
                //.Matches(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?"":{}|<>]).{8,}$")
                .WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter and one special character");

            RuleFor(request => request.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(request => request.Password);
        }
    }
}
