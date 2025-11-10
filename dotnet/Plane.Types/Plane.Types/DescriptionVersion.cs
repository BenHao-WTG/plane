namespace Plane.Types;

/// <summary>
/// Description version model
/// </summary>
public class DescriptionVersion
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string Id { get; set; } = string.Empty;
    public DateTime LastSavedAt { get; set; }
    public string OwnedBy { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

/// <summary>
/// Description version with detailed content
/// </summary>
public class DescriptionVersionDetails : DescriptionVersion
{
    public string? DescriptionBinary { get; set; }
    public string? DescriptionHtml { get; set; }
    public object? DescriptionJson { get; set; }
    public string? DescriptionStripped { get; set; }
}

/// <summary>
/// Paginated response for description versions list
/// </summary>
public class DescriptionVersionsListResponse
{
    public string Cursor { get; set; } = string.Empty;
    public string? NextCursor { get; set; }
    public bool NextPageResults { get; set; }
    public int PageCount { get; set; }
    public string? PrevCursor { get; set; }
    public bool PrevPageResults { get; set; }
    public List<DescriptionVersion> Results { get; set; } = new();
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
}
