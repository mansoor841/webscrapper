namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class MiniTaskResult
{
    public required string TaskName { get; init; }
    public long ElapsedMs { get; set; }

    public Dictionary<string, object> Inputs { get; set; } = new();
    public List<MiniTaskStepResult> Steps { get; init; } = new();
}
