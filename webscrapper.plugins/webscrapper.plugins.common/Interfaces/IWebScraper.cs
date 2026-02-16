namespace Scraping.Common.Interfaces;

public interface IWebScraper : IDisposable
{
    Task<string> GetAsync(string url, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default);
    Task<string> PostAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default);
    Task<string> PostUrlEncodedAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default);
}