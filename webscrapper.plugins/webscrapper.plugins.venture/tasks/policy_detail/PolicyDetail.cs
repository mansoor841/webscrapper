using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.eod_report.shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webscrapper.plugins.venture.tasks.eod_report;

public class PolicyDetail : BaseScrapingTask
{
    public override string TaskName => "venture.policy_detail_task";

    public PolicyDetail(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        "SearchValue=VGAO-04172-000&lookuptype=Policy&view=quicksearch&DisplayAcctFrame=Yes&MatchRule=AnyPart&doubleclicksearchcaution=1"

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

        var jsScriptContent = await ExecuteStepAsync("Load Javascript Extractor", () =>
            Task.FromResult(Utilities.GetJsScript(GetType(), "getReportData.js")));

        var result = await ExecuteStepAsync("Execute Report Extractor", () =>
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var payments = await ExecuteStepAsync("Deserialize Payments Data", () =>
            Task.FromResult(JsonSerializer.Deserialize<List<PaymentModel>>(result.ToString())));

        return payments;
    }
}
