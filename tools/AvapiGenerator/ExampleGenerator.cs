using System.Collections.Generic;

namespace AvapiGenerator
{
    public class ExampleGenerator
    {
        private static string examplePath;
        private static IList<Operation> operations;

        public static void initExampleGenerator(IList<Operation> list_operations ,string destinationPath)
        {
            examplePath = destinationPath;
            operations = list_operations;
            string list_key ="{\n"
                +"\t\t\t\"<API_KEY1>\",\n"
                + "\t\t\t\"<API_KEY2>\",\n"
                + "\t\t\t\"<API_KEY3>\",\n"
                + "\t\t\t\"<API_KEY4>\",\n"
                + "\t\t\t\"<API_KEY5>\",\n"
                + "\t\t\t\"<API_KEY6>\",\n"
                + "\t\t\t\"<API_KEY7>\",\n"
                + "\t\t\t\"<API_KEY8>\",\n"
                + "\t\t\t\"<API_KEY9>\",\n"
                + "\t\t\t\"<API_KEY10>\",\n"
                + "\t\t\t};\n";

            Example.init(examplePath , list_key);
        }    

        public static bool GenerateExample()
        {

            foreach (Operation operation in operations)
            {
                Example.add_entry(operation);
                Example.create();
            }

            return true;
        }
    }
}