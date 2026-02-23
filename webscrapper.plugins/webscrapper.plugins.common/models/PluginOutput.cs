namespace webscrapper.plugins.common.models;

public class PluginOutput
{
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public object? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<MiniTaskResult> TaskResults { get; set; } = new();
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;
    public long ElapsedMs { get; set; }
}
