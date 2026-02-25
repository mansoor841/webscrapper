using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.eod_report.shared;

namespace webscrapper.plugins.venture.tasks.eod_report;

public class EodReportTask : BaseScrapingTask
{
    public override string TaskName => "venture.eod_report_task";

    public EodReportTask(IWebScraper _webScraper)
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
            ["ReportStart"] = AppConstants.InputModel.StartDate,
            ["ReportEnd"] = AppConstants.InputModel.EndDate,
            ["DateSelect"] = EodConstants.DateSelect
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";

        var html = await ExecuteStepAsync("Fetch EOD Report Page", async () => 
            await webScraper.GetAsync(mainUrl, queryParams, cancellationToken));

        var document = await ExecuteStepAsync("Parse EOD Document", async () => 
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () => 
            Task.FromResult(Utilities.GetJsScript(GetType(), "getReportData.js")));

        var result = await ExecuteStepAsync("Execute Javascript", () => 
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var payments = await ExecuteStepAsync("Deserialize Payments Data", () => 
            Task.FromResult(JsonSerializer.Deserialize<List<PaymentModel>>(result.ToString())));

        return payments;
    }
}
