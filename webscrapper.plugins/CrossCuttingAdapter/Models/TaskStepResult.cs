namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class TaskStepResult
{
    public required string StepName { get; init; }
    public string? ErrorMessage { get; set; }
    public long ElapsedMs { get; set; }
}
