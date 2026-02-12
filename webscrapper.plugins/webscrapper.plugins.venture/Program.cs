using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Flurl.Http;
using System.Dynamic;

var cookieJar = new CookieJar();
var url = "https://ventureinsga.net/test/root/logon/index.cfm";
var html = await url.GetStringAsync();
var htmlParser = new HtmlParser();
var document = await htmlParser.ParseDocumentAsync(html);
var formData = new Dictionary<string, string>();

foreach (var element in document.QuerySelectorAll<IHtmlInputElement>("input"))
{
    if (!string.IsNullOrEmpty(element.Name))
    {
        formData[element.Name] = element.Value ?? "";
    }
}

formData["userloginid"] = "12821015";
formData["userloginname"] = "ONLINETEST";
formData["password"] = "Am@xit22!";
formData = formData.Where(p => !string.IsNullOrEmpty(p.Key) && !string.IsNullOrEmpty(p.Value)).ToDictionary<string, string>();
url = "https://ventureinsga.net/test/root/main.cfm";

var loginResult = await url.WithCookies(out cookieJar).PostUrlEncodedAsync(formData);
var queryParams = new Dictionary<string, string>();

queryParams.Add("view", "reports_CFML");
queryParams.Add("rpt", "314");
queryParams.Add("ReportType", "ShowHTML");
queryParams.Add("ReportStart", "01/01/2026");
queryParams.Add("ReportEnd", "01/31/2026");
queryParams.Add("DateSelect", "LastMonth");

html = await url.WithCookies(cookieJar).SetQueryParams(queryParams).GetStringAsync();
document = await htmlParser.ParseDocumentAsync(html);
var pmntTable = document.QuerySelector(".PmntTable")!;
var list = new List<dynamic>();

foreach (var tr in pmntTable.QuerySelectorAll("tr"))
{
    var tds = tr.QuerySelectorAll("td");

    if (tds != null && tds.Length > 0)
    {
        dynamic data = new ExpandoObject();

        data.BatchType = tds[0].InnerHtml;
        data.BatchUserID = tds[1].InnerHtml;
        data.BatchUserName = tds[2].InnerHtml;
        data.PaymentDate = tds[3].InnerHtml;
        data.Policy = tds[4].InnerHtml;
        data.NamedInsured = tds[5].InnerHtml;
        data.HowPaid = tds[6].InnerHtml;
        data.Amount = tds[7].InnerHtml;
        data.BatchNumber = tds[8].InnerHtml;
        data.AgentID = tds[9].InnerHtml;

        list.Add(data);
    }
}

Console.WriteLine("scrapping complete");
