namespace webscrapper.plugins.common.models;

public class TaskStepResult
{
    public required string StepName { get; init; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public object? Data { get; set; }
    public long ElapsedMs { get; set; }
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;
}
