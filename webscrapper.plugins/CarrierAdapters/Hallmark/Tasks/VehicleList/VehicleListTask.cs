using AngleSharp.Js;
using CarrierFeedDownload.CrossCutting.Adapter.Interfaces;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.hallmark.classes;
using webscrapper.plugins.hallmark.shared;
using webscrapper.plugins.hallmark.tasks.policy_info.models;
using webscrapper.plugins.hallmark.tasks.vehicle_list.models;
using webscrapper.plugins.hallmark.tasks.vehicle_list.shared;

namespace webscrapper.plugins.hallmark.tasks.vehicle_list;

public class VehicleListTask : BaseAdapterTask
{
    public override string TaskName => "hallmark.vehical_list_task";

    public VehicleListTask(IAdapter _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["PolicyId"] = AppConstants.InputModel.OtherInputs["PolicyId"];

        var queryParams = new Dictionary<string, string>
        {
            ["view"] = VlConstants.View,
            ["Pages"] = VlConstants.Pages,
            ["PolicyID"] = _inputs["PolicyId"].ToString()
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";

        var html = await ExecuteStepAsync("Fetch Page", async () =>
            await webScraper.GetAsync(mainUrl, queryParams, cancellationToken));

        var document = await ExecuteStepAsync("Parse Document", async () =>
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () =>
            Task.FromResult(Utilities.GetJsScript(GetType(), "getVehicleList.js")));

        var result = await ExecuteStepAsync("Execute Javascript", () =>
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var data = await ExecuteStepAsync("Deserialize Data", () =>
            Task.FromResult(JsonSerializer.Deserialize<List<VehicleInfo>>(result.ToString())));

        return data;
    }
}
