using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.policy_info.models;
using webscrapper.plugins.venture.tasks.vehicle_list.shared;

namespace webscrapper.plugins.venture.tasks.vehicle_list;

public class VehicleListTask : BaseScrapingTask
{
    public override string TaskName => "venture.vehical_list_task";

    public VehicleListTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        //ClaimID=&AGENTCODE=

        //&=&=&=0&=&=&=&=VGAO-04172-000&AGENTCODE=&=

        _inputs["PolicyNo"] = AppConstants.InputModel.OtherInputs["PolicyNo"];

        var queryParams = new Dictionary<string, string>
        {
            ["view"] = VlConstants.View,
            ["Pages"] = VlConstants.Pages,
            ["PolicyID"] = "",
            ["DisplayAcctFrame"] = VlConstants.DisplayAcctFrame,
            ["ViewPrefix"] = VlConstants.ViewPrefix,
            ["SEARCHCLIENTNUMBER"] = VlConstants.SearchClientNumber,
            ["MULTICLIENTSEARCH"] = VlConstants.MultiClientSearch,
            ["FIELDNAMES"] = VlConstants.FieldNames,
            ["LOOKUPTYPE"] = VlConstants.LookupType,
            ["DOUBLECLICKSEARCHCAUTION"] = VlConstants.DoubleClickSearchCaution,
            ["SEARCHVALUE"] = _inputs["PolicyNo"].ToString()
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

        var policyInfo = await ExecuteStepAsync("Deserialize Data", () =>
            Task.FromResult(JsonSerializer.Deserialize<PolicyInfoModel>(result.ToString())));

        return policyInfo;
    }
}
