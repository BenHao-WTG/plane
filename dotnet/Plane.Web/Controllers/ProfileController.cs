using Microsoft.AspNetCore.Mvc;
using Plane.Types;

namespace Plane.Web.Controllers;

/// <summary>
/// Profile controller - converts from apps/web/app/(all)/[workspaceSlug]/(projects)/profile/[userId]/
/// Handles user profile routes
/// </summary>
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// User profile page
    /// Route: /{workspaceSlug}/profile/{userId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/profile/[userId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/profile/{userId}")]
    public IActionResult Index(string workspaceSlug, string userId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["UserId"] = userId;
        ViewData["Title"] = "Profile";
        return View();
    }

    /// <summary>
    /// User activity page
    /// Route: /{workspaceSlug}/profile/{userId}/activity
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/profile/[userId]/activity/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/profile/{userId}/activity")]
    public IActionResult Activity(string workspaceSlug, string userId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["UserId"] = userId;
        ViewData["Title"] = "Activity";
        return View();
    }

    /// <summary>
    /// User profile view page with specific view ID
    /// Route: /{workspaceSlug}/profile/{userId}/{profileViewId}
    /// Converted from: apps/web/app/(all)/[workspaceSlug]/(projects)/profile/[userId]/[profileViewId]/page.tsx
    /// </summary>
    [HttpGet("{workspaceSlug}/profile/{userId}/{profileViewId}")]
    public IActionResult ProfileView(string workspaceSlug, string userId, string profileViewId)
    {
        ViewData["WorkspaceSlug"] = workspaceSlug;
        ViewData["UserId"] = userId;
        ViewData["ProfileViewId"] = profileViewId;
        ViewData["Title"] = $"Profile - {profileViewId}";
        return View();
    }
}
