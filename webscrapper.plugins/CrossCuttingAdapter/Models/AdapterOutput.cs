using CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class AdapterOutput
{
    public string? RawData { get; set; }
    public string? ReportStartDate { get; init; }
    public string? ReportEndDate { get; init; }
    public long JobExecutionTime { get; set; }
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;
    public string? ErrorMessage { get; set; }
    public object? Data { get; set; }

    public JobErrorType JobErrorType { get; init; }

    public List<MiniTaskResult> TaskResults { get; set; } = new();

}
