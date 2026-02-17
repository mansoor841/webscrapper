namespace webscrapper.plugins.common.models;

public class PluginInput
{
    public required string BaseUrl { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string AgentCode { get; init; }
    public required string StartDate { get; init; }
    public required string EndDate { get; init; }
}
