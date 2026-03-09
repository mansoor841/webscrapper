using CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class AdapterOutput
{
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public object? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public long ElapsedMs { get; set; }

    public JobErrorType JobErrorType { get; init; }    
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    public List<MiniTaskResult> TaskResults { get; set; } = new();

}
