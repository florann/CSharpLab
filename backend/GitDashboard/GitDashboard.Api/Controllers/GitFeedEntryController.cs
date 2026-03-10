using AutoMapper;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Services.Interfaces;
using GitDashboard.Domain.Requests.GitEntryRequests;
using GitDashboard.Domain.Responses.GitEntryResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GitDashboard.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Tags("GitFeedEntry")]
    public class GitFeedEntryController(
        IGitFeedEntryService gitFeedEntryService,
        GetEntriesByGitFeedIdRequestValidator getEntriesByGitFeedIdRequestValidator,
        IMapper mapper
        ) : ControllerBase
    {
        [HttpGet("getEntriesByGitFeedId/{gitFeedId:long}")]
        [ProducesResponseType<List<GitFeedEntryResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GitFeedEntryResponse>>> GetEntriesByGitFeedId(long gitFeedId)
        {
            var result = getEntriesByGitFeedIdRequestValidator.Validate(gitFeedId);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var gitFeedEntries = await gitFeedEntryService.GetListEntriesByGitIdFeed(gitFeedId);

            return Ok(mapper.Map<List<GitFeedEntryResponse>>(gitFeedEntries));
    }
}
