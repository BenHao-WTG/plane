namespace Plane.Types;

/// <summary>
/// Start of the week enumeration
/// </summary>
public enum StartOfTheWeek
{
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6
}

/// <summary>
/// Login mediums
/// </summary>
public enum LoginMediums
{
    Email,
    MagicCode,
    Github,
    Gitlab,
    Google
}

/// <summary>
/// User theme settings
/// </summary>
public class UserTheme
{
    public string? Text { get; set; }
    public string? Theme { get; set; }
    public string? Palette { get; set; }
    public string? Primary { get; set; }
    public string? Background { get; set; }
    public bool? DarkPalette { get; set; }
    public string? SidebarText { get; set; }
    public string? SidebarBackground { get; set; }
}

/// <summary>
/// Lightweight user information
/// </summary>
public class UserLite
{
    public string AvatarUrl { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public bool IsBot { get; set; }
    public string LastName { get; set; } = string.Empty;
    public DateTime? JoiningDate { get; set; }
}

/// <summary>
/// Full user information
/// </summary>
public class User : UserLite
{
    public string? CoverImageAsset { get; set; }
    public string? CoverImage { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime DateJoined { get; set; }
    public new string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsPasswordAutoset { get; set; }
    public bool IsTourCompleted { get; set; }
    public string? MobileNumber { get; set; }
    public string LastWorkspaceId { get; set; } = string.Empty;
    public string UserTimezone { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public LoginMediums LastLoginMedium { get; set; }
    public UserTheme Theme { get; set; } = new();
}

/// <summary>
/// User account information
/// </summary>
public class UserAccount
{
    public string ProviderAccountId { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// User profile information
/// </summary>
public class UserProfile
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? Role { get; set; }
    public string? LastWorkspaceId { get; set; }
    public UserTheme Theme { get; set; } = new();
    public string OnboardingStep { get; set; } = string.Empty;
    public bool IsOnboarded { get; set; }
    public bool IsTourCompleted { get; set; }
    public string? UseCase { get; set; }
    public string? BillingAddressCountry { get; set; }
    public string? BillingAddress { get; set; }
    public bool HasBillingAddress { get; set; }
    public bool HasMarketingEmailConsent { get; set; }
    public string Language { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public StartOfTheWeek StartOfTheWeek { get; set; }
}

/// <summary>
/// Instance admin status
/// </summary>
public class InstanceAdminStatus
{
    public bool IsInstanceAdmin { get; set; }
}

/// <summary>
/// User workspace settings
/// </summary>
public class UserWorkspaceSettings
{
    public string? LastWorkspaceId { get; set; }
    public string? LastWorkspaceSlug { get; set; }
    public string? LastWorkspaceName { get; set; }
    public string? LastWorkspaceLogo { get; set; }
    public string? FallbackWorkspaceId { get; set; }
    public string? FallbackWorkspaceSlug { get; set; }
    public int? Invites { get; set; }
}

/// <summary>
/// User settings
/// </summary>
public class UserSettings
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public UserWorkspaceSettings Workspace { get; set; } = new();
}

/// <summary>
/// User activity information
/// </summary>
public class UserActivity
{
    public DateTime CreatedDate { get; set; }
    public int ActivityCount { get; set; }
}

/// <summary>
/// User priority distribution
/// </summary>
public class UserPriorityDistribution
{
    public string Priority { get; set; } = string.Empty;
    public int PriorityCount { get; set; }
}

/// <summary>
/// User state distribution
/// </summary>
public class UserStateDistribution
{
    public string StateGroup { get; set; } = string.Empty;
    public int StateCount { get; set; }
}

/// <summary>
/// User activity response with pagination
/// </summary>
public class UserActivityResponse
{
    public int Count { get; set; }
    public object? ExtraStats { get; set; }
    public string NextCursor { get; set; } = string.Empty;
    public bool NextPageResults { get; set; }
    public string PrevCursor { get; set; } = string.Empty;
    public bool PrevPageResults { get; set; }
    public List<object> Results { get; set; } = new();
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
}

/// <summary>
/// User authorization information
/// </summary>
public class UserAuth
{
    public bool IsMember { get; set; }
    public bool IsOwner { get; set; }
    public bool IsGuest { get; set; }
}
