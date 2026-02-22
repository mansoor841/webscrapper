namespace webscrapper.plugins.common.models;

public class TaskResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }
    public List<TaskStepResult> Steps { get; init; } = new();
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    public static TaskResult SuccessResult(object data, List<TaskStepResult> steps) => new() { Success = true, Data = data, Steps = steps };

    public static TaskResult FailureResult(string error, List<TaskStepResult> steps) => new() { Success = false, ErrorMessage = error, Steps = steps };
}
