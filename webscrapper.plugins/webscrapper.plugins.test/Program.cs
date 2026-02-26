using webscrapper.plugins.common.models;
using webscrapper.plugins.venture;
using webscrapper.plugins.venture.classes;

var scrapper = new VentureScrapper();
var inputModel = new PluginInput()
{
    BaseUrl = "https://ventureinsga.net",
    Username = "ONLINETEST",
    Password = "Am@xit22!",
    AgentCode = "12821015",
    StartDate = "01/01/2026",
    EndDate = "01/31/2026",
    JobType = VentureJobTypeEnum.TEST,
    OtherInputs = new Dictionary<string, object>() { ["PolicyNo"] = "VGAO-04172-000" }
};
var result = await scrapper.RunAsync(inputModel);

Console.WriteLine("scrapping completed");