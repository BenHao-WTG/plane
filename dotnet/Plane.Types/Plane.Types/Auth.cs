namespace Plane.Types;

/// <summary>
/// Email check types for authentication
/// </summary>
public enum EmailCheckTypes
{
    MagicCode,
    Password
}

/// <summary>
/// Email check data request
/// </summary>
public class EmailCheckData
{
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Email check response
/// </summary>
public class EmailCheckResponse
{
    public string Status { get; set; } = string.Empty; // "MAGIC_CODE" or "CREDENTIAL"
    public bool Existing { get; set; }
    public bool IsPasswordAutoset { get; set; }
}

/// <summary>
/// Login token response
/// </summary>
public class LoginTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Magic sign-in data
/// </summary>
public class MagicSignInData
{
    public string Email { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

/// <summary>
/// Password sign-in data
/// </summary>
public class PasswordSignInData
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// CSRF token data
/// </summary>
public class CsrfTokenData
{
    public string CsrfToken { get; set; } = string.Empty;
}
