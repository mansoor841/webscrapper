using AngleSharp.Js;
using webscrapper.plugins.common.classes;
using webscrapper.plugins.common.interfaces;
using webscrapper.plugins.venture.classes;
using webscrapper.plugins.venture.shared;
using webscrapper.plugins.venture.tasks.eod_report.models;
using webscrapper.plugins.venture.tasks.eod_report.shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webscrapper.plugins.venture.tasks.eod_report;

public class EodReportTask : BaseScrapingTask
{
    public override string TaskName => "venture.eod_report_task";

    public EodReportTask(IWebScraper _webScraper)
    {
        webScraper = _webScraper;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["view"] = EodConstants.View,
            ["rpt"] = EodConstants.Rpt,
            ["ReportType"] = EodConstants.ReportType,
            ["ReportStart"] = AppConstants.InputModel.StartDate,
            ["ReportEnd"] = AppConstants.InputModel.EndDate,
            ["DateSelect"] = EodConstants.DateSelect
        };
        var mainUrl = $"{AppConstants.InputModel.BaseUrl}{AppConstants.MainPath}";
        var html = await webScraper.GetAsync(mainUrl, queryParams, cancellationToken);
        var document = await webScraper.ParseDocumentAsync(html, cancellationToken);
        var jsScriptContent = Utilities.GetJsScript(GetType(), "getReportData.js");
        var result = document.ExecuteScript(jsScriptContent);
        var obj = JsonConvert.DeserializeObject<PaymentModel>(json);

        //var document = await webScraper.ParseHtmlAsync(html, cancellationToken);
        //var pmntTable = document.QuerySelector(".PmntTable");

        //if (pmntTable == null) throw new InvalidOperationException("Payment Table Not Found");

        //var payments = new List<PaymentModel>();
        //var columnNames = new[]
        //{
        //    "BatchType", "BatchUserID", "BatchUserName", "PaymentDate", "Policy", "NamedInsured", "HowPaid", "Amount", "BatchNumber", "AgentID"
        //};

        //foreach (var row in pmntTable.QuerySelectorAll("tr").Skip(1))
        //{
        //    var rowData = row.GetTableRowData(columnNames);

        //    if (rowData.Count > 0)
        //    {
        //        payments.Add(new PaymentModel
        //        {
        //            BatchType = rowData.GetValueOrDefault("BatchType"),
        //            BatchUserID = rowData.GetValueOrDefault("BatchUserID"),
        //            BatchUserName = rowData.GetValueOrDefault("BatchUserName"),
        //            PaymentDate = rowData.GetValueOrDefault("PaymentDate"),
        //            Policy = rowData.GetValueOrDefault("Policy"),
        //            NamedInsured = rowData.GetValueOrDefault("NamedInsured"),
        //            HowPaid = rowData.GetValueOrDefault("HowPaid"),
        //            Amount = rowData.GetValueOrDefault("Amount"),
        //            BatchNumber = rowData.GetValueOrDefault("BatchNumber"),
        //            AgentID = rowData.GetValueOrDefault("AgentID")
        //        });
        //    }
        //}

        return null;
    }
}
