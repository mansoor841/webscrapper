using System.Diagnostics;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.models;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.authentication;
using webscrapper.plugins.venture.tasks.eod_report;

namespace webscrapper.plugins.venture.classes;

public class VentureScrapper : BaseWebScraper
{
    public VentureScrapper() { }

    public override async Task<PluginOutput> RunAsync(PluginInput inputModel, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        AppConstants.InputModel = inputModel;

        var outputModel = new PluginOutput()
        {
            StartDate = AppConstants.InputModel.StartDate,
            EndDate = AppConstants.InputModel.EndDate
        };
        var authTask = new AuthenticationTask(this);
        var result = await authTask.ExecuteAsync(cancellationToken);

        if (!result.Success)
        {
            stopwatch.Stop();

            outputModel.ErrorMessage = result.ErrorMessage;
            outputModel.Duration = stopwatch.Elapsed;

            return outputModel;
        }

        var eodTask = new EodReportTask(this);
        result = await eodTask.ExecuteAsync(cancellationToken);

        stopwatch.Stop();

        outputModel.Duration = stopwatch.Elapsed;

        if (result.Success)
        {
            outputModel.Data = result.Data;
        }
        else
        {
            outputModel.ErrorMessage = result.ErrorMessage;
        }

        return outputModel;
    }
}

