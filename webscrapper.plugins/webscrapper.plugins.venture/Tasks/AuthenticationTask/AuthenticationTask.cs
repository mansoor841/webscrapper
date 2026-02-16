using Scraping.Common.Abstractions;
using Scraping.Common.Extensions;
using Scraping.Common.Models;
using Scraping.Plugins.Venture.Configuration;

namespace webscrapper.plugins.venture.Tasks.AuthenticationTask;

public class AuthenticationTask : BaseAuthenticationTask
{
    private readonly PluginSettings _settings;

    public override TaskContext Context { get; set; }
    public override string TaskName => "venture.authentication_task";

    public AuthenticationTask(TaskContext context, PluginSettings settings)
    {
        Context = context;
        _settings = settings;
    }

    public override async Task<AuthenticationResult> AuthenticateAsync(Credentials credentials, CancellationToken cancellationToken = default)
    {
        try
        {
            var loginUrl = $"{_settings.BaseUrl}{_settings.LoginPath}";
            var html = await Context.Scraper.GetAsync(loginUrl, cancellationToken: cancellationToken);
            var parser = new AngleSharp.Html.Parser.HtmlParser();
            var document = await parser.ParseDocumentAsync(html, cancellationToken);
            var formData = document.ExtractFormData();

            formData["userloginid"] = credentials.AdditionalFields.GetValueOrDefault("userloginid", "");
            formData["userloginname"] = credentials.Username;
            formData["password"] = credentials.Password;

            var mainUrl = $"{_settings.BaseUrl}{_settings.MainPath}";
            var result = await Context.Scraper.PostUrlEncodedAsync(mainUrl, formData);
            var isAuthenticated = result.Contains("index.cfm?view=logon/logout") && !result.Contains("invalid");

            var authResult = new AuthenticationResult
            {
                IsAuthenticated = isAuthenticated,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_settings.SessionTimeoutMinutes),
                ErrorMessage = isAuthenticated ? null : "Authentication failed"
            };

            SetAuthentication(authResult);

            return authResult;
        }
        catch (Exception ex)
        {
            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = ex.Message
            };
        }
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("Use AuthenticateAsync method directly");
    }
}