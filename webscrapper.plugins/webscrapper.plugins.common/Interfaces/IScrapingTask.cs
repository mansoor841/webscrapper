using Scraping.Common.Models;

namespace Scraping.Common.Interfaces;

public interface IScrapingTask
{
    string TaskName { get; }
    Task<ScrapingResult> ExecuteAsync(CancellationToken cancellationToken = default);
}