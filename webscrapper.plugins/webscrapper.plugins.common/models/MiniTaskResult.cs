namespace webscrapper.plugins.common.models;

public class MiniTaskResult
{
    public required string TaskName { get; init; }
    public long ElapsedMs { get; set; }
    public Dictionary<string, object> Inputs { get; set; } = new();
    public List<MiniTaskStepResult> Steps { get; init; } = new();
}

public class MiniTaskStepResult
{
    public required string StepName { get; init; }
    public string? ErrorMessage { get; set; }
    public long ElapsedMs { get; set; }
}
