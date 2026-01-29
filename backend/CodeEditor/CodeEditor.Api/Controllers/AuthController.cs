using CodeEditor.Api.Exceptions;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
using CodeEditor.Domain.Responses.AuthResponses;
using CodeEditor.Domain.Services;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodeEditor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        ILogger<AuthController> logger,
        IAuthService authService,
        LoginRequestValidator loginRequestValidator,
        CreateAccountRequestValidator createAccountRequestValidator,
        RefreshTokensRequestValidator refreshTokenRequestValidator,
        IOptions<JwtSettings> jwtSettings
        ) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var response = await authService.Login(request);

                Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.AccessTokenExpirationInMinutes)
                });

                Response.Cookies.Append("refreshToken", response.RefreshToken   , new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.RefreshTokenExpirationInMinutes)
                });

                return Ok("Login successful");
            }
            catch(HttpResponseException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex )
            {
                logger.LogError("Exception : {Ex}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
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

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> refresh(RefreshTokensRequest request)
        {
            var validationResult = await refreshTokenRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var response = await authService.RefreshToken(request);
                return Ok(response);
            }
            catch(HttpResponseException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                logger.LogError("Exception : {Ex}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
