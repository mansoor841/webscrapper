using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Scraping.Common.Abstractions;

public abstract class BaseHtmlScrapingTask : BaseScrapingTask
{
    protected HtmlParser HtmlParser { get; } = new();

    protected async Task<IHtmlDocument> ParseHtmlAsync(string html)
    {
        return await HtmlParser.ParseDocumentAsync(html);
    }
}