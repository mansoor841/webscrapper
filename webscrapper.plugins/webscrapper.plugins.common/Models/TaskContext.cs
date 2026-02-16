using Scraping.Common.Interfaces;

namespace Scraping.Common.Models;

public class TaskContext
{
    public required IWebScraper Scraper { get; init; }
    public Dictionary<string, object> SharedData { get; } = new();
    public Dictionary<string, string> Configuration { get; set; } = new();
}