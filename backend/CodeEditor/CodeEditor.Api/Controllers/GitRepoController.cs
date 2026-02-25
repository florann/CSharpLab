using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Requests.GitRepoRequests.Validators;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            GitRepoRequestValidator addGitRepoRequestValidator,
            GetGitRepoRequestValidator getGitRepoRequestValidator
        )
        : ControllerBase
    {
        [HttpPost("addGitRepo")]
        [ProducesResponseType<GitRepoResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GitRepoResponse>> AddGitRepo(AddGitRepoRequest request)
        {
            var result = await addGitRepoRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await gitRepoService.HandleGitRepoCreation(request);

            return Ok();
        }

        [HttpGet("GetGitReposFeed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GitRepoResponse>> GetGitRepo(GetGitRepoRequest request)
        {
            var result = getGitRepoRequestValidator.Validate(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepos = await gitRepoService.GetAllAsync();



            return Ok();
        }
    }
}
