using System;
using System.IO;

namespace AvapiGenerator
{
    public class Constant
    {
        private string str_prefix;
        private string str_postfix;
        private string str_content;
        private string path;    
        private string FileName;
        private Operation operation;
  
        public Constant(string pathFolder , Operation operation)
        {

            str_content = "";
            FileName = "Const_" + operation.Name + ".cs";
            path =  Path.Combine(pathFolder, FileName);
            this.operation = operation;            
            init_prefix();
            init_postfix();
        }

        private void init_prefix()
        {
            str_prefix = "namespace Avapi.Avapi" + operation.Name + "\n" +
                "{" + "\n" +
                "\t" + "public static class Const_" + operation.Name + "\n" +
                "\t" + "{" + "\n";
        }

        private void init_postfix()
        {
            str_postfix = "\t" + "}" + "\n" +"}\n";
        }

        /* Generate a constant cs file related to an operation, we have 2 cases:

            1) operation.Parameters.Count <= 0:
            2) operation.Parameters.Count> 0, param_1.Items.Count > 0, param_2.Items.Count = 0
            
            For more details see above.
        */
        public bool generate_constant()
        {
            if (operation.Parameters.Count > 0)
            {
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Items.Count > 0)
                    {
                        str_content += "\t\tpublic enum " + operation.Name + 
                            "_" + parameter.Name + "\n" + "\t\t{";
                    
                        int i = 0;
                        foreach (string item in parameter.Items)
                        {
                            // If the const name starts with number then prepend n_
                            string strItem = "\n\t\t\t" ;
                            if (Char.IsDigit(item[0]))
                            {
                                strItem += "n_";
                                strItem = string.Concat(strItem , item);
                            }
                            else if(item =="-1")
                            {
                                strItem = "none";
                            }
                            else{
                                strItem = string.Concat(strItem , item);
                            }

                            str_content += strItem;

                            if (i != (parameter.Items.Count - 1))
                            {
                                str_content +=  ",";
                            }
                            ++i;
                        }
                        str_content += "\n\t\t" + "}" + "\n";
                    }
                }
                create();
            }
            return true;
        }

        // create the Constant .cs file
        private int create()
        {
            using (var fileStream = new FileStream(String.Format(path), FileMode.Create))
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