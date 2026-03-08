    using AutoMapper;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Requests.GitRepoRequests.Validators;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Services.Interfaces;
using FluentValidation;
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
            AddGitRepoRequestValidator addGitRepoRequestValidator,
            GetGitRepoRequestValidator getGitRepoRequestValidator,
            GetUserGitRepoRequestValidator getUserGitRepoRequestValidator,
            IMapper mapper
        )
        : ControllerBase
    {
        [HttpGet("{gitRepoId}")]
        [EndpointDescription("Get the GitRepo thank to the given id")]
        [ProducesResponseType<GitRepoResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GitRepoResponse>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GitRepoResponse>> GetGitRepo(long gitRepoId)
        {
            var result = await getGitRepoRequestValidator.ValidateAsync(gitRepoId);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepo = await gitRepoService.GetGitRepo(gitRepoId);

            if (gitRepo == null)
                return NoContent();

            return Ok(mapper.Map<GitRepoResponse>(gitRepo));
        }


        [HttpPost]
        [EndpointDescription("Add a GitRepo")]
        [ProducesResponseType<GitRepoResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GitRepoResponse>> AddGitRepo(AddGitRepoRequest request)
        {
            var result = await addGitRepoRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await gitRepoService.HandleGitRepoCreation(request);

            return Created();
        }

        [HttpGet("getAllGitRepoSummary")]
        [EndpointDescription("Get all GitRepoSummary stored in database")]
        [ProducesResponseType<List<GitRepoTitleResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoTitleResponse>>> GetAllGitRepoSummary()
        {
            var result = await gitRepoService.GetAllGitRepSummaryAsync();

            if (result.Count == 0)
                return NoContent();

            return Ok(result);
        }


        [HttpPost("setUserAllGitRepo/{userId}")]
        [EndpointDescription("Set all GitRepo linked to the given userId")]
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoResponse>>> SetUserAllGitRepo(long userId)
        {
            if (userId <= 0)
                return BadRequest();

            var result = await gitRepoService.SetUserAllGitRepo(userId);
            if (result.Count <= 0)
                return NoContent();
            
            return Ok(mapper.Map<GitRepoResponse>(result));
        }    
    
        [HttpGet("getUserGitRepos")]
        [EndpointDescription("Used to retrieve all GitRepos entities link to the specified user")]
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoResponse>>> GetUserGitRepos(GetUserGitRepoRequest request)
        {
            var result = getUserGitRepoRequestValidator.Validate(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepos = await gitRepoService.GetAllGitRepoAsync(request);

            return Ok(mapper.Map<List<GitRepoResponse>>(gitRepos));
        }
    }
}
