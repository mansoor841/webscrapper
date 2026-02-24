using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.models;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.authentication;
using webscrapper.plugins.venture.tasks.eod_report;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.policy_detail;

namespace webscrapper.plugins.venture;

public class VentureScrapper : BaseWebScraper
{
    public VentureScrapper() { }

    public override async Task<PluginOutput> RunAsync(PluginInput inputModel, CancellationToken cancellationToken = default)
    {
        long start = Environment.TickCount64;

        AppConstants.InputModel = inputModel;

        var outputModel = new PluginOutput()
        {
            StartDate = AppConstants.InputModel.StartDate,
            EndDate = AppConstants.InputModel.EndDate
        };
        var authTask = new AuthenticationTask(this);
        var result = await authTask.ExecuteAsync(cancellationToken);

        outputModel.TaskResults.Add(result.ToMini());

        if (!result.Success)
        {
            outputModel.ElapsedMs = Environment.TickCount64 - start;
            outputModel.ErrorMessage = result.ErrorMessage;

            return outputModel;
        }

        if (AppConstants.InputModel.JobType == VentureJobTypeEnum.EOD)
        {
            var eodTask = new EodReportTask(this);
            result = await eodTask.ExecuteAsync(cancellationToken);

            outputModel.TaskResults.Add(result.ToMini());

            foreach (var payment in (List<PaymentModel>)result.Data)
            {
                AppConstants.InputModel.OtherInputs = new Dictionary<string, object>() { ["PolicyNo"] = payment.Policy };

                var pdTask = new PolicyDetailTask(this);
                var pdResult = await pdTask.ExecuteAsync(cancellationToken);

                payment.PolicyInfo = pdResult.Data;

                outputModel.TaskResults.Add(pdResult.ToMini());
            }
        }
        else if (AppConstants.InputModel.JobType == VentureJobTypeEnum.UPDATE)
        {
            var pdTask = new PolicyDetailTask(this);
            result = await pdTask.ExecuteAsync(cancellationToken);

            outputModel.TaskResults.Add(result.ToMini());
        }

        outputModel.ElapsedMs = Environment.TickCount64 - start;
        outputModel.Data = result.Data;
        outputModel.ErrorMessage = result.ErrorMessage;

        return outputModel;
    }
}

