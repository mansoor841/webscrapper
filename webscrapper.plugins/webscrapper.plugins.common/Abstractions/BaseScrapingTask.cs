using Scraping.Common.Interfaces;
using Scraping.Common.Models;

namespace Scraping.Common.Abstractions;

public abstract class BaseScrapingTask : IScrapingTask
{
    public abstract TaskContext Context { get; set; }
    public abstract string TaskName { get; }

    public virtual async Task<ScrapingResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await ExecuteCoreAsync(cancellationToken);

            return ScrapingResult.SuccessResult(result);
        }
        catch (Exception ex)
        {
            return ScrapingResult.FailureResult(ex.Message);
        }
    }

    protected abstract Task<object> ExecuteCoreAsync(CancellationToken cancellationToken);
}