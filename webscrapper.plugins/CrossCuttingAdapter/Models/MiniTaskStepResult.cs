namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class MiniTaskStepResult
{
    public required string StepName { get; init; }
    public string? ErrorMessage { get; set; }
    public long ElapsedMs { get; set; }
}
