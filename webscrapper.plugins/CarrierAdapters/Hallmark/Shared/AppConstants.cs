using CarrierFeedDownload.CrossCutting.Adapter.Models;

namespace Adapter.Hallmark.Shared;

public class AppConstants
{
    public const string LoginPath = "/test/root/logon/index.cfm";
    public const string MainPath = "/test/root/main.cfm";

    public static AdapterInput Input { get; set; }
}