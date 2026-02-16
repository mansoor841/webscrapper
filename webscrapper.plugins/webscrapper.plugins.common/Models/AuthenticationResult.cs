namespace Scraping.Common.Models;

public class AuthenticationResult
{
    public bool IsAuthenticated { get; init; }
    public string? Token { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public string? ErrorMessage { get; init; }
}