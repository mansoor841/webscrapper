namespace Scraping.Common.Models;

public class ScrapingResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    public static ScrapingResult SuccessResult(object data) => new() { Success = true, Data = data };

    public static ScrapingResult FailureResult(string error) => new() { Success = false, ErrorMessage = error };
}