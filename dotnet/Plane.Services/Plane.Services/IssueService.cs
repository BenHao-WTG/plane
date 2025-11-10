using System.Net.Http.Json;
using Plane.Types.Issues;

namespace Plane.Services;

/// <summary>
/// Service for managing issues
/// Converted from TypeScript SitesIssueService
/// </summary>
public class IssueService
{
    private readonly HttpClient _httpClient;

    public IssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves a paginated list of issues for a specific anchor
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="queryParams">Optional query parameters</param>
    /// <returns>Promise resolving to a paginated list of issues</returns>
    public async Task<PublicIssuesResponse?> ListAsync(string anchor, Dictionary<string, string>? queryParams = null)
    {
        var queryString = queryParams != null 
            ? "?" + string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"))
            : string.Empty;
            
        return await _httpClient.GetFromJsonAsync<PublicIssuesResponse>(
            $"/api/public/anchor/{anchor}/issues/{queryString}"
        );
    }

    /// <summary>
    /// Retrieves details of a specific issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <returns>Promise resolving to the issue details</returns>
    public async Task<Issue?> RetrieveAsync(string anchor, string issueId)
    {
        return await _httpClient.GetFromJsonAsync<Issue>(
            $"/api/public/anchor/{anchor}/issues/{issueId}/"
        );
    }

    /// <summary>
    /// Retrieves the votes associated with a specific issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <returns>Promise resolving to the votes</returns>
    public async Task<List<IssueVote>?> ListVotesAsync(string anchor, string issueId)
    {
        return await _httpClient.GetFromJsonAsync<List<IssueVote>>(
            $"/api/public/anchor/{anchor}/issues/{issueId}/votes/"
        );
    }

    /// <summary>
    /// Creates a new vote for a specific issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <param name="voteData">The vote data</param>
    /// <returns>Promise resolving to the created vote</returns>
    public async Task<IssueVote?> AddVoteAsync(string anchor, string issueId, IssueVote voteData)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"/api/public/anchor/{anchor}/issues/{issueId}/votes/",
            voteData
        );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IssueVote>();
    }

    /// <summary>
    /// Removes a vote from a specific issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <param name="voteId">The vote identifier</param>
    public async Task RemoveVoteAsync(string anchor, string issueId, string voteId)
    {
        var response = await _httpClient.DeleteAsync(
            $"/api/public/anchor/{anchor}/issues/{issueId}/votes/{voteId}/"
        );
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Retrieves comments for a specific issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <returns>List of issue comments</returns>
    public async Task<List<IssueComment>?> ListCommentsAsync(string anchor, string issueId)
    {
        return await _httpClient.GetFromJsonAsync<List<IssueComment>>(
            $"/api/public/anchor/{anchor}/issues/{issueId}/comments/"
        );
    }

    /// <summary>
    /// Creates a new comment on an issue
    /// </summary>
    /// <param name="anchor">The anchor identifier</param>
    /// <param name="issueId">The issue identifier</param>
    /// <param name="commentData">The comment data</param>
    /// <returns>The created comment</returns>
    public async Task<IssueComment?> AddCommentAsync(string anchor, string issueId, IssueComment commentData)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"/api/public/anchor/{anchor}/issues/{issueId}/comments/",
            commentData
        );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IssueComment>();
    }
}

/// <summary>
/// Public issues response with pagination
/// </summary>
public class PublicIssuesResponse
{
    public List<Issue> Results { get; set; } = new();
    public int Count { get; set; }
    public string? NextCursor { get; set; }
    public string? PrevCursor { get; set; }
    public bool NextPageResults { get; set; }
    public bool PrevPageResults { get; set; }
    public int TotalPages { get; set; }
}

/// <summary>
/// Issue vote model
/// </summary>
public class IssueVote
{
    public string Id { get; set; } = string.Empty;
    public string IssueId { get; set; } = string.Empty;
    public string Actor { get; set; } = string.Empty;
    public int Vote { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Issue comment model
/// </summary>
public class IssueComment
{
    public string Id { get; set; } = string.Empty;
    public string IssueId { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string Actor { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
