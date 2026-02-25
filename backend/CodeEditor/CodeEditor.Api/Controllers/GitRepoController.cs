using AutoMapper;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Requests.GitRepoRequests.Validators;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Responses.UserResponses;
using CodeEditor.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

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
            GetGitRepoRequestValidator getGitRepoRequestValidator,
            IMapper mapper
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
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoResponse>>> GetGitRepo(GetGitRepoRequest request)
        {
            var result = getGitRepoRequestValidator.Validate(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepos = await gitRepoService.GetAllAsync();

            return Ok(mapper.Map<List<GitRepoResponse>>(gitRepos));
        }
    }
}
