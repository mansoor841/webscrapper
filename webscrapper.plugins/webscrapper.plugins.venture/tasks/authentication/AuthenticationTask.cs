using AngleSharp.Js;
using System.Text.Json;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;

namespace webscrapper.plugins.venture.tasks.authentication;

public class AuthenticationTask : BaseScrapingTask
{
    public override string TaskName => "venture.authentication_task";

    public AuthenticationTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _inputs["userloginid"] = AppConstants.InputModel.AgentCode;
        _inputs["userloginname"] = AppConstants.InputModel.Username;
        _inputs["password"] = AppConstants.InputModel.Password;

        var loginUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.LoginPath}";

        var html = await ExecuteStepAsync("Fetch Login Page", async () => 
            await webScraper.GetAsync(loginUrl, cancellationToken: cancellationToken));

        var document = await ExecuteStepAsync("Parse Login Page", async () => 
            await webScraper.ParseDocumentAsync(html, cancellationToken));

        var jsScriptContent = await ExecuteStepAsync("Load Javascript", () => 
            Task.FromResult(Utilities.GetJsScript(GetType(), "getFormData.js")));

        var jsResult = await ExecuteStepAsync("Execute Javascript", () => 
            Task.FromResult(document.ExecuteScript(jsScriptContent)));

        var formData = await ExecuteStepAsync("Deserialize Form Data", () => 
            Task.FromResult(JsonSerializer.Deserialize<Dictionary<string, string>>(jsResult.ToString())));

        formData["userloginid"] = AppConstants.InputModel.AgentCode;
        formData["userloginname"] = AppConstants.InputModel.Username;
        formData["password"] = AppConstants.InputModel.Password;

        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";
        
        var result = await ExecuteStepAsync("Submit Authentication Form", async () => 
            await webScraper.PostUrlEncodedAsync(mainUrl, formData));

        var isAuthenticated = result.Contains("index.cfm?view=logon/logout");

        return isAuthenticated ? isAuthenticated : throw new InvalidOperationException("Login Failed");
    }
}