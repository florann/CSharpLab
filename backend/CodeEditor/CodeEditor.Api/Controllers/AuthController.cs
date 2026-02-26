using AutoMapper;
using CodeEditor.Api.Exceptions;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
using CodeEditor.Domain.Responses.UserResponses;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodeEditor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Auth")]
    public class AuthController(
        ILogger<AuthController> logger,
        IAuthService authService,
        IUserService userService,
        LoginRequestValidator loginRequestValidator,
        CreateAccountRequestValidator createAccountRequestValidator,
        IOptions<JwtSettings> jwtSettings,
        IMapper mapper
        ) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var loginResponse = await authService.Login(request);
                Response.Cookies.Append("accessToken", loginResponse.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.AccessTokenExpirationInMinutes)
                });

                Response.Cookies.Append("refreshToken", loginResponse.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.RefreshTokenExpirationInMinutes)
                });

                var user = await userService.GetUserById(loginResponse.UserId);
                return Ok(mapper.Map<UserResponse>(user));
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception : {Ex}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createAccount")]
        public async Task<ActionResult> CreateAccount(CreateAccountRequest request)
        {
            var validationResult = await createAccountRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await authService.CreateAccount(request);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<bool>> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            try
            {
                var response = await authService.RefreshToken(refreshToken);

                Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.AccessTokenExpirationInMinutes)
                });

                Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtSettings.Value.RefreshTokenExpirationInMinutes)
                });

                return Ok(true);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception : {Ex}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> CheckStatus()
        {
            return Ok();
        }

    }
}
