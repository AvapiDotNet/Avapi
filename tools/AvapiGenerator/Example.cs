using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AvapiGenerator
{
    public class Example
    {
        static string str_using;
        static string str_prefix;
        static string str_postfix;
        static string str_content;
        static string basePath;   
        static string list_key; 
        
        public static void init(string path , string key_priv)
        {
			str_content = "";
            basePath = path;
            list_key = key_priv;
            init_using();
            init_prefix();
            init_postfix();
        }


        internal static void init_using()
        {
            str_using =  "using System;\n" + "using System.IO;\n";
            str_using += "using System.Threading;\n";
        }

        internal static void init_prefix()
        {
            str_prefix = "\nnamespace Avapi\n" + "{\n" +
                "\tpublic class Example\n" + "\t{\n" +
                "\t\tprivate static string[] array_key = " + list_key + "\n" +

                "\t\tstatic private int index_key = 0;\n\n" +
                 "\t\tstatic private string get_key()\n" +
                "\t\t{\n" +
                "\t\t\tstring str_key = array_key[index_key];\n" +
                "\t\t\tindex_key = ( ( index_key + 1) % array_key.Length );\n" +
                "\t\t\treturn str_key;\n" +
                "\t\t}\n\n" +
                "\t\tstatic void Main()\n" + "\t\t{\n"+
                "\t\t\tIAvapiConnection connection = AvapiConnection.Instance;\n"+
                "\t\t\tstring pathFolder = Path.Combine(\"./\" , \"Results\" );\n" +
                "\t\t\tint retry = 5;\n" +
                "\t\t\tDirectory.CreateDirectory(pathFolder); \n" +
                "\t\t\tFileStream fileStream;\n"+
                "\t\t\tstring exception = string.Empty;\n" +
                "\t\t\tStreamWriter writer;\n" +
                "\t\t\tstring lastHttpRequest = string.Empty;\n\n";
        }

        internal static void init_postfix()
        {
            str_postfix =  "\t\t}\n" +
                "\t}\n" +
                "}\n";
        }

        internal static string exceptions =
            "\t\t\tusing (fileStream = new FileStream(String.Format(pathFolder + \"/EXCEPTION_MESSAGES.out\"), FileMode.Create))" + "\n" +
            "\t\t\t{" + "\n" +
            "\t\t\t\tusing (writer = new StreamWriter(fileStream))" + "\n" +
            "\t\t\t\t{" + "\n" +
            "\t\t\t\t\twriter.Write(exception);" + "\n" +
            "\t\t\t\t}" + "\n" +
            "\t\t\t}";


        // Add an using directive
        public static void add_using(Operation operation)
        {
            str_using += "using Avapi.Avapi" + operation.Name + ";\n";
        }

        // Add the parsing feature related with the operation
        public static void parsing_operation(Operation operation)
        {
            str_content +="\t\t\t\t\tstring parsed_data =\"\";\n";
            str_content += "\t\t\t\t\tparsed_data += lastHttpRequest +\"\\n\";" + "\n";
            if (operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content +=$"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content +=$"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.MetaData.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";

                doc = XDocument.Parse(operation.Response.TimeSeries);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                Node dateTime = new Node
                {
                    Name = "DateTime",
                    Text = "DateTime"
                };

                timeSeriesNodes.Add(dateTime);
                str_content += "\t\t\t\t\tforeach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "\t\t\t\t\t{" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content +=$"\t\t\t\t\t\tparsed_data += \"{node.Name}: \" + timeseries.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\t}" + "\n";
            }
            else if (operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content +=$"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                str_content +=$"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.MetaData.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";

                doc = XDocument.Parse(operation.Response.TechnicalIndicator);
                IList<Node> technicalIndicatorNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                Node dateTime = new Node
                {
                    Name = "DateTime",
                    Text = "DateTime"
                };

                technicalIndicatorNodes.Add(dateTime);
                str_content += "\t\t\t\t\tforeach (var technical in data.TechnicalIndicator)" + "\n";
                str_content += "\t\t\t\t\t{" + "\n";

                foreach (Node node in technicalIndicatorNodes)
                {
                str_content +=$"\t\t\t\t\t\tparsed_data += \"{node.Name}: \" + technical.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\t}" + "\n";
            }
            else if (operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content +=$"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content +=$"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.MetaData.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                
                doc = XDocument.Parse(operation.Response.SectorP);
                IList<Rank> RankNode = doc.Root.Elements("rank")
                    .Select(x => new Rank
                    {
                        Name = (string)x.Attribute("name"),
                        Text = (string)x.Attribute("text"),
                        ListNode = x.Elements("node")
                        .Select(y => new Node
                        {
                            Name = (string)y.Element("name"),
                            Text = (string)y.Element("text")
                        }).ToList(),
                    }).ToList();

                for(int i=0; i< RankNode.Count; ++i)
                {
                    str_content += "\t\t\t\t\tparsed_data += \"RankName: \" + "
                    + $"data.{RankNode[i].Name}.RankName"+ " +\"\\n\" ;" + "\n";

                    for(int j = 0 ; j < RankNode[i].ListNode.Count ;j++ )
                    {
                        str_content += "\t\t\t\t\tparsed_data += \""
                        +$"{RankNode[i].ListNode[j].Name} : \" + "
                        + $"data.{RankNode[i].Name}.{RankNode[i].ListNode[j].Name}"+ " +\"\\n\" ;" + "\n";
                    }
                    str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                }
            }
            else if (operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += $"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in currencyExchangeNode)
                {
                    str_content += $"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += $"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.MetaData.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";

                doc = XDocument.Parse(operation.Response.TimeSeries);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                Node dateTime = new Node
                {
                    Name = "DateTime",
                    Text = "DateTime"
                };

                timeSeriesNodes.Add(dateTime);
                str_content += "\t\t\t\t\tforeach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "\t\t\t\t\t{" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"\t\t\t\t\t\tparsed_data += \"{node.Name}: \" + timeseries.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\t}" + "\n";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += $"\t\t\t\t\tvar data = {operation.Name.ToLower()}Response.Data;" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"\t\t\t\t\tparsed_data += \"{node.Name}: \" + data.MetaData.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";

                doc = XDocument.Parse(operation.Response.StockQuotes);
                IList<Node> stockQuotesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "\t\t\t\t\tforeach (var stockQuotes in data.StockQuotes)" + "\n";
                str_content += "\t\t\t\t\t{" + "\n";

                foreach (Node node in stockQuotesNodes)
                {
                    str_content += $"\t\t\t\t\t\tparsed_data += \"{node.Name}: \" + stockQuotes.{node.Name} +\"\\n\" ;" + "\n";
                }
                str_content += "\t\t\t\t\t\tparsed_data += \"========================\" +\"\\n\" ;" + "\n";
                str_content += "\t\t\t\t\t}" + "\n";
            }

            str_content += "\t\t\t\t\tusing (fileStream = new FileStream(String.Format(pathFolder + \"/"
                + operation.Name + "_parsing.out\"), FileMode.Create))\n";
            str_content +="\t\t\t\t\tusing (writer = new StreamWriter(fileStream))\n";
            str_content +="\t\t\t\t\twriter.Write(parsed_data);\n";
        }

        // Add an entry related with the operation
        public static void add_entry(Operation operation , string symbol = "MSFT")
        {
            add_using(operation);
            str_content += "\t\t\ttry\n";
            str_content += "\t\t\t{\n";


            str_content += "\t\t\t\tconnection.Connect(get_key());\n";

            str_content += "\t\t\t\tvar " + operation.Name.ToLower() + " = " +
                "connection.GetQueryObject_"+ operation.Name +"();\n";
            
            str_content += "\t\t\t\tfor(int i = 0 ; i < retry ; i++ )\n"+
                "\t\t\t\t{\n";

            str_content += "\t\t\t\t\tIAvapiResponse_"+ operation.Name + " " + operation.Name.ToLower() +
                "Response = " + operation.Name.ToLower() +".Query(";
            if (operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += "\n                 \"GBP\",\"EUR\"";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += "\n                 \"BTC\", \"CNY\", null";
            }
            else if(operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += "\n                \"MSFT,FB\", null";
            }
            else if (operation.Parameters.Count > 0)
            {
                foreach (Parameter parameter in operation.Parameters)
                {
                    // If the parameter is a symbol type normaly the first, we can skip the other check
                    if(parameter.Name.Equals("symbol"))
                    {
                        str_content += "\n\t\t\t\t\t\t\""+symbol+"\",";
                        continue;
                    }

                    // If the parameter has a list of elements, normaly coded as enum
                    if (parameter.Items.Count > 0)
                    {
                        str_content += "\n\t\t\t\t\t\tConst_" + operation.Name + "." + operation.Name + 
                            "_" + parameter.Name + ".";

                        // If I have more than one element I will take the second one, cause the first normally is none
                        if(parameter.Items.Count > 1)
                        {
                            string item = parameter.Items.ElementAt(1);
                            if (Char.IsDigit(item[0]))
                                str_content +=  "n_";
                            
                            str_content += item+",";
                        }
                        else
                        {
                            string item = parameter.Items.ElementAt(0);
                            if (Char.IsDigit(item[0]))
                                str_content +=  "n_";
                            
                            str_content += item+",";
                        }
                    }
                    else 
                    {
                        // the element is coded as primitive type (ex: int, float, ...) 
                        switch (parameter.DataType)
                        {
                            case "float":
                                str_content += "\n\t\t\t\t\t\t0.2f," ;
                                break;
                            case "int":
                                str_content += "\n\t\t\t\t\t\t10," ;
                                break;

                            default:
                                str_content += "\n\t\t\t\t\t\tnull," ;
                                break;
                        }
                    }
                }
                str_content = str_content.Remove(str_content.Length-1);
            }
            str_content +="); \n\n";

            str_content += "\t\t\t\t\tlastHttpRequest = " + operation.Name.ToLower() + "Response.LastHttpRequest;\n";
            str_content +="\t\t\t\t\tif("+ operation.Name.ToLower() + "Response.Data != null)\n" +
                "\t\t\t\t\t{\n";
            str_content +="\t\t\t\t\t\tusing (fileStream = new FileStream(String.Format(pathFolder + \"/"
                + operation.Name + ".out\"), FileMode.Create))\n";
            str_content +="\t\t\t\t\t\tusing (writer = new StreamWriter(fileStream))\n";
            str_content +="\t\t\t\t\t\twriter.Write("+ operation.Name.ToLower() + "Response.RawData);\n\n";
            parsing_operation(operation);
            str_content +="\t\t\t\t\t\tThread.Sleep(1000);\n";            
            str_content +="\t\t\t\t\t\tbreak;\n"+
            "\t\t\t\t\t}\n";
            str_content +="\t\t\t\t}\n";
            str_content +="\t\t\t}\n";
            str_content +="\t\t\tcatch(Exception e)\n";
            str_content += "\t\t\t{\n";
            str_content += "\t\t\t\texception = $\"" + operation.Name + " -- Exception Message: {e.Message} - REQUEST: {lastHttpRequest}\";\n";
            str_content += "\t\t\t}\n\n\n";
        }

        // create the Example.cs file
        public static int create()
        {
            Directory.CreateDirectory(basePath);
            string projectPath = Path.Combine(basePath, "Example.cs");
            using (var fileStream = new FileStream(string.Format(projectPath), FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(str_using);
                writer.Write(str_prefix);
                str_content = str_content.Remove(str_content.Length-1);
                writer.Write(str_content);
                writer.Write(exceptions);
                writer.Write(str_postfix);
            }
            return 0;
        } 
    }
}