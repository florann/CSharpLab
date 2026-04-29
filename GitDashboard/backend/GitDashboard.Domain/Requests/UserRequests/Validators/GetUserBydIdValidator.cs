using CodeEditor.Domain.Extensions;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using FluentValidation;

namespace CodeEditor.Domain.Requests.UserRequests.Validators
{
    public class GetUserBydIdValidator : AbstractValidator<long>
    {
        public GetUserBydIdValidator(IUserService userService)
        {
            RuleFor(userId => userId)
                .DbCheck(userService,
                (userId) => new FindUserByIdSpecification(userId))
                .WithMessage("User does not exist");
        }
    }
}
