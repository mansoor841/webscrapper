using CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;

namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class AdapterInput
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string LoginCode { get; init; }
    public required string BaseUrl { get; init; }
    public DateTime ReportStartDate { get; init; }
    public DateTime ReportEndDate { get; init; }
    public required string ExtraParam1 { get; init; }
    public required string ExtraParam2 { get; init; }
    public required string ExtraParam3 { get; init; }
    public required string ExtraParam4 { get; init; }
    public required string ExtraParam5 { get; init; }

    public ReportType JobRunType { get; init; }

    public List<string> PolicyNoList { get; set; } = new();
}
