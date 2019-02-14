using System;
using System.IO;

namespace AvapiGenerator
{
    public class IAvapiConnection
    {
        static string str_prefix;
        static string str_postfix;
        static string str_content;
        static string basePath;    
        
        public static void init(string path)
        {   
            init_prefix();
            init_postfix();
			str_content = "";
            basePath = path;
        }

        internal static void init_prefix()
        {
            str_prefix = "namespace Avapi\n" +
                "{\n" +
                "\tpublic interface IAvapiConnection\n" +
                "\t{\n" +
                "\t\tvoid Connect(string apiKey);\n" +
                "\t\tstring AvapiUrl { get; set; }\n" + 
                "\t\tstring AvapiUrlDefault { get; }\n" +
                "\t\tstring ApiKey { get; set; }\n"; 
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
            str_content += "\t\tAvapi" + operation.Name + ".Int_" + operation.Name + 
                " GetQueryObject_" + operation.Name + "();" + "\n";
        }

        // create the IAvapiConnection.cs file
        public static int create()
        {
            string projectPath = Path.Combine(basePath, "IAvapiConnection.cs");
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