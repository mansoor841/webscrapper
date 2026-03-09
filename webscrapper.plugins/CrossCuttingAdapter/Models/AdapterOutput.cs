using CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class AdapterOutput
{
    public string? RawData { get; set; }
    public string? ReportStartDate { get; init; }
    public string? ReportEndDate { get; init; }
    public long JobExecutionTime { get; set; }
    public object? Data { get; set; }


    public string? ErrorMessage { get; set; }
    

    public JobErrorType JobErrorType { get; init; }    
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;

    

    public List<MiniTaskResult> TaskResults { get; set; } = new();

}
