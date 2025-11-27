namespace Plane.Types.Issues;

/// <summary>
/// Issue priorities enumeration
/// </summary>
public enum IssuePriorities
{
    None,
    Urgent,
    High,
    Medium,
    Low
}

/// <summary>
/// Issue layout types enumeration
/// </summary>
public enum IssueLayoutTypes
{
    List,
    Kanban,
    Calendar,
    GanttChart,
    Spreadsheet
}

/// <summary>
/// Issue service type enumeration
/// </summary>
public enum IssueServiceType
{
    Issues,
    Epics,
    WorkItems
}

/// <summary>
/// Issues store type enumeration
/// </summary>
public enum IssuesStoreType
{
    Global,
    Profile,
    Team,
    Project,
    Cycle,
    Module,
    TeamView,
    ProjectView,
    Archived,
    Default,
    WorkspaceDraft,
    Epic,
    TeamProjectWorkItems
}

/// <summary>
/// Loader type
/// </summary>
public enum LoaderType
{
    InitLoader,
    Mutation,
    Pagination,
    Loaded,
    Undefined
}

/// <summary>
/// Issue relation types
/// </summary>
public enum IssueRelationTypes
{
    Related,
    Blocking,
    BlockedBy,
    Duplicate
}

/// <summary>
/// Grouped issues dictionary
/// </summary>
public class GroupedIssues : Dictionary<string, List<string>>
{
}

/// <summary>
/// Sub-grouped issues dictionary
/// </summary>
public class SubGroupedIssues : Dictionary<string, GroupedIssues>
{
}

/// <summary>
/// Pagination data
/// </summary>
public class PaginationData
{
    public string NextCursor { get; set; } = string.Empty;
    public string PrevCursor { get; set; } = string.Empty;
    public bool NextPageResults { get; set; }
}

/// <summary>
/// Issue pagination data dictionary
/// </summary>
public class IssuePaginationData : Dictionary<string, PaginationData>
{
}

/// <summary>
/// Grouped issue count dictionary
/// </summary>
public class GroupedIssueCount : Dictionary<string, int>
{
}

/// <summary>
/// Un-grouped issues list
/// </summary>
public class UnGroupedIssues : List<string>
{
}
