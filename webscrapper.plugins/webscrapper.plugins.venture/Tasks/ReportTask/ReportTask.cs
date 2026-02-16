using Scraping.Common.Abstractions;
using Scraping.Common.Extensions;
using Scraping.Common.Models;
using Scraping.Plugins.Venture.Configuration;
using webscrapper.plugins.venture.Tasks.ReportTask.Models;

namespace webscrapper.plugins.venture.Tasks.ReportTask;

public class ReportTask : BaseHtmlScrapingTask
{
    private readonly PluginSettings _settings;

    public override TaskContext Context { get; set; }
    public override string TaskName => "venture.report_task";

    public ReportTask(TaskContext context, PluginSettings settings)
    {
        Context = context;
        _settings = settings;
    }

    protected override async Task<object> ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["view"] = Context.Configuration.GetValueOrDefault("view", ""),
            ["rpt"] = Context.Configuration.GetValueOrDefault("rpt", ""),
            ["ReportType"] = Context.Configuration.GetValueOrDefault("ReportType", ""),
            ["ReportStart"] = Context.Configuration.GetValueOrDefault("ReportStart", ""),
            ["ReportEnd"] = Context.Configuration.GetValueOrDefault("ReportEnd", ""),
            ["DateSelect"] = Context.Configuration.GetValueOrDefault("DateSelect", "")
        };
        var mainUrl = $"{_settings.BaseUrl}{_settings.MainPath}";
        var html = await Context.Scraper.GetAsync(mainUrl, queryParams, cancellationToken);
        var document = await ParseHtmlAsync(html);

        var pmntTable = document.QuerySelector(".PmntTable");
        if (pmntTable == null)
        {
            throw new InvalidOperationException("Payment table not found");
        }

        var payments = new List<PaymentModel>();
        var columnNames = new[]
        {
            "BatchType", "BatchUserID", "BatchUserName", "PaymentDate", "Policy", "NamedInsured", "HowPaid", "Amount", "BatchNumber", "AgentID"
        };

        foreach (var row in pmntTable.QuerySelectorAll("tr").Skip(1))
        {
            var rowData = row.GetTableRowData(columnNames);

            if (rowData.Count > 0)
            {
                payments.Add(new PaymentModel
                {
                    BatchType = rowData.GetValueOrDefault("BatchType"),
                    BatchUserID = rowData.GetValueOrDefault("BatchUserID"),
                    BatchUserName = rowData.GetValueOrDefault("BatchUserName"),
                    PaymentDate = rowData.GetValueOrDefault("PaymentDate"),
                    Policy = rowData.GetValueOrDefault("Policy"),
                    NamedInsured = rowData.GetValueOrDefault("NamedInsured"),
                    HowPaid = rowData.GetValueOrDefault("HowPaid"),
                    Amount = rowData.GetValueOrDefault("Amount"),
                    BatchNumber = rowData.GetValueOrDefault("BatchNumber"),
                    AgentID = rowData.GetValueOrDefault("AgentID")
                });
            }
        }

        return new ReportResult
        {
            Payments = payments,
            GeneratedAt = DateTime.UtcNow,
            Parameters = queryParams
        };
    }
}
