using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.policy_detail.models;
using webscrapper.plugins.venture.tasks.policy_detail.shared;

namespace webscrapper.plugins.venture.tasks.policy_detail;

public class PolicyDetailTask : BaseScrapingTask
{
    public override string TaskName => "venture.policy_detail_task";

    public PolicyDetailTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["PolicyNo"] = Convert.ToString(AppConstants.InputModel.OtherInputs["PolicyNo"]);

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

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () =>
            Task.FromResult(Utilities.GetJsScript(GetType(), "getPolicyInfo.js")));

        var result = await ExecuteStepAsync("Execute Javascript", () =>
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var policyInfo = await ExecuteStepAsync("Deserialize Policy Info Data", () =>
            Task.FromResult(JsonSerializer.Deserialize<PolicyInfoModel>(result.ToString())));

        return policyInfo;
    }
}
