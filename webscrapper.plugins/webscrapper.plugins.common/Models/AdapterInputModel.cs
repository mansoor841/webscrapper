using CarrierFeedDownload.CrossCutting.Adapter.Shared.Enums;
using webscrapper.plugins.common.classes;

namespace CarrierFeedDownload.CrossCutting.Adapter.Models;

public class AdapterInputModel
{
    public required string BaseUrl { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string AgentCode { get; init; }
    public required string StartDate { get; init; }
    public required string EndDate { get; init; }
    public required BaseJobTypeEnum JobType { get; init; }
    public Dictionary<string, object> OtherInputs { get; set; } = new();


    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string LoginCode { get; set; }
    public string BaseUrl { get; set; }
    public ReportTypeEnum JobRunType { get; set; }
    public JobErrorTypeEnum JobErrorType { get; set; }
    public DateTime? ReportStartDate { get; set; }
    public DateTime? ReportEndDate { get; set; }
    public string ExtraParam1 { get; set; }
    public string ExtraParam2 { get; set; }
    public string ExtraParam3 { get; set; }
    public string ExtraParam4 { get; set; }
    public string ExtraParam5 { get; set; }
    public List<string> PolicyNoList { get; set; }
}
