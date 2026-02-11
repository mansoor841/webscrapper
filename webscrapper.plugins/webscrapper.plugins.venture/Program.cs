using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Flurl.Http;

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
var headerValues = loginResult.Headers.Where(p => p.Name == "Set-Cookie").Select(p => p.Value.Split(";"));
var queryParams = new Dictionary<string, string>();
//formData.Add("view", "acct_ReportToolBar");
//formData.Add("ReportID", "314");
//formData.Add("ReportStart", "01/01/2000");
//formData.Add("ReportEnd", "12/31/2025");
//formData.Add("IsDetailQry", "No");
//formData.Add("DateSelect", "CustomDates");
//formData.Add("LinkString", "");
//formData.Add("ReportType", "CFML");

queryParams.Add("view", "reports_cfml");
queryParams.Add("EUDATA", formData["EUDATA"]);
queryParams.Add("CFID", "reports_cfml");
queryParams.Add("CFTOKEN", "reports_cfml");
queryParams.Add("MarkViewed", "No");
queryParams.Add("RequestTimeout", "500");


html = await url.WithCookies(cookieJar).SetQueryParams(formData).GetStringAsync();
document = await htmlParser.ParseDocumentAsync(html);
