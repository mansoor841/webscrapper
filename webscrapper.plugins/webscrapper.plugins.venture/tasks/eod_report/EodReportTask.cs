using AngleSharp.Js;
using CarrierFeedDownload.CrossCutting.Adapter.Interfaces;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.eod_report.shared;

namespace webscrapper.plugins.venture.tasks.eod_report;

public class EodReportTask : BaseAdapterTask
{
    public override string TaskName => "venture.eod_report_task";

    public EodReportTask(IAdapter _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["ReportStart"] = AppConstants.InputModel.StartDate;
        _inputs["ReportEnd"] = AppConstants.InputModel.EndDate;

        var queryParams = new Dictionary<string, string>
        {
            ["view"] = EodConstants.View,
            ["rpt"] = EodConstants.Rpt,
            ["ReportType"] = EodConstants.ReportType,
            ["ReportStart"] = _inputs["ReportStart"].ToString(),
            ["ReportEnd"] = _inputs["ReportEnd"].ToString(),
            ["DateSelect"] = EodConstants.DateSelect
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";

        var html = await ExecuteStepAsync("Fetch Page", async () => 
            await webScraper.GetAsync(mainUrl, queryParams, cancellationToken));

        var document = await ExecuteStepAsync("Parse Document", async () => 
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () => 
            Task.FromResult(Utilities.GetJsScript(GetType(), "getReportData.js")));

        var result = await ExecuteStepAsync("Execute Javascript", () => 
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var data = await ExecuteStepAsync("Deserialize Data", () => 
            Task.FromResult(JsonSerializer.Deserialize<List<PaymentModel>>(result.ToString())));

        return data;
    }
}
