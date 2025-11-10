using Microsoft.AspNetCore.Mvc;
using Plane.Types.Issues;
using Plane.Services;

namespace Plane.Web.Controllers.Api;

/// <summary>
/// Issues API Controller
/// Converted from: packages/services/src/issue/sites-issue.service.ts
/// Provides REST API endpoints for issue operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly ILogger<IssuesController> _logger;
    private readonly IssueService _issueService;

    public IssuesController(
        ILogger<IssuesController> logger,
        IssueService issueService)
    {
        _logger = logger;
        _issueService = issueService;
    }

    /// <summary>
    /// Get paginated list of issues
    /// GET: api/issues/{workspaceSlug}/{projectId}
    /// </summary>
    [HttpGet("{workspaceSlug}/{projectId}")]
    [ProducesResponseType(typeof(PublicIssuesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIssues(
        string workspaceSlug,
        string projectId,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 25)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["per_page"] = perPage.ToString()
            };

            var result = await _issueService.ListAsync(workspaceSlug, queryParams);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving issues for workspace {WorkspaceSlug}, project {ProjectId}", 
                workspaceSlug, projectId);
            return StatusCode(500, new { error = "Failed to retrieve issues" });
        }
    }

    /// <summary>
    /// Get issue details by ID
    /// GET: api/issues/{workspaceSlug}/{projectId}/{issueId}
    /// </summary>
    [HttpGet("{workspaceSlug}/{projectId}/{issueId}")]
    [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIssue(
        string workspaceSlug,
        string projectId,
        string issueId)
    {
        try
        {
            var issue = await _issueService.RetrieveAsync(workspaceSlug, issueId);
            if (issue == null)
            {
                return NotFound(new { error = $"Issue {issueId} not found" });
            }
            return Ok(issue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving issue {IssueId}", issueId);
            return StatusCode(500, new { error = "Failed to retrieve issue" });
        }
    }

    /// <summary>
    /// Get votes for an issue
    /// GET: api/issues/{workspaceSlug}/{projectId}/{issueId}/votes
    /// </summary>
    [HttpGet("{workspaceSlug}/{projectId}/{issueId}/votes")]
    [ProducesResponseType(typeof(List<IssueVote>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVotes(
        string workspaceSlug,
        string projectId,
        string issueId)
    {
        try
        {
            var votes = await _issueService.ListVotesAsync(workspaceSlug, issueId);
            return Ok(votes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving votes for issue {IssueId}", issueId);
            return StatusCode(500, new { error = "Failed to retrieve votes" });
        }
    }

    /// <summary>
    /// Add a vote to an issue
    /// POST: api/issues/{workspaceSlug}/{projectId}/{issueId}/votes
    /// </summary>
    [HttpPost("{workspaceSlug}/{projectId}/{issueId}/votes")]
    [ProducesResponseType(typeof(IssueVote), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddVote(
        string workspaceSlug,
        string projectId,
        string issueId,
        [FromBody] IssueVote voteData)
    {
        try
        {
            var vote = await _issueService.AddVoteAsync(workspaceSlug, issueId, voteData);
            return CreatedAtAction(nameof(GetVotes), 
                new { workspaceSlug, projectId, issueId }, 
                vote);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding vote to issue {IssueId}", issueId);
            return StatusCode(500, new { error = "Failed to add vote" });
        }
    }

    /// <summary>
    /// Remove a vote from an issue
    /// DELETE: api/issues/{workspaceSlug}/{projectId}/{issueId}/votes/{voteId}
    /// </summary>
    [HttpDelete("{workspaceSlug}/{projectId}/{issueId}/votes/{voteId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveVote(
        string workspaceSlug,
        string projectId,
        string issueId,
        string voteId)
    {
        try
        {
            await _issueService.RemoveVoteAsync(workspaceSlug, issueId, voteId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing vote {VoteId} from issue {IssueId}", voteId, issueId);
            return StatusCode(500, new { error = "Failed to remove vote" });
        }
    }

    /// <summary>
    /// Get comments for an issue
    /// GET: api/issues/{workspaceSlug}/{projectId}/{issueId}/comments
    /// </summary>
    [HttpGet("{workspaceSlug}/{projectId}/{issueId}/comments")]
    [ProducesResponseType(typeof(List<IssueComment>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetComments(
        string workspaceSlug,
        string projectId,
        string issueId)
    {
        try
        {
            var comments = await _issueService.ListCommentsAsync(workspaceSlug, issueId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving comments for issue {IssueId}", issueId);
            return StatusCode(500, new { error = "Failed to retrieve comments" });
        }
    }

    /// <summary>
    /// Add a comment to an issue
    /// POST: api/issues/{workspaceSlug}/{projectId}/{issueId}/comments
    /// </summary>
    [HttpPost("{workspaceSlug}/{projectId}/{issueId}/comments")]
    [ProducesResponseType(typeof(IssueComment), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddComment(
        string workspaceSlug,
        string projectId,
        string issueId,
        [FromBody] IssueComment commentData)
    {
        try
        {
            var comment = await _issueService.AddCommentAsync(workspaceSlug, issueId, commentData);
            return CreatedAtAction(nameof(GetComments), 
                new { workspaceSlug, projectId, issueId }, 
                comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding comment to issue {IssueId}", issueId);
            return StatusCode(500, new { error = "Failed to add comment" });
        }
    }
}
