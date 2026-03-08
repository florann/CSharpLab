using AutoMapper;
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
            GitRepoRequestValidator addGitRepoRequestValidator,
            GetUserGitRepoRequestValidator getUserGitRepoRequestValidator,
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

        [HttpGet("getAllGitRepoTittle")]
        [ProducesResponseType<List<GitRepoTitleResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitRepoTitleResponse>>> GetAllGitRepoTittle()
        {
            //await gitRepoService.HandleGitRepoCreation(request);

            return Ok();
        }


        [HttpPost("setUserAllGitRepo/{userId}")]
        [ProducesResponseType<List<GitRepoResponse>>(StatusCodes.Status200OK)]
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
