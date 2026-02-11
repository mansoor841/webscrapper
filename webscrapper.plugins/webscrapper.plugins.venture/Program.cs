using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;
using Flurl;
using Flurl.Http;
using System;

var url = "https://ventureinsga.net/test/root/logon/index.cfm";
IFlurlClient _httpClient = new FlurlClient();
var flurlRequest = _httpClient.Request(url);
var response = await flurlRequest.GetAsync();
var html = await response.GetStringAsync();
IBrowsingContext _browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
var document = await _browsingContext.OpenAsync(req => req.Content(html));
var loginForm = document.QuerySelector<IHtmlFormElement>("form#LogonForm");
if (loginForm == null)
{
    throw new InvalidOperationException("Login form not found on the page.");
}
var formData = new Dictionary<string, string>
        {
            { "userloginid", "12821015" },
            { "userloginname", "ONLINETEST" },
            { "password", "Am@xit22!" }
        };
document = await loginForm.SubmitAsync(formData);
var link = document.QuerySelector("a[target='MainIS25test']");
var nextUrl = link.GetAttribute("href");
var nextContent = await nextUrl.GetStringAsync();

//var url = "https://ventureinsga.net/test/root/logon/index.cfm";
//var jar = new CookieJar();
//var html = await url.WithCookies(jar).GetStringAsync();
//var parser = new HtmlParser();
//var doc = await parser.ParseDocumentAsync(html);
//var viewState = doc.QuerySelector<IHtmlInputElement>("input[name=__VIEWSTATE]")?.Value;
//var formData = new Dictionary<string, string>
//        {
//            { "userloginid", "12821015" },
//            { "userloginname", "ONLINETEST" },
//            { "password", "Am@xit22!" },
//            { "__VIEWSTATE", viewState }
//        };
//url = "https://ventureinsga.net/test/root/main.cfm";
//var resultDocument = await url.WithCookies(jar).PostUrlEncodedAsync(formData);
//html = await resultDocument.GetStringAsync();
//doc = await parser.ParseDocumentAsync(html);
//viewState = doc.QuerySelector<IHtmlInputElement>("input[name=__VIEWSTATE]")?.Value;

////url = "https://ventureinsga.net/test/root/index.cfm";
////html = await url.WithCookies(jar).GetStringAsync();

//Console.WriteLine("test");



//try
//{
//    var cookieJar = new CookieJar();
//    string loginUrl = "https://ventureinsga.net/test/root/logon/index.cfm";

//    // 1. GET the initial page to grab ViewState and Cookies
//    var initialResponse = await loginUrl
//        .WithCookies(cookieJar)
//        .GetStringAsync();

//    // 2. Parse with AngleSharp to find the form and its hidden inputs
//    var context = BrowsingContext.New(Configuration.Default);
//    var document = await context.OpenAsync(req => req.Content(initialResponse));
//    //var form = document.QuerySelector<IHtmlFormElement>("form");

//    // Collect ALL inputs from the form (including hidden ViewStates)
//    var postData = new Dictionary<string, string>();
//    //foreach (var element in document.QuerySelectorAll<IHtmlInputElement>("input"))
//    //{
//    //    if (!string.IsNullOrEmpty(element.Name))
//    //    {
//    //        postData[element.Name] = element.Value ?? "";
//    //    }
//    //}

//    postData["userloginid"] = "12821015";
//    postData["userloginname"] = "ONLINETEST";
//    postData["password"] = "Am@xit22!";
//    //postData["ctl00$MainContent$LoginControl$LoginButton"] = "Log In";

//    // 4. Submit the form
//    var postResponse = await loginUrl
//        .WithCookies(cookieJar)
//        .PostUrlEncodedAsync(postData)
//        .ReceiveString();

//    // 5. Now find your anchor tag in the resulting page
//    var postDoc = await context.OpenAsync(req => req.Content(postResponse));
//    var anchor = postDoc.QuerySelector<IHtmlAnchorElement>("a[target='MainIS25test']");

//    if (anchor != null)
//    {
//        var finalHtml = await anchor.Href.WithCookies(cookieJar).GetStringAsync();
//        Console.WriteLine("Navigated successfully!");
//    }
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.ToString()); 
//}