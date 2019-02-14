using System; 
using System.IO;

namespace AvapiGenerator
{
    public class AvapiConnection
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
            str_prefix = "using System; \n"+
                "using System.Net.Http;\n"+
                "namespace Avapi\n"+
                "{\n"+
                "\tpublic class AvapiConnection : IAvapiConnection\n"+
                "\t{\n"+
                "\t\tprivate const string m_avapiUrlDefault = \"https://www.alphavantage.co\";\n"+
                "\t\tprivate string m_avapiUrl;\n"+
                "\t\tprivate HttpClient m_restClient;\n"+
                "\t\tprivate static readonly Lazy<AvapiConnection> s_avapiConnection =\n"+
                "\t\t\tnew Lazy<AvapiConnection>(() => new AvapiConnection());\n"+
                "\t\tpublic static AvapiConnection Instance\n"+
                "\t\t{\n"+
                "\t\t\tget\n"+
                "\t\t\t{\n"+
                "\t\t\t\treturn s_avapiConnection.Value;\n"+
                "\t\t\t}\n"+
                "\t\t}\n"+
                "\t\tprivate AvapiConnection()\n"+
                "\t\t{\n"+
                "\t\t}\n"+
                "\t\tpublic string AvapiUrl\n"+
                "\t\t{\n"+
                "\t\t\tget\n"+
                "\t\t\t{\n"+
                "\t\t\t\tif (!string.IsNullOrEmpty(m_avapiUrl))\n"+
                "\t\t\t\t{\n"+
                "\t\t\t\t\treturn m_avapiUrl;\n"+
                "\t\t\t\t}\n"+
                "\t\t\t\treturn m_avapiUrlDefault;\n"+
                "\t\t\t}\n"+
                "\t\t\tset\n"+
                "\t\t\t{\n"+
                "\t\t\t\tm_avapiUrl = value;\n"+
                "\t\t\t}\n"+
                "\t\t}\n"+
                "\t\tpublic string AvapiUrlDefault\n"+
                "\t\t{\n"+
                "\t\t\tget\n"+
                "\t\t\t{\n"+
                "\t\t\t\treturn m_avapiUrlDefault;\n"+
                "\t\t\t}\n"+
                "\t\t}\n"+
                "\t\tpublic string ApiKey\n"+
                "\t\t{\n"+
                "\t\t\tget;\n"+
                "\t\t\tset;\n"+
                "\t\t}\n"+
                "\t\tpublic void Connect(string apiKey)\n"+
                "\t\t{\n"+
                "\t\t\tm_restClient = new HttpClient();\n"+
                "\t\t\tApiKey = apiKey;\n"+
                "\t\t}\n";
        }

        internal static void init_postfix()
        {
            str_postfix = "\t}\n" +"}\n";
        }

        // Add an entry
        public static void add_str(string str)
        {
            str_content += str;
        }

        // Add an entry related with the operation.Name
        public static void add_entry(Operation operation)
        {
            str_content += "\t\tpublic Avapi" + operation.Name + ".Int_" + operation.Name + " GetQueryObject_" + operation.Name + "()" + "\n";
            str_content += "\t\t{" + "\n";
            str_content += "\t\t\tAvapi" + operation.Name + ".Impl_" + operation.Name + ".ApiKey = ApiKey;" + "\n";
            str_content += "\t\t\tAvapi" + operation.Name + ".Impl_" + operation.Name + ".AvapiUrl = AvapiUrl;" + "\n";
            str_content += "\t\t\tAvapi" + operation.Name + ".Impl_" + operation.Name + ".RestClient = m_restClient;" + "\n";
            str_content += "\t\t\treturn Avapi" + operation.Name + ".Impl_" + operation.Name + ".Instance;" + "\n";
            str_content += "\t\t}" + "\n";
        }

        // create the AvapiConnection.cs file
        public static int create()
        {
            string projectPath = Path.Combine(basePath, "AvapiConnection.cs");
            using (var fileStream = new FileStream(String.Format(projectPath), FileMode.Create))
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
