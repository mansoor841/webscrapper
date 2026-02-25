using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.common.models;

namespace webscrapper.plugins.common.classes;

public abstract class BaseScrapingTask : IScrapingTask, IDisposable
{
    public abstract string TaskName { get; }

    public IWebScraper webScraper { get; set; }

    protected List<TaskStepResult> _steps = new();

    public virtual async Task<TaskResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        long start = Environment.TickCount64;
        
        try
        {
            var result = await ExecuteCoreAsync(cancellationToken);
            long elapsed = Environment.TickCount64 - start;

            return TaskResult.SuccessResult(TaskName, result, _steps, elapsed);
        }
        catch (Exception ex)
        {
            long elapsed = Environment.TickCount64 - start;
            
            return TaskResult.FailureResult(TaskName, ex.Message, _steps, elapsed);
        }
    }

    protected abstract Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default);

    protected async Task<T> ExecuteStepAsync<T>(string stepName, Func<Task<T>> action)
    {
        var stepResult = new TaskStepResult { StepName = stepName };
        long start = Environment.TickCount64;

        try
        {
            var result = await action();

            return result;
        }
        catch (Exception ex)
        {
            stepResult.ErrorMessage = ex.Message;

            throw;
        }
        finally
        {
            stepResult.ElapsedMs = Environment.TickCount64 - start;

            _steps.Add(stepResult);
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}