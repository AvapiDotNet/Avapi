using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvapiGenerator
{
    public class DocGenerator
    {
        private static string docPath;
        private static IList<Operation> operations;

        public static void initDocGenerator(IList<Operation> list_operations ,string destinationPath)
        {
            docPath = destinationPath;
            operations = list_operations;
        }    

        internal static void create_home()
        {
            string str_home = string.Empty;
            str_home += "# ALPHA VANTAGE API .NET CORE WRAPPER" + "\n";
            str_home += "\n";
            str_home += "## Intro" + "\n";
            str_home += "[[Introduction]]  " + "\n";
            str_home += "[[Getting Started]]  " + "\n";

            HashSet<string> types = new HashSet<string>();
            foreach(Operation operation in operations)
            {
                types.Add(operation.Type);
            }
            foreach(string currentType in types)
            {
                str_home += $"## {currentType}" + "\n";
                foreach(Operation operation in operations.Where(o => o.Type == currentType))
                {
                    str_home += $"[[{operation.Name}]]  " + "\n";
                }
            }

            Directory.CreateDirectory(docPath);
            using (var fileStream = new FileStream($"{docPath}/Home.md", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(str_home);    
                }
            }
        }

        public static bool GenerateDocumentation()
        {
            create_home();

            foreach (Operation operation in operations)
            {
                // Create a documentation file for the operation
                var file_doc = new Documentation(docPath , operation);
                if(!file_doc.generate_documentation())
                {
                     Console.WriteLine("Error: "+operation.Name+"generate_documentation()");
                     return false;
                }
            }

            return true;
        }
    }
}




