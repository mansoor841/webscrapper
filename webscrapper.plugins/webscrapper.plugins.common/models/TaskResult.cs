namespace webscrapper.plugins.common.models;

public class TaskResult
{
    public string? TaskName { get; init; }
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }
    public List<TaskStepResult> Steps { get; init; } = new();
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    public MiniTaskResult ToMini() => new()
    {
        TaskName = TaskName ?? "UnknownTask",
        Steps = Steps.Select(s => new MiniTaskStepResult
        {
            StepName = s.StepName,
            ErrorMessage = s.ErrorMessage,
            ElapsedMs = s.ElapsedMs
        }).ToList()
    };

    public static TaskResult SuccessResult(string taskName, object data, List<TaskStepResult> steps) => new() { TaskName = taskName, Success = true, Data = data, Steps = steps };

    public static TaskResult FailureResult(string taskName, string error, List<TaskStepResult> steps) => new() { TaskName = taskName, Success = false, ErrorMessage = error, Steps = steps };
}
