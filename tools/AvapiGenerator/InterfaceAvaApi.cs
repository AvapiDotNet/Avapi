using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AvapiGenerator
{
    public class InterfaceAvApi
    {
        private string str_prefix;
        private string str_postfix;
        private string str_content;
        private string path;    
        private string FileName;
        private Operation operation;
  
        public InterfaceAvApi(string pathFolder , Operation operation)
        {
            str_content = "";
            FileName = "Int_" + operation.Name + ".cs";
            path =  Path.Combine(pathFolder, FileName);
            this.operation = operation;            
            init_prefix();
            init_postfix();
        }

        private void init_prefix()
        {
            str_prefix =
                "using System.Collections.Generic;"             + "\n" +
                "using System.Threading.Tasks;"                 + "\n" +
                "namespace Avapi.Avapi" + operation.Name        + "\n" +
                "{"                                             + "\n" +
                "    public interface Int_" + operation.Name    + "\n" +
                "    {"                                         + "\n";
        }

        private void init_postfix()
        {
            str_postfix = "}" + "\n";
        }

        private void generate_query()
        {
            if (operation.Parameters.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 2; ++i)
            {
                bool areThereItems = false;
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Items.Count > 0)
                    {
                        areThereItems = true;
                        break;
                    }
                }
                if (!areThereItems)
                {
                    return;
                }
                if (i == 0)
                {
                    str_content += "\t\tIAvapiResponse_" + operation.Name + " Query(";
                }
                else if (i == 1)
                {
                    str_content += "\t\tTask<IAvapiResponse_" + operation.Name + "> QueryAsync(";
                }
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Mandatory == true)
                    {
                        if (parameter.Items.Count == 0)
                        {
                            str_content += "\n\t\t\t" + parameter.DataType + " " + parameter.Name + ",";
                        }
                        else
                        {
                            str_content += "\n\t\t\tConst_" + operation.Name + "." + operation.Name +
                                "_" + parameter.Name + " " + parameter.Name + ",";
                        }
                    }
                    else
                    {
                        string tmpParameter = "";

                        switch (parameter.DataType)
                        {
                            case "float":
                            case "int":
                                tmpParameter = "= -1";
                                break;

                            default:
                                tmpParameter = "= null";
                                break;
                        }

                        if (parameter.Items.Count == 0)
                        {
                            str_content += "\n\t\t\t" + parameter.DataType + " " + parameter.Name + " " + tmpParameter + ",";
                        }
                        else
                        {
                            string constNamePrefix = "Const_" + operation.Name + "." + operation.Name + "_" + parameter.Name;
                            string constNameAndVariable = constNamePrefix + " " + parameter.Name;
                            str_content += "\n\t\t\t" + constNameAndVariable + " = " + constNamePrefix + ".none,";
                        }
                    }
                }
                str_content = str_content.Remove(str_content.Length - 1);
                str_content += ");\n\n";
            }
        }

        private void generate_queryPrimitive()
        {
            for (int i = 0; i < 2; ++i)
            {
                if (operation.Parameters.Count == 0)
                {

                    if (i == 0)
                    {
                        str_content += "\t\tIAvapiResponse_" + operation.Name + " QueryPrimitive();\n";
                        continue;
                    }
                    else if (i == 1)
                    {
                        str_content += "\t\tTask<IAvapiResponse_" + operation.Name + "> QueryPrimitiveAsync();\n";
                        return;
                    }
                }

                if (i == 0)
                {
                    str_content += "\n\t\tIAvapiResponse_" + operation.Name + " QueryPrimitive(";
                }
                else if (i == 1)
                {
                    str_content += "\t\tTask<IAvapiResponse_" + operation.Name + "> QueryPrimitiveAsync(";
                }

                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Mandatory == true)
                    {
                        str_content += "\n\t\t\t" + parameter.DataType + " " + parameter.Name + ",";
                    }
                    else
                    {
                        string tmpParameter = "";
                        switch (parameter.DataType)
                        {
                            case "float":
                            case "int":
                                tmpParameter = "= -1";
                                break;

                            default:
                                tmpParameter = "= null";
                                break;
                        }
                        str_content += "\n\t\t\t" + parameter.DataType + " " + parameter.Name + " " + tmpParameter + ",";
                    }
                }
                str_content = str_content.Remove(str_content.Length - 1);
                str_content += ");\n\n";
            }
        }

        private void generate_iavapiResponse()
        {
            str_content +=
               $"    public interface IAvapiResponse_{operation.Name}" + "\n" +
                "    {" + "\n" +
                "        string LastHttpRequest" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "" + "\n" +
                "        string RawData" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "" + "\n" +
               $"        IAvapiResponse_{operation.Name}_Content Data" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "    }" + "\n\n" ;
        }

        // Generate: public interface IAvapiResponse_XXXXX_Content{}
        private void generate_iavapiResponseContent()
        {
                str_content +=
            "    public interface IAvapiResponse_" + operation.Name + "_Content" + "\n" +
            "    {" + "\n";



            if (operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content +=
            "        bool Error" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n" +
            "        string ErrorMessage" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        MetaData_Type_{operation.Name} MetaData" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        IList <TimeSeries_Type_{operation.Name}> TimeSeries" + "\n" +
            "        {"                                                   + "\n" +
            "            get;"                                            + "\n" +
            "        }"                                                   + "\n";
            }
            else if(operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content +=
            "        bool Error" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n" +
            "        string ErrorMessage" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        MetaData_Type_{operation.Name} MetaData" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        IList <TechnicalIndicator_Type_{operation.Name}> TechnicalIndicator" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n";
            }

            else if(operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content +=
                "        bool Error" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "" + "\n" +
                "        string ErrorMessage" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "" + "\n";

                str_content +=
                $"        MetaData_Type_{operation.Name} MetaData" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "        }" + "\n" +
                "" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.SectorP);
                IList<Rank> RankNode = doc.Root.Elements("rank")
                    .Select(x => new Rank
                    {
                        Name = (string)x.Attribute("name"),
                        Text = (string)x.Attribute("text"),
                        ListNode = null
                    }).ToList();

                for(int i=0; i< RankNode.Count; ++i)
                {
                    str_content +=
                    $"        {RankNode[i].Name}_Type_{operation.Name} {RankNode[i].Name}" + "\n" +
                    "        {" + "\n" +
                    "            get;" + "\n" +
                    "        }" + "\n";
                }
            }
            else if (operation.Response.TypeResponse == "FX_RESPONSE")
            {

                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in currencyExchangeNode)
                {
                    str_content += $"        string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }

                str_content +=
            "        bool Error" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n" +
            "        string ErrorMessage" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content +=
            "        bool Error" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n" +
            "        string ErrorMessage" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        MetaData_Type_{operation.Name} MetaData" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        IList <TimeSeries_Type_{operation.Name}> TimeSeries" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n";

            }
            else if(operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content +=
            "        bool Error" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n" +
            "        string ErrorMessage" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        MetaData_Type_{operation.Name} MetaData" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n" +
            "" + "\n";

                str_content +=
           $"        IList <StockQuotes_Type_{operation.Name}> StockQuotes" + "\n" +
            "        {" + "\n" +
            "            get;" + "\n" +
            "        }" + "\n";
            }

            str_content +=
            "    }" + "\n";
        }

        // generate an interface file related to an operation.
        public bool generate_interface(Response response)
        {
            generate_query() ;
            generate_queryPrimitive() ;
            str_content += "\t" + "}" + "\n\n";
            //end of public interface Int_
            
            generate_iavapiResponse();
            generate_iavapiResponseContent();
            
            create();
            return true;
        }

        // create the Interface .cs file
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