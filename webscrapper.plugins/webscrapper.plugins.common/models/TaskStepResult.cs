namespace webscrapper.plugins.common.models;

public class TaskStepResult
{
    public required string StepName { get; init; }
    public string? ErrorMessage { get; set; }
    public long ElapsedMs { get; set; }
}
