using AutoMapper;
using CodeEditor.Domain.Requests.UserRequests.Validators;
using CodeEditor.Domain.Responses.UserResponses;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("User")]
    public class UserController
        (
            GetUserBydIdValidator getUserByIdValidator,
            IUserService userService,
            IMapper mapper
        ) : ControllerBase
    {

        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<UserResponse>> GetUserById(long id)
        {
            var validationResult = await getUserByIdValidator.ValidateAsync(id);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await userService.GetUserById(id);

            return Ok(mapper.Map<UserResponse>(user));
        }
    }
}
