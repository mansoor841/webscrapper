using System.Reflection;

namespace Adapter.Venture.Classes;

public static class Utilities
{
    public static string GetJsScript(Type source, string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = source.Namespace + ".scripts." + filename;
        var jsScript = string.Empty;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsScript = reader.ReadToEnd();
                }
            }
        }

        return jsScript;
    }
}
