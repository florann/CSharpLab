using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Requests.GitRepoRequests.Validators;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Tags("GitRepo")]
    public class GitRepoController
        (
            ILogger<GitRepoController> logger,
            IGitRepoService gitRepoService,
            GitRepoRequestValidator gitRepoRequestValidator
        )
        : ControllerBase
    {
        [HttpPost("addGitRepo")]
        [ProducesResponseType<GitRepoResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GitRepoResponse>> AddGitRepo(GitRepoRequest request)
        {
            var result = await gitRepoRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await gitRepoService.HandleGitRepoCreation(request);

            return Ok();
        }
    }
}
