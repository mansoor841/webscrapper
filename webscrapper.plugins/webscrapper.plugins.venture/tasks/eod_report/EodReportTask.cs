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

public class EodReportTask : BaseScrapingTask
{
    public override string TaskName => "venture.eod_report_task";

    public EodReportTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
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
        var html = await webScraper.GetAsync(mainUrl, queryParams, cancellationToken);
        var document = await webScraper.ParseDocumentAsync(html, cancellationToken);
        var jsScriptContent = Utilities.GetJsScript(GetType(), "getReportData.js");
        var result = document.ExecuteScript(jsScriptContent);
        var payments = JsonSerializer.Deserialize<List<PaymentModel>>(result.ToString());

        return payments;
    }
}
