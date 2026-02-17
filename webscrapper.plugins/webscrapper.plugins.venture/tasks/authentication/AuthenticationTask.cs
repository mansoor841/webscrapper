using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.common.shared;
using webscrapper.plugins.venture.shared;

namespace webscrapper.plugins.venture.tasks.authentication;

public class AuthenticationTask : BaseScrapingTask
{
    public override string TaskName => "venture.authentication_task";

    public AuthenticationTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var loginUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.LoginPath}";
        var html = await webScraper.GetAsync(loginUrl, cancellationToken: cancellationToken);
        var document = await webScraper.ParseHtmlAsync(html, cancellationToken);
        var formData = document.ExtractFormData();

        formData["userloginid"] = AppConstants.InputModel.AgentCode;
        formData["userloginname"] = AppConstants.InputModel.Username;
        formData["password"] = AppConstants.InputModel.Password;

        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";
        var result = await webScraper.PostUrlEncodedAsync(mainUrl, formData);
        var isAuthenticated = result.Contains("index.cfm?view=logon/logout");

        return isAuthenticated ? isAuthenticated : throw new InvalidOperationException("Login Failed");

    }
}