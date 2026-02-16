namespace Scraping.Common.Models;

public record Credentials
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public Dictionary<string, string> AdditionalFields { get; init; } = new();
}