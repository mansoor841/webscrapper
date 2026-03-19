using Adapter.Hallmark.Shared;
using AngleSharp.Js;
using CarrierFeedDownload.CrossCutting.Adapter.BaseClasses;
using CarrierFeedDownload.CrossCutting.Adapter.Interfaces;
using System.Text.Json;

namespace Adapter.Hallmark.Tasks.Auth;

public class AuthTask : BaseAdapterTask
{
    public override string TaskName => "Hallmark.AuthTask";

    public AuthTask(IAdapter _adapter)
    {
        adapter = _adapter;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["userloginid"] = AppConstants.Input.LoginCode;
        _inputs["userloginname"] = AppConstants.Input.Username;
        _inputs["password"] = AppConstants.Input.Password;

        var loginUrl = $"{AppConstants.Input.BaseUrl}{AppConstants.LoginPath}";

        var html = await ExecuteStepAsync("Fetch Page", async () => 
            await adapter.GetAsync(loginUrl, cancellationToken: cancellationToken));

        var document = await ExecuteStepAsync("Parse Page", async () => 
            await adapter.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () => 
            Task.FromResult(Utilities.GetJsScript(GetType(), "GetData.js")));

        var jsResult = await ExecuteStepAsync("Execute Javascript", () => 
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var formData = await ExecuteStepAsync("Deserialize Data", () => 
            Task.FromResult(JsonSerializer.Deserialize<Dictionary<string, string>>(jsResult.ToString())));

        formData["userloginid"] = _inputs["userloginid"].ToString();
        formData["userloginname"] = _inputs["userloginname"].ToString();
        formData["password"] = _inputs["password"].ToString();

        var mainUrl = $"{AppConstants.Input.BaseUrl}{AppConstants.MainPath}";
        
        var result = await ExecuteStepAsync("Submit Form", async () => 
            await adapter.PostUrlEncodedAsync(mainUrl, formData));

        var isAuthenticated = result.Contains("index.cfm?view=logon/logout");

        return isAuthenticated ? isAuthenticated : throw new InvalidOperationException("Login Failed");
    }
}