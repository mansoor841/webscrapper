namespace webscrapper.plugins.common.models;

public class TaskResult
{
    public string? TaskName { get; init; }
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }
    public List<TaskStepResult> Steps { get; init; } = new();
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;
    public long ElapsedMs { get; init; }
    public Dictionary<string, object> Inputs { get; set; } = new();

    public MiniTaskResult ToMini() => new()
    {
        TaskName = TaskName ?? "UnknownTask",
        ElapsedMs = ElapsedMs,
        Inputs = Inputs,
        Steps = Steps.Select(s => new MiniTaskStepResult
        {
            StepName = s.StepName,
            ErrorMessage = s.ErrorMessage,
            ElapsedMs = s.ElapsedMs
        }).ToList()
    };

    public static TaskResult SuccessResult(string taskName, object data, List<TaskStepResult> steps, long elapsedMs, Dictionary<string, object> inputs) => new() { TaskName = taskName, Success = true, Data = data, Steps = steps, ElapsedMs = elapsedMs, Inputs = inputs };

    public static TaskResult FailureResult(string taskName, string error, List<TaskStepResult> steps, long elapsedMs, Dictionary<string, object> inputs) => new() { TaskName = taskName, Success = false, ErrorMessage = error, Steps = steps, ElapsedMs = elapsedMs, Inputs = inputs };
}
