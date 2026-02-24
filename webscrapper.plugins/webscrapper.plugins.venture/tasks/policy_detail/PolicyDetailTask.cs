using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.eod_report.shared;
using webscrapper.plugins.venture.tasks.policy_detail.shared;

namespace webscrapper.plugins.venture.tasks.eod_report;

public class PolicyDetailTask : BaseScrapingTask
{
    public override string TaskName => "venture.policy_detail_task";

    public PolicyDetailTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        //"SearchValue=VGAO-04172-000&lookuptype=Policy&view=quicksearch&DisplayAcctFrame=Yes&MatchRule=AnyPart&doubleclicksearchcaution=1"

        var queryParams = new Dictionary<string, string>
        {
            ["SearchValue"] = Convert.ToString(AppConstants.InputModel.OtherInputs["PolicyNo"]),
            ["lookuptype"] = PdConstants.LookupType,
            ["view"] = PdConstants.View,
            ["DisplayAcctFrame"] = PdConstants.DisplayAcctFrame,
            ["MatchRule"] = PdConstants.MatchRule,
            ["doubleclicksearchcaution"] = PdConstants.DoubleClickSearchCaution
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";

        var html = await ExecuteStepAsync("Fetch Policy Detail Page", async () =>
            await webScraper.GetAsync(mainUrl, queryParams, cancellationToken));

        var document = await ExecuteStepAsync("Parse Policy Detail Document", async () =>
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        //var jsScriptContent = await ExecuteStepAsync("Load Javascript Extractor", () =>
        //    Task.FromResult(Utilities.GetJsScript(GetType(), "getReportData.js")));

        //var result = await ExecuteStepAsync("Execute Report Extractor", () =>
        //    Task.FromResult(document.ExecuteScript(jsScriptContent)));

        //var payments = await ExecuteStepAsync("Deserialize Payments Data", () =>
        //    Task.FromResult(JsonSerializer.Deserialize<List<PaymentModel>>(result.ToString())));

        return null;
    }
}
