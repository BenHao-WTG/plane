namespace Plane.Types.Issues;

/// <summary>
/// Base issue model
/// </summary>
public class BaseIssue
{
    public string Id { get; set; } = string.Empty;
    public int SequenceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double SortOrder { get; set; }

    public string? StateId { get; set; }
    public IssuePriorities? Priority { get; set; }
    public List<string> LabelIds { get; set; } = new();
    public List<string> AssigneeIds { get; set; } = new();
    public string? EstimatePoint { get; set; }

    public int SubIssuesCount { get; set; }
    public int AttachmentCount { get; set; }
    public int LinkCount { get; set; }

    public string? ProjectId { get; set; }
    public string? ParentId { get; set; }
    public string? CycleId { get; set; }
    public List<string>? ModuleIds { get; set; }
    public string? TypeId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? TargetDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;

    public bool IsDraft { get; set; }
    public bool? IsEpic { get; set; }
    public bool? IsIntake { get; set; }
}

/// <summary>
/// Issue relation model
/// </summary>
public class IssueRelation
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public IssueRelationTypes RelationType { get; set; }
    public int SequenceId { get; set; }
}

/// <summary>
/// Issue attachment model
/// </summary>
public class IssueAttachment
{
    public string Id { get; set; } = string.Empty;
    public string Asset { get; set; } = string.Empty;
    public string? Attributes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string IssueId { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Issue link model
/// </summary>
public class IssueLink
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string IssueId { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Issue reaction model
/// </summary>
public class IssueReaction
{
    public string Id { get; set; } = string.Empty;
    public string Reaction { get; set; } = string.Empty;
    public string IssueId { get; set; } = string.Empty;
    public string Actor { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Full issue model extending base issue
/// </summary>
public class Issue : BaseIssue
{
    public string? DescriptionHtml { get; set; }
    public bool? IsSubscribed { get; set; }
    public BaseIssue? Parent { get; set; }
    public List<IssueReaction>? IssueReactions { get; set; }
    public List<IssueAttachment>? IssueAttachments { get; set; }
    public List<IssueLink>? IssueLinks { get; set; }
    public List<IssueRelation>? IssueRelation { get; set; }
    public List<IssueRelation>? IssueRelated { get; set; }
    
    /// <summary>
    /// Temporary ID for optimistic updates (not part of API response)
    /// </summary>
    public string? TempId { get; set; }
    
    /// <summary>
    /// Source issue ID for cloning property values (not part of API response)
    /// </summary>
    public string? SourceIssueId { get; set; }
    
    public string? StateGroup { get; set; }
}

/// <summary>
/// Issue map dictionary
/// </summary>
public class IssueMap : Dictionary<string, Issue>
{
}
