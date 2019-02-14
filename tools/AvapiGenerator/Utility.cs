using System.IO;

namespace AvapiGenerator
{
    public class Utility
    {
        static string str_prefix;
        static string str_postfix;
        static string str_content;
        static string basePath;

        public static void init(string path)
        {
            str_content = "";
            basePath = path;
            init_prefix();
            init_postfix();
        }

        internal static void init_prefix()
        {
            str_prefix = "using System.Collections.Generic;\n"+
                "using System.Linq;\n"+
                "using System.Net;\n"+
                "using System.Text;\n\n"+
                "namespace Avapi\n"+
                "{\n"+
                "\t"+"internal static class UrlUtility\n"+
                "\t"+"{\n"+
                "\t\t"+"internal static string AsQueryString(IDictionary<string, string> parameters)\n"+
                "\t\t"+"{\n"+
                "\t\t\t"+"if (!parameters.Any())\n"+
                "\t\t\t\t"+"return \"\";\n\n"+
                "\t\t\t"+"var builder = new StringBuilder(\"?\");\n\n"+
                "\t\t\t"+"var separator = \"\";\n"+
                "\t\t\t"+"foreach (var kvp in parameters.Where(kvp => kvp.Value != null))\n"+
                "\t\t\t"+"{\n"+
                "\t\t\t\t"+"builder.AppendFormat(\"{0}{1}={2}\", separator, WebUtility.UrlEncode(kvp.Key),"+
                " WebUtility.UrlEncode(kvp.Value.ToString()));\n\n"+
                "\t\t\t\t"+"separator = \"&\";\n"+
                "\t\t\t"+"}\n"+
                "\t\t\t"+"return builder.ToString();\n"+
                "\t\t"+"}\n"+
                "\t"+"}\n"+"}";
        }

        internal static void init_postfix()
        {
            str_postfix = "";
        }

        // Add a string
        public static void add_str(string str)
        {
            str_content += str;
        }

        // create the project file
        public static int create()
        {
            string projectPath = Path.Combine(basePath, "Utility.cs");
            using (var fileStream = new FileStream(string.Format(projectPath), FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(str_prefix);
                writer.Write(str_content);
                writer.Write(str_postfix);
            }
            return 0;
        } 
    }
}
