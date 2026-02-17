namespace webscrapper.plugins.common.models;

public class TaskResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    public static TaskResult SuccessResult(object data) => new() { Success = true, Data = data };

    public static TaskResult FailureResult(string error) => new() { Success = false, ErrorMessage = error };
}
