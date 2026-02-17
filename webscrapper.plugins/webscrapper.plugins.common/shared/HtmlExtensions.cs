using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace webscrapper.plugins.common.shared;

public static class HtmlExtensions
{
    public static string? GetInputValue(this IHtmlDocument document, string name)
    {
        return document.QuerySelector<IHtmlInputElement>($"input[name='{name}']")?.Value;
    }

    public static IEnumerable<IElement> GetTableRows(this IHtmlDocument document, string tableSelector)
    {
        var table = document.QuerySelector(tableSelector);

        return table?.QuerySelectorAll("tr") ?? Enumerable.Empty<IElement>();
    }

    public static Dictionary<string, string> GetTableRowData(this IElement row, params string[] columnNames)
    {
        var tds = row.QuerySelectorAll("td").ToArray();
        var result = new Dictionary<string, string>();

        for (int i = 0; i < Math.Min(columnNames.Length, tds.Length); i++)
        {
            result[columnNames[i]] = tds[i].InnerHtml;
        }

        return result;
    }

    public static Dictionary<string, string> ExtractFormData(this IHtmlDocument document, string formSelector = "input")
    {
        var formData = new Dictionary<string, string>();

        foreach (var element in document.QuerySelectorAll<IHtmlInputElement>(formSelector))
        {
            if (!string.IsNullOrEmpty(element.Name))
            {
                formData[element.Name] = element.Value ?? "";
            }
        }

        return formData;
    }
}