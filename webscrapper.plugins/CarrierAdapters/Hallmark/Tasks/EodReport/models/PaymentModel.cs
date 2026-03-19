namespace Adapter.Hallmark.Tasks.EodReport.Models;

public class PaymentModel
{
    public string? BatchType { get; set; }
    public string? BatchUserID { get; set; }
    public string? BatchUserName { get; set; }
    public string? PaymentDate { get; set; }
    public string? Policy { get; set; }
    public string? NamedInsured { get; set; }
    public string? HowPaid { get; set; }
    public string? Amount { get; set; }
    public string? BatchNumber { get; set; }
    public string? AgentID { get; set; }

    public object? PolicyInfo { get; set; }
    public object? VehicleList { get; set; }
}