using CodeEditor.Domain.Requests.UserRequests.Validators;
using CodeEditor.Domain.Responses.UserResponses;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("User")]
    public class UserController
        (
            GetUserBydIdValidator getUserByIdValidator,
            IService<Domain.Entities.User> userService
        ) : ControllerBase
    {

        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<UserResponse>> GetUserById(long id)
        {
            var validationResult = await getUserByIdValidator.ValidateAsync(id);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var spec = new FindUserByIdSpecification(id);
            var result = await userService.GetAsync(spec);

            return Ok(result);
        }
    }
}
