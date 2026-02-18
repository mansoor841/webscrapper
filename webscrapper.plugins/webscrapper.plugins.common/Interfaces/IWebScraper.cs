using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace webscrapper.plugins.common.interfaces;

public interface IWebScraper : IDisposable
{
    Task<string> GetAsync(string url, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default);
    Task<string> PostAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default);
    Task<string> PostUrlEncodedAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default);
    Task<IHtmlDocument> ParseHtmlAsync(string html, CancellationToken cancellationToken = default);
    Task<IDocument> ParseDocumentAsync(string html, CancellationToken cancellationToken = default);
}