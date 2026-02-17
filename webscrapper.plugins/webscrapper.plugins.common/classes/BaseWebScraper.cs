using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Flurl.Http;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.common.models;

namespace webscrapper.plugins.common.classes;

public abstract class BaseWebScraper : IWebScraper, IDisposable
{
    protected CookieJar _cookieJar = new();
    protected HtmlParser _htmlParser { get; } = new();
    protected IBrowsingContext context { get; set; }

    public BaseWebScraper()
    {
        var config = Configuration.Default
            .WithJs()
            .WithDefaultLoader();

        context = BrowsingContext.New(config);
    }

    public async Task<string> GetAsync(string url, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        var request = url.WithCookies(_cookieJar);

        if (queryParams != null)
        {
            request = request.SetQueryParams(queryParams);
        }

        return await request.GetStringAsync(cancellationToken: cancellationToken);
    }

    public async Task<string> PostAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default)
    {
        return await url
            .WithCookies(_cookieJar)
            .PostJsonAsync(formData, cancellationToken: cancellationToken)
            .ReceiveString();
    }

    public async Task<string> PostUrlEncodedAsync(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default)
    {
        var filteredData = formData
            .Where(p => !string.IsNullOrEmpty(p.Key) && !string.IsNullOrEmpty(p.Value))
            .ToDictionary(x => x.Key, x => x.Value);

        return await url
            .WithCookies(out _cookieJar)
            .PostUrlEncodedAsync(filteredData, cancellationToken: cancellationToken)
            .ReceiveString();
    }

    public async Task<IHtmlDocument> ParseHtmlAsync(string html, CancellationToken cancellationToken = default)
    {
        //return await context.OpenAsync(req => req.Content(html));

        return await _htmlParser.ParseDocumentAsync(html, cancellationToken);
    }

    public abstract Task<PluginOutput> RunAsync(PluginInput inputModel, CancellationToken cancellationToken = default);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}