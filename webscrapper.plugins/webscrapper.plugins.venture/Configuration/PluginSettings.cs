namespace Scraping.Plugins.Venture.Configuration;

public class PluginSettings
{
    public string BaseUrl { get; set; } = "https://ventureinsga.net";
    public string LoginPath { get; set; } = "/test/root/logon/index.cfm";
    public string MainPath { get; set; } = "/test/root/main.cfm";
    public int SessionTimeoutMinutes { get; set; } = 30;
}