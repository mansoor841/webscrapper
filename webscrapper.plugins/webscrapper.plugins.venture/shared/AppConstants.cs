using webscrapper.plugins.common.models;

namespace webscrapper.plugins.venture.shared;

public class AppConstants
{
    public const string LoginPath = "/test/root/logon/index.cfm";
    public const string MainPath = "/test/root/main.cfm";

    public static PluginInput InputModel { get; set; }
}