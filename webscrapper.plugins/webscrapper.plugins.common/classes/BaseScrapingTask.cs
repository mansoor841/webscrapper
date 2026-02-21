using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.common.models;

namespace webscrapper.plugins.common.classes;

public abstract class BaseScrapingTask : IScrapingTask, IDisposable
{
    public abstract string TaskName { get; }

    public IWebScraper webScraper { get; set; }

    public virtual async Task<TaskResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await ExecuteCoreAsync(cancellationToken);

            return TaskResult.SuccessResult(result);
        }
        catch (Exception ex)
        {
            return TaskResult.FailureResult(ex.Message);
        }
    }

    protected abstract Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}