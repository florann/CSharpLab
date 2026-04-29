using AutoMapper;
using CodeEditor.Domain.Requests.GitRepoRequests;
using CodeEditor.Domain.Requests.GitRepoRequests.Validators;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Services.Interfaces;
using GitDashboard.Domain.Requests.GitRepoRequests.Validators;
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
            SetUserAllGitRepoRequestValidator setUserAllGitRepoRequestValidator,
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

            if (result == null || result.Count == 0)
                return NoContent();

            return Ok(mapper.Map<List<GitRepoTitleResponse>>(result));
        }


        [HttpPost("setUserAllGitRepo/{userGuid:guid}")]
        [EndpointDescription("Set all GitRepo linked to the given userId")]
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoResponse>>> SetUserAllGitRepo([FromRoute] Guid userGuid)
        {
            var result = setUserAllGitRepoRequestValidator.Validate(userGuid);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepos = await gitRepoService.SetUserAllGitRepo(userGuid);
            if (gitRepos.Count <= 0)
                return NoContent();
            
            return Ok(mapper.Map<List<GitRepoResponse>>(gitRepos));
        }    
    
        [HttpGet("getUserGitRepos/{userGuid:guid}")]
        [EndpointDescription("Used to retrieve all GitRepos entities link to the specified user")]
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoResponse>>> GetUserGitRepos([FromRoute] Guid userGuid)
        {
            var result = getUserGitRepoRequestValidator.Validate(userGuid);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitRepos = await gitRepoService.GetAllGitRepoAsync(userGuid);

            return Ok(mapper.Map<List<GitRepoResponse>>(gitRepos));
        }
    }
}
