using Microsoft.AspNetCore.Mvc;
using Plane.Types;
using Plane.Services;
using Plane.Types.Issues;

namespace Plane.Web.Controllers;

/// <summary>
/// Project controller - converts from apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/
/// Handles project-level routes for issues, cycles, modules
/// </summary>
public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IssueService? _issueService;

    public ProjectController(
        ILogger<ProjectController> logger,
        IssueService? issueService = null)
    {
        _logger = logger;
        _issueService = issueService;
    }

    /// <summary>
    /// Project issues list page
    /// Route: /{workspaceSlug}/projects/{projectId}/issues
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/issues/(list)/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/issues")]
    public IActionResult Issues(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Issues";
        return View();
    }

    /// <summary>
    /// Project issue detail page
    /// Route: /{workspaceSlug}/projects/{projectId}/issues/{issueId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/issues/(detail)/[issueId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/issues/{issueId}")]
    public IActionResult IssueDetail(string workspaceSlug, string projectId, string issueId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["IssueId"] = issueId;
        ViewData["Title"] = $"Issue - {issueId}";
        return View();
    }

    /// <summary>
    /// Project cycles list page
    /// Route: /{workspaceSlug}/projects/{projectId}/cycles
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/cycles/(list)/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/cycles")]
    public IActionResult Cycles(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Cycles";
        return View();
    }

    /// <summary>
    /// Project cycle detail page
    /// Route: /{workspaceSlug}/projects/{projectId}/cycles/{cycleId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/cycles/(detail)/[cycleId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/cycles/{cycleId}")]
    public IActionResult CycleDetail(string workspaceSlug, string projectId, string cycleId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["CycleId"] = cycleId;
        ViewData["Title"] = $"Cycle - {cycleId}";
        return View();
    }

    /// <summary>
    /// Project modules list page
    /// Route: /{workspaceSlug}/projects/{projectId}/modules
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/modules/(list)/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/modules")]
    public IActionResult Modules(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Modules";
        return View();
    }

    /// <summary>
    /// Project module detail page
    /// Route: /{workspaceSlug}/projects/{projectId}/modules/{moduleId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/modules/(detail)/[moduleId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/modules/{moduleId}")]
    public IActionResult ModuleDetail(string workspaceSlug, string projectId, string moduleId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["ModuleId"] = moduleId;
        ViewData["Title"] = $"Module - {moduleId}";
        return View();
    }

    /// <summary>
    /// Project intake page
    /// Route: /{workspaceSlug}/projects/{projectId}/intake
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/intake/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/intake")]
    public IActionResult Intake(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Intake";
        return View();
    }

    /// <summary>
    /// Project archived issues page
    /// Route: /{workspaceSlug}/projects/{projectId}/archives/issues
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/issues/(list)/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/archives/issues")]
    public IActionResult ArchivedIssues(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Archived Issues";
        return View();
    }

    /// <summary>
    /// Project archived cycles page
    /// Route: /{workspaceSlug}/projects/{projectId}/archives/cycles
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/cycles/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/archives/cycles")]
    public IActionResult ArchivedCycles(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Archived Cycles";
        return View();
    }

    /// <summary>
    /// Project archived modules page
    /// Route: /{workspaceSlug}/projects/{projectId}/archives/modules
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/modules/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/projects/{projectId}/archives/modules")]
    public IActionResult ArchivedModules(string workspaceSlug, string projectId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["ProjectId"] = projectId;
        ViewData["Title"] = "Archived Modules";
        return View();
    }
}
