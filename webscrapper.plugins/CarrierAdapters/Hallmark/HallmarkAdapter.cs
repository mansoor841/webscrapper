using CarrierFeedDownload.CrossCutting.Adapter.BaseClasses;
using CarrierFeedDownload.CrossCutting.Adapter.Models;
using webscrapper.plugins.hallmark.classes;
using webscrapper.plugins.hallmark.shared;
using webscrapper.plugins.hallmark.tasks.authentication;
using webscrapper.plugins.hallmark.tasks.eod_report;
using webscrapper.plugins.hallmark.tasks.eod_report.models;
using webscrapper.plugins.hallmark.tasks.policy_info;
using webscrapper.plugins.hallmark.tasks.policy_info.models;
using webscrapper.plugins.hallmark.tasks.vehicle_list;

namespace webscrapper.plugins.hallmark;

public class HallmarkAdapter : BaseAdapter
{
    public HallmarkAdapter() { }

    public override async Task<AdapterOutput> RunAsync(AdapterInput input, CancellationToken cancellationToken = default)
    {
        long start = Environment.TickCount64;

        AppConstants.InputModel = input;

        var output = new AdapterOutput()
        {
            StartDate = AppConstants.Input.StartDate,
            EndDate = AppConstants.Input.EndDate
        };
        var authTask = new AuthenticationTask(this);
        var result = await authTask.ExecuteAsync(cancellationToken);

        output.TaskResults.Add(result.ToMini());

        if (!result.Success)
        {
            output.ElapsedMs = Environment.TickCount64 - start;
            output.ErrorMessage = result.ErrorMessage;

            return output;
        }

        if (AppConstants.Input.JobType == HallmarkJobTypeEnum.TEST)
        {
            var pdTask = new PolicyInfoTask(this);
            result = await pdTask.ExecuteAsync(cancellationToken);

            AppConstants.InputModel.OtherInputs.Add("PolicyId", ((PolicyInfoModel)result.Data).PolicyId);

            var vlTask = new VehicleListTask(this);
            result = await vlTask.ExecuteAsync(cancellationToken);

            outputModel.TaskResults.Add(result.ToMini());
        }
        else if (AppConstants.InputModel.JobType == HallmarkJobTypeEnum.EOD)
        {
            var eodTask = new EodReportTask(this);
            result = await eodTask.ExecuteAsync(cancellationToken);

            outputModel.TaskResults.Add(result.ToMini());

            foreach (var payment in (List<PaymentModel>)result.Data)
            {
                AppConstants.InputModel.OtherInputs = new Dictionary<string, object>() { ["PolicyNo"] = payment.Policy };

                var pdTask = new PolicyInfoTask(this);
                var pdResult = await pdTask.ExecuteAsync(cancellationToken);

                payment.PolicyInfo = pdResult.Data;

                outputModel.TaskResults.Add(pdResult.ToMini());

                AppConstants.InputModel.OtherInputs.Add("PolicyId", ((PolicyInfoModel)pdResult.Data).PolicyId);

                var vlTask = new VehicleListTask(this);
                var vlResult = await vlTask.ExecuteAsync(cancellationToken);

                payment.VehicleList = vlResult.Data;

                outputModel.TaskResults.Add(vlResult.ToMini());
            }
        }
        else if (AppConstants.InputModel.JobType == HallmarkJobTypeEnum.UPDATE)
        {
            var pdTask = new PolicyInfoTask(this);
            result = await pdTask.ExecuteAsync(cancellationToken);

            outputModel.TaskResults.Add(result.ToMini());
        }

        outputModel.ElapsedMs = Environment.TickCount64 - start;
        outputModel.Data = result.Data;
        outputModel.ErrorMessage = result.ErrorMessage;

        return outputModel;
    }
}

