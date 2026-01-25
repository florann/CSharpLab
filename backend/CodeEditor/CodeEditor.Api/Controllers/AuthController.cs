using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
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
        IAuthService tokenService,
        LoginRequestValidator loginRequestValidator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login(LoginRequest request)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await tokenService.Login(request);
            return Ok(response);
        }
    }
}
