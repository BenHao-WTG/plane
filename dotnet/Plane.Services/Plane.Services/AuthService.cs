using System.Net.Http.Json;
using Plane.Types;

namespace Plane.Services;

/// <summary>
/// Service for authentication-related operations
/// Converted from TypeScript auth service
/// </summary>
public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Check email for authentication method
    /// </summary>
    /// <param name="email">User email address</param>
    /// <returns>Email check response indicating authentication method</returns>
    public async Task<EmailCheckResponse?> CheckEmailAsync(string email)
    {
        var data = new EmailCheckData { Email = email };
        var response = await _httpClient.PostAsJsonAsync("/api/auth/email-check", data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmailCheckResponse>();
    }

    /// <summary>
    /// Sign in with magic code
    /// </summary>
    /// <param name="signInData">Magic sign-in data</param>
    /// <returns>Login token response</returns>
    public async Task<LoginTokenResponse?> SignInWithMagicCodeAsync(MagicSignInData signInData)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/magic-sign-in", signInData);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LoginTokenResponse>();
    }

    /// <summary>
    /// Sign in with password
    /// </summary>
    /// <param name="signInData">Password sign-in data</param>
    /// <returns>Login token response</returns>
    public async Task<LoginTokenResponse?> SignInWithPasswordAsync(PasswordSignInData signInData)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/password-sign-in", signInData);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LoginTokenResponse>();
    }

    /// <summary>
    /// Get CSRF token
    /// </summary>
    /// <returns>CSRF token data</returns>
    public async Task<CsrfTokenData?> GetCsrfTokenAsync()
    {
        return await _httpClient.GetFromJsonAsync<CsrfTokenData>("/api/auth/csrf-token");
    }

    /// <summary>
    /// Sign out the current user
    /// </summary>
    public async Task SignOutAsync()
    {
        var response = await _httpClient.PostAsync("/api/auth/sign-out", null);
        response.EnsureSuccessStatusCode();
    }
}
