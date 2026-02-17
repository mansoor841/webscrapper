namespace webscrapper.plugins.venture.Tasks.ReportTask.Models;

public class ReportResult
{
    public List<PaymentModel> Payments { get; set; } = new();
    public DateTime GeneratedAt { get; set; }
    public object? Parameters { get; set; }
}