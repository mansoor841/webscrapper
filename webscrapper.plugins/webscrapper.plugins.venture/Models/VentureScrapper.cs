using Scraping.Common.Models;
using Scraping.Plugins.Venture.Configuration;
using webscrapper.plugins.common.Abstractions;
using webscrapper.plugins.venture.Tasks.AuthenticationTask;
using webscrapper.plugins.venture.Tasks.ReportTask;

namespace webscrapper.plugins.venture.Models;

public class VentureScrapper : BaseWebScraper
{
    private readonly PluginSettings _settings;

    public VentureScrapper()
    {
        _settings = new PluginSettings();
    }

    public async Task<bool> RunAsync(CancellationToken cancellationToken = default)
    {
        var context = new TaskContext
        {
            Scraper = this
        };
        var credentials = new Credentials
        {
            Username = "ONLINETEST",
            Password = "Am@xit22!",
            AdditionalFields = new Dictionary<string, string>
            {
                ["userloginid"] = "12821015"
            }
        };
        var authTask = new AuthenticationTask(context, _settings);
        var authResult = await authTask.AuthenticateAsync(credentials);

        if (!authResult.IsAuthenticated)
        {
            Console.WriteLine($"Authentication failed: {authResult.ErrorMessage}");
            return false;
        }

        Console.WriteLine("Authentication successful");

        context.Configuration = new Dictionary<string, string>
        {
            ["view"] = "reports_CFML",
            ["rpt"] = "314",
            ["ReportType"] = "ShowHTML",
            ["ReportStart"] = "01/01/2026",
            ["ReportEnd"] = "01/31/2026",
            ["DateSelect"] = "LastMonth"
        };
        var reportTask = new ReportTask(context, _settings);
        var result = await reportTask.ExecuteAsync();

        if (result.Success)
        {
            Console.WriteLine("Report Data Scraping completed successfully");
        }
        else
        {
            Console.WriteLine($"Scraping failed: {result.ErrorMessage}");
        }

        return true;
    }
}

