using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
using CodeEditor.Domain.Responses.AuthResponses;
using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        IAuthService authService,
        LoginRequestValidator loginRequestValidator,
        CreateAccountRequestValidator createAccountRequestValidator
        ) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> login(LoginRequest request)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await authService.Login(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createAccount")]
        public async Task<ActionResult> createAccount(CreateAccountRequest request)
        {
            var validationResult = await createAccountRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await authService.CreateAccount(request);
            return NoContent();
        }

    }
}
