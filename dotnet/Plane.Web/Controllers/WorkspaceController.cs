using Microsoft.AspNetCore.Mvc;
using Plane.Types;
using Plane.Services;

namespace Plane.Web.Controllers;

/// <summary>
/// Workspace controller - converts from apps/web/app/(all)/[workspaceSlug]/(projects)/
/// Handles workspace-level routes and dashboard
/// </summary>
public class WorkspaceController : Controller
{
    private readonly ILogger<WorkspaceController> _logger;

    public WorkspaceController(ILogger<WorkspaceController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Workspace dashboard page
    /// Route: /{workspaceSlug}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}")]
    public IActionResult Index(string workspaceSlug)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["Title"] = "Workspace Dashboard";
        return View();
    }

    /// <summary>
    /// Active cycles page
    /// Route: /{workspaceSlug}/active-cycles
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/active-cycles/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/active-cycles")]
    public IActionResult ActiveCycles(string workspaceSlug)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["Title"] = "Active Cycles";
        return View();
    }

    /// <summary>
    /// Analytics page
    /// Route: /{workspaceSlug}/analytics/{tabId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/analytics/[tabId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/analytics/{tabId}")]
    public IActionResult Analytics(string workspaceSlug, string tabId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["TabId"] = tabId;
        ViewData["Title"] = $"Analytics - {tabId}";
        return View();
    }

    /// <summary>
    /// Browse work items page
    /// Route: /{workspaceSlug}/browse/{workItem}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/browse/[workItem]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/browse/{workItem}")]
    public IActionResult Browse(string workspaceSlug, string workItem)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["WorkItem"] = workItem;
        ViewData["Title"] = $"Browse - {workItem}";
        return View();
    }

    /// <summary>
    /// Drafts page
    /// Route: /{workspaceSlug}/drafts
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/drafts/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/drafts")]
    public IActionResult Drafts(string workspaceSlug)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["Title"] = "Drafts";
        return View();
    }

    /// <summary>
    /// Notifications page
    /// Route: /{workspaceSlug}/notifications
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/notifications/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/notifications")]
    public IActionResult Notifications(string workspaceSlug)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["Title"] = "Notifications";
        return View();
    }
}
