using webscrapper.plugins.common.models;

namespace webscrapper.plugins.common.interfaces;

public interface IScrapingTask
{
    string TaskName { get; }
    Task<TaskResult> ExecuteAsync(CancellationToken cancellationToken = default);
}