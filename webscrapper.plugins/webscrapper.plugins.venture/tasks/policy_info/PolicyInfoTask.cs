using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.policy_info.models;
using webscrapper.plugins.venture.tasks.policy_info.shared;

namespace webscrapper.plugins.venture.tasks.policy_info;

public class PolicyInfoTask : BaseScrapingTask
{
    public override string TaskName => "venture.policy_info_task";

    public PolicyInfoTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["PolicyNo"] = AppConstants.InputModel.OtherInputs["PolicyNo"];

        var queryParams = new Dictionary<string, string>
        {
            ["SearchValue"] = _inputs["PolicyNo"].ToString(),
            ["lookuptype"] = PiConstants.LookupType,
            ["view"] = PiConstants.View,
            ["DisplayAcctFrame"] = PiConstants.DisplayAcctFrame,
            ["MatchRule"] = PiConstants.MatchRule,
            ["doubleclicksearchcaution"] = PiConstants.DoubleClickSearchCaution
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";

        var html = await ExecuteStepAsync("Fetch Page", async () =>
            await webScraper.GetAsync(mainUrl, queryParams, cancellationToken));

        var document = await ExecuteStepAsync("Parse Document", async () =>
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () =>
            Task.FromResult(Utilities.GetJsScript(GetType(), "getPolicyInfo.js")));

        var result = await ExecuteStepAsync("Execute Javascript", () =>
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var data = await ExecuteStepAsync("Deserialize Data", () =>
            Task.FromResult(JsonSerializer.Deserialize<PolicyInfoModel>(result.ToString())));

        return data;
    }
}
