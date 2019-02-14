using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AvapiGenerator
{
    public class Implementation
    {
        private string str_prefix;
        private string str_postfix;
        private string str_content;
        private string path;    
        private string FileName;
        private Operation operation;
  
        public Implementation(string pathFolder , Operation operation)
        {
            str_content = "";
            FileName = "Impl_" + operation.Name + ".cs";
            path =  Path.Combine(pathFolder, FileName);
            this.operation = operation;            
            init_prefix();
            init_postfix();
        }

        private void init_prefix()
        {
            str_prefix = 
                "using System; " + "\n" +
                "using System.Collections;" + "\n" +
                "using System.Collections.Generic;" + "\n" +
                "using System.Net.Http;" + "\n" +
                "using Newtonsoft.Json;" + "\n" +
                "using System.Threading.Tasks;" + "\n" +
                "using Newtonsoft.Json.Linq;" + "\n\n" +
                "namespace Avapi.Avapi" + operation.Name + "\n" +
                "{" + "\n" ;
        }

        private void init_postfix()
        {
            str_postfix = "\n}";
        }

        //  Generate the body of public IAvapiResponse_XXXXX Query(...){...} for more detail check generate_query()
        // If i == 0 then return Query(symbol,current_interval,current_outputsize,current_datatype);
        // else if i == 1 then return await AsyncQuery(symbol, current_interval, current_outputsize, current_datatype);
        private void generate_queryBody(int i)
        {
            str_content += "\t\t{" + "\n";

            string str_Qreturn = string.Empty;
            if (i == 0)
            {
                str_Qreturn = "\n\t\t\t" + "return QueryPrimitive(";
            }
            else if(i == 1)
            {
                str_Qreturn = "\n\t\t\t" + "return await QueryPrimitiveAsync(";
            }
            foreach (Parameter parameter in operation.Parameters)
            {
                if(parameter.Items.Count != 0)
                {
                    if (parameter.DataType == "string")
                    {
                        str_content += "\t\t\t" + parameter.DataType +" current_" + parameter.Name + " = " +
                            "s_" + operation.Name + "_" + parameter.Name + "_translation["+ parameter.Name + 
                            "] as string";
                    }
                    else if(parameter.DataType == "int")
                    {
                        str_content += "\t\t\t" + parameter.DataType +" current_" + parameter.Name + " = (int)" +
                            "s_" + operation.Name + "_" + parameter.Name + "_translation["+ parameter.Name + 
                            "]";
                    }
                    str_content += ";\n";
                    str_Qreturn += "current_";
                }
                str_Qreturn += ""+parameter.Name+",";
            }

            str_Qreturn = str_Qreturn.Remove(str_Qreturn.Length-1);
            str_Qreturn += ");\n" ;

            str_content += "" + str_Qreturn;
            str_content += "\t\t}" + "\n" ;
        }



        /* Generate public IAvapiResponse_XXXXX Query( ....){...}
            1) if operation.Parameters.Count == 0, the method is not generated

            2) if operation.Parameters.Count > 0, Example:

            	public IAvapiResponse_XXXXX Query(
                    string symbol,
                    Const_XXXXX.XXXXX_interval interval,
                    Const_XXXXX.XXXXX_outputsize outputsize = Const_XXXXX.XXXXX_outputsize.none,
                    Const_XXXXX.XXXXX_datatype datatype = Const_XXXXX.XXXXX_datatype.none)
                {
                    generate_queryBody() ====> string current_interval = s_XXXXX_interval_translation[interval] as string;
                    generate_queryBody() ====> string current_outputsize = s_XXXXX_outputsize_translation[outputsize] as string;
                    generate_queryBody() ====> string current_datatype = s_XXXXX_datatype_translation[datatype] as string;

                    generate_queryBody() ====> return Query(symbol,current_interval,current_outputsize,current_datatype);
                }
        */
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
                    str_content += "\t\tpublic IAvapiResponse_" + operation.Name + " Query(";
                }
                else if(i == 1)
                {
                    str_content += "\t\tpublic async Task<IAvapiResponse_" + operation.Name + "> QueryAsync(";
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
                            str_content += "\n\t\t\tConst_" + operation.Name + "." + operation.Name + "_" +
                                parameter.Name + " " + parameter.Name + ",";
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
                            str_content += "\n\t\t\t" + parameter.DataType + " " + parameter.Name +
                                " " + tmpParameter + ",";
                        }
                        else
                        {
                            string constNamePrefix = "Const_" + operation.Name + "." + operation.Name +
                                "_" + parameter.Name;
                            string constNameAndVariable = constNamePrefix + " " + parameter.Name;
                            str_content += "\n\t\t\t" + constNameAndVariable + " = " + constNamePrefix + ".none,";
                        }
                    }
                }
                str_content = str_content.Remove(str_content.Length - 1);
                str_content += ")\n";

                generate_queryBody(i);

                str_content += "\n";
            }
        }

        //  Generate the body of public IAvapiResponse_XXXXX Query( ....) {...} for more detail check generate_queryPrimitive()
        // If i == 0 then
        // Sent the Request and get the raw data from the Response
        // string response = RestClient?.
        //      GetAsync(queryString)?.
        //      Result?.
        //      Content?.
        //      ReadAsStringAsync()?.
        //      Result;

        // else if i == 1 then
        // string content;
        //    using (var result = await RestClient.GetAsync(queryString))
        //    {
        //        content = await result.Content.ReadAsStringAsync();
        //    }
        private void generate_queryPrimitiveBody(int i)
        {
            str_content += "\t\t{" + "\n" +
            "\t\t\t" + "// Build Base Uri" + "\n" +
            "\t\t\t" + "string queryString = AvapiUrl + \"/query\";" + "\n\n" +
            "\t\t\t" + "// Build query parameters" + "\n" +
            "\t\t\t" + "IDictionary<string, string> getParameters = new Dictionary<string, string>();" + "\n" +
            "\t\t\t" + "getParameters.Add(new KeyValuePair<string, string>(\"function\", s_function));" + "\n" +
            "\t\t\t" + "getParameters.Add(new KeyValuePair<string, string>(\"apikey\", ApiKey));" + "\n";
            if (operation.Parameters.Count != 0)
            {
                foreach (Parameter parameter in operation.Parameters)
                {
                    str_content += "\t\t\t" + "getParameters.Add(new KeyValuePair<string, string>(" +
                        "\"" + parameter.Name + "\"" + "," + parameter.Name;

                    if (parameter.DataType == "string")
                    {
                        str_content += "));" + "\n";
                    }
                    else
                    {
                        str_content += ".ToString()" + "));" + "\n";
                    }
                }
            }
            str_content +=
                "\t\t\t" + "queryString += UrlUtility.AsQueryString(getParameters);" + "\n\n";

            if (i == 0)
            {
                str_content +=
                    "\t\t\t" + "// Sent the Request and get the raw data from the Response" + "\n" +
                    "\t\t\t" + "string response = RestClient?." + "\n" +
                    "\t\t\t\t" + "GetAsync(queryString)?." + "\n" +
                    "\t\t\t\t" + "Result?." + "\n" +
                    "\t\t\t\t" + "Content?." + "\n" +
                    "\t\t\t\t" + "ReadAsStringAsync()?." + "\n" +
                    "\t\t\t\t" + "Result; " + "\n\n";
            }
            else if(i == 1)
            {
                str_content +=
                    "\t\t\t" + "string response;" + "\n" +
                    "\t\t\t" + "using (var result = await RestClient.GetAsync(queryString))" + "\n" +
                    "\t\t\t" + "{" + "\n" +
                    "\t\t\t\t" + "response = await result.Content.ReadAsStringAsync();" + "\n" +
                    "\t\t\t" + "}" + "\n";
            }

            str_content +=
                "\t\t\t" + "IAvapiResponse_" + operation.Name + " ret = new AvapiResponse_" + operation.Name + "\n" +
                "\t\t\t{" + "\n" +
                "\t\t\t\t" + "RawData = response," + "\n" +
                "\t\t\t\t" + "Data = ParseInternal(response)," + "\n" +
                "\t\t\t\t" + "LastHttpRequest = queryString" + "\n" +
                "\t\t\t};" + "\n" +
                "\n\t\t\t"+"return ret;" + "\n" +
                "\t\t}" + "\n" ;
        }

        private void generate_queryPrimitive()
        {
            for (int i = 0; i < 2; ++i)
            {
                if (operation.Parameters.Count == 0)
                {
                    if(i == 0)
                    {
                        str_content += "\t\tpublic IAvapiResponse_" + operation.Name + " QueryPrimitive()";
                    }
                    else if (i == 1)
                    {
                        str_content += "\t\tpublic async Task<IAvapiResponse_" + operation.Name + "> QueryPrimitiveAsync()";
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        str_content += "\n\t\tpublic IAvapiResponse_" + operation.Name + " QueryPrimitive(";
                    }
                    else if(i == 1)
                    {
                        str_content += "\n\t\tpublic async Task<IAvapiResponse_" + operation.Name + "> QueryPrimitiveAsync(";
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
                    str_content += ")";
                }

                str_content += "\n";
                generate_queryPrimitiveBody(i);
            }
        }

        private void generate_avapiResponse()
        {
            str_content +=
               $"    internal class AvapiResponse_{operation.Name} : IAvapiResponse_{operation.Name}" + "\n" +
                "    {" + "\n" +
                "        public string LastHttpRequest" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "            internal set;" + "\n" +
                "" + "\n" + 
                "        }" + "\n" +
                "        public string RawData" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "            internal set;" + "\n" +
                "        }" + "\n" +
                "" + "\n" +
               $"        public IAvapiResponse_{operation.Name}_Content Data" + "\n" +
                "        {" + "\n" +
                "            get;" + "\n" +
                "            internal set;\n"+
                "        }" + "\n" +
                "    }" + "\n";
        }

        private void generate_avapiResponseClasses()
        {
            if (operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content += $"    public class MetaData_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    public class TimeSeries_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
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

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += $"           MetaData = new MetaData_Type_{operation.Name}();" + "\n";
                str_content += $"           TimeSeries = new List<TimeSeries_Type_{operation.Name}>();" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public MetaData_Type_{operation.Name} MetaData" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public IList<TimeSeries_Type_{operation.Name}> TimeSeries" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }
            else if (operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content += $"    public class MetaData_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    public class TechnicalIndicator_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
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

                foreach (Node node in technicalIndicatorNodes)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += $"           MetaData = new MetaData_Type_{operation.Name}();" + "\n";
                str_content += $"           TechnicalIndicator = new List<TechnicalIndicator_Type_{operation.Name}>();" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public MetaData_Type_{operation.Name} MetaData" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public IList<TechnicalIndicator_Type_{operation.Name}> TechnicalIndicator" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }
            else if (operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content += $"    public class MetaData_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

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
                    str_content +=
                    $"    public class {RankNode[i].Name}_Type_{operation.Name}" + "\n" +
                    "        {" + "\n" ;
                    str_content += $"        public string RankName" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                    for(int j = 0 ; j < RankNode[i].ListNode.Count ;j++ )
                    {
                        str_content += $"        public string {RankNode[i].ListNode[j].Name}" + "\n";
                        str_content += "        {" + "\n";
                        str_content += "            internal set;" + "\n";
                        str_content += "            get;" + "\n";
                        str_content += "        }" + "\n";
                    }
                    str_content +="        }" + "\n";
                }

                str_content += "\n";

                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += $"           MetaData = new MetaData_Type_{operation.Name}();" + "\n";
                for(int i=0; i< RankNode.Count; ++i)
                {
                    str_content += $"           {RankNode[i].Name} = new {RankNode[i].Name}_Type_{operation.Name}();" + "\n";
                }
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public MetaData_Type_{operation.Name} MetaData" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";

                for(int i=0; i< RankNode.Count; ++i)
                {
                    str_content +=
                    $"        public {RankNode[i].Name}_Type_{operation.Name} {RankNode[i].Name}" + "\n" +
                    "        {" + "\n" +
                    "            internal set;" + "\n" +
                    "            get;" + "\n" +
                    "        }" + "\n";
                    str_content += "" + "\n";
                }

                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }
            else if(operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in currencyExchangeNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += $"    public class MetaData_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    public class TimeSeries_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
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

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += $"           MetaData = new MetaData_Type_{operation.Name}();" + "\n";
                str_content += $"           TimeSeries = new List<TimeSeries_Type_{operation.Name}>();" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public MetaData_Type_{operation.Name} MetaData" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public IList<TimeSeries_Type_{operation.Name}> TimeSeries" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += $"    public class MetaData_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    public class StockQuotes_Type_{operation.Name}" + "\n";
                str_content += "    {" + "\n";
                doc = XDocument.Parse(operation.Response.StockQuotes);
                IList<Node> stockQuotesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in stockQuotesNodes)
                {
                    str_content += $"        [JsonProperty(\"{node.Text}\")]" + "\n";
                    str_content += $"        public string {node.Name}" + "\n";
                    str_content += "        {" + "\n";
                    str_content += "            internal set;" + "\n";
                    str_content += "            get;" + "\n";
                    str_content += "        }" + "\n";
                    str_content += "" + "\n";
                }
                str_content += "    }" + "\n";

                str_content += "\n";

                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += $"        internal AvapiResponse_{operation.Name}_Content()" + "\n";
                str_content += "        {" + "\n";
                str_content += $"           MetaData = new MetaData_Type_{operation.Name}();" + "\n";
                str_content += $"           StockQuotes = new List<StockQuotes_Type_{operation.Name}>();" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public MetaData_Type_{operation.Name} MetaData" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += $"       public IList<StockQuotes_Type_{operation.Name}> StockQuotes" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public bool Error" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "" + "\n";
                str_content += "        public string ErrorMessage" + "\n";
                str_content += "        {" + "\n";
                str_content += "            internal set;" + "\n";
                str_content += "            get;" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
            }

            else
            {
                str_content += $"    internal class AvapiResponse_{operation.Name}_Content : IAvapiResponse_{operation.Name}_Content" + "\n";
                str_content += "    {" + "\n";
                str_content += "    }" + "\n";
            }
        }

        private void generate_singletonImpl()
        {
            str_content += "\t\tprivate static readonly Lazy<Impl_" + operation.Name + 
                "> s_Impl_" + operation.Name + " =" + "\n" +
                "\t\t\tnew Lazy<Impl_" + operation.Name + ">(() => new Impl_" + 
                operation.Name + "());" + "\n" +
                "\t\tpublic static Impl_" + operation.Name + " Instance" + "\n" +
                "\t\t{" + "\n" +
                "\t\t\tget" + "\n" +
                "\t\t\t{" + "\n" +
                "\t\t\t\treturn s_Impl_" + operation.Name + ".Value;" + "\n" +
                "\t\t\t}" + "\n" +
                "\t\t}" + "\n" +
                "\t\tprivate Impl_" + operation.Name + "()" + "\n" +
                "\t\t{" + "\n" +
                "\t\t}" + "\n\n";
        }

        private void generate_IDictionary(Parameter parameter)
        {
            str_content += "\t\tinternal static readonly IDictionary s_" + operation.Name + "_" + 
                parameter.Name + "_translation" + "\n" + "\t\t\t = new Dictionary<Const_" + 
                operation.Name + "." + operation.Name + "_" + parameter.Name + ", " + parameter.DataType +
                ">()" + "\n" + "\t\t{";

            foreach (string item in parameter.Items)
            {
                // If the const name starts with number then prepend n_
                string strItem = item;
                if (Char.IsDigit(item[0]))
                {
                    strItem = item.Insert(0, "n_");
                }
                else if(item == "-1")
                {
                    strItem = "none";
                }

                str_content += "\n\t\t\t{" + "\n";
                str_content += "\t\t\t\tConst_" + operation.Name + "." + operation.Name + "_" + parameter.Name + "." + strItem + "," + "\n";
                if(parameter.DataType == "string")
                {
                    if (string.Equals(item.ToUpper(), "NONE")){
                        str_content += "\t\t\t\tnull" + "\n";
                    }
                    else {
                        str_content += "\t\t\t\t\"" + item + "\"" + "\n";
                    }
                }
                else if(parameter.DataType == "int")
                {
                    str_content += "\t\t\t\t" + item + "\n";
                }

                str_content += "\t\t\t},";
            }
            str_content = str_content.Remove(str_content.Length-1);
            str_content += "\n\t\t};\n\n" ;
        }

        private void generate_classImpl()
        {
            str_content += "\tpublic class Impl_" + operation.Name + " : Int_" + operation.Name + "\n" +
                        "\t{" + "\n" +
                        "\t\tconst string s_function = \"" + operation.Name + "\";" + "\n" +
                        "\n\t\tinternal static string ApiKey" + "\n" +
                        "\t\t{" + "\n" +
                        "\t\t\tget;" + "\n" +
                        "\t\t\tset;" + "\n" +
                        "\t\t}" + "\n" +
                        "\n\t\tinternal static HttpClient RestClient" + "\n" +
                        "\t\t{" + "\n" +
                        "\t\t\tget;" + "\n" +
                        "\t\t\tset;" + "\n" +
                        "\t\t}" + "\n" +
                        "\n\t\tinternal static string AvapiUrl" + "\n" +
                        "\t\t{" + "\n" +
                        "\t\t\tget;" + "\n" +
                        "\t\t\tset;" + "\n" +
                        "\t\t}" + "\n\n" ;

            generate_singletonImpl();

            foreach (Parameter parameter in operation.Parameters)
            {
                if (parameter.Items.Count > 0)
                    generate_IDictionary(parameter);
            }
            generate_query();
            generate_queryPrimitive();

        }

        private void generate_parsing()
        {
            str_content +=$"        static internal IAvapiResponse_{operation.Name}_Content ParseInternal(string jsonInput)" + "\n";
            str_content += "        {" + "\n";
            str_content += "            if (string.IsNullOrEmpty(jsonInput))" + "\n";
            str_content += "            {" + "\n";
            str_content += "                return null;" + "\n";
            str_content += "            }" + "\n";
            str_content += "            if(jsonInput == \"{}\")" + "\n";
            str_content += "            {" + "\n";
            str_content += "                return null;" + "\n";
            str_content += "            }" + "\n";
            str_content += "" + "\n";
            str_content +=$"            AvapiResponse_{operation.Name}_Content ret = new AvapiResponse_{operation.Name}_Content();" + "\n";
            if (operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Meta Data Parsing
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken metaData = jsonInputParsed[\"Meta Data\"];" + "\n";
                foreach (Node metaDataItem in metaDataNodes)
                {
                str_content +=$"                ret.MetaData.{metaDataItem.Name} = (string)metaData[\"{metaDataItem.Text}\"];" + "\n";
                }

                // Time Series Parsing
                doc = XDocument.Parse(operation.Response.TimeSeries);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                string timeSeriesName = (string)doc.Element("timeseries").Attribute("name");
                string[] timeSeriesNames = timeSeriesName.Split('|');

                str_content += $"                string timeSeries = \"{timeSeriesNames[0]}\";" + "\n";
                if(timeSeriesNames.Length > 1)
                {
                    str_content += "                string[] timeSeriesIntervals =" + "\n";
                    str_content += "                {" + "\n";
                    int i = 0;
                    for(; i < timeSeriesNames.Length - 1 ; ++i)
                    {
                    str_content += $"                    \"{timeSeriesNames[i]}\"," + "\n";
                    }
                    str_content += $"                    \"{timeSeriesNames[i]}\"" + "\n";
                    str_content += "                };" + "\n";
                    str_content += "                for (int i = 0; i < timeSeriesIntervals.Length; ++i)" + "\n";
                    str_content += "                {" + "\n";
                    str_content += "                    if (jsonInputParsed[timeSeriesIntervals[i]] != null)" + "\n";
                    str_content += "                    {" + "\n";
                    str_content += "                        timeSeries = timeSeriesIntervals[i];" + "\n";
                    str_content += "                        break;" + "\n";
                    str_content += "                    }" + "\n";
                    str_content += "                }" + "\n";
                }

                str_content +=$"                JEnumerable<JToken> results = jsonInputParsed[timeSeries].Children();" + "\n";
                str_content += "                foreach (JToken result in results)" + "\n";
                str_content += "                {" + "\n";
                str_content +=$"                    TimeSeries_Type_{operation.Name} timeseries = new TimeSeries_Type_{operation.Name}" + "\n";
                str_content += "                    {" + "\n";
                str_content += "                        DateTime = ((JProperty)result).Name," + "\n";

                for(int i=0; i<timeSeriesNodes.Count; ++i)
                {
                    if (i < timeSeriesNodes.Count - 1)
                    {
                str_content +=$"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]," + "\n";
                    }
                    else
                    {
                str_content +=$"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]" + "\n";
                    }
                }
                str_content += "                    };" + "\n";
                str_content += "                    ret.TimeSeries.Add(timeseries);" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";

            }
            else if (operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Meta Data Parsing
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken metaData = jsonInputParsed[\"Meta Data\"];" + "\n";
                foreach (Node metaDataItem in metaDataNodes)
                {
                    str_content += $"                ret.MetaData.{metaDataItem.Name} = (string)metaData[\"{metaDataItem.Text}\"];" + "\n";
                }

                // Time Series Parsing
                doc = XDocument.Parse(operation.Response.TechnicalIndicator);
                IList<Node> technicalIndicatorNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                string timeSeriesName = (string)doc.Element("technicalindicator").Attribute("name");

                str_content +=$"                JEnumerable<JToken> results = jsonInputParsed[\"{timeSeriesName}\"].Children();" + "\n";
                str_content += "                foreach (JToken result in results)" + "\n";
                str_content += "                {" + "\n";
                str_content +=$"                    TechnicalIndicator_Type_{operation.Name} technicalindicator = new TechnicalIndicator_Type_{operation.Name}" + "\n";
                str_content += "                    {" + "\n";
                str_content += "                        DateTime = ((JProperty)result).Name," + "\n";

                for (int i = 0; i < technicalIndicatorNodes.Count; ++i)
                {
                    if (i < technicalIndicatorNodes.Count - 1)
                    {
                        str_content += $"                        {technicalIndicatorNodes[i].Name} = (string)result.First[\"{technicalIndicatorNodes[i].Text}\"]," + "\n";
                    }
                    else
                    {
                        str_content += $"                        {technicalIndicatorNodes[i].Name} = (string)result.First[\"{technicalIndicatorNodes[i].Text}\"]" + "\n";
                    }
                }
                str_content += "                    };" + "\n";
                str_content += "                    ret.TechnicalIndicator.Add(technicalindicator);" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";

            }
            else if (operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Meta Data Parsing
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken metaData = jsonInputParsed[\"Meta Data\"];" + "\n";
                foreach (Node metaDataItem in metaDataNodes)
                {
                    str_content += $"                ret.MetaData.{metaDataItem.Name} = (string)metaData[\"{metaDataItem.Text}\"];" + "\n";
                }

                //Rank Parsing
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

                str_content += "                JToken result;\n\n";
                for(int i=0; i< RankNode.Count; ++i)
                {
                    str_content +=$"                //{RankNode[i].Name}\n"+
                    $"                result  = jsonInputParsed[\"{RankNode[i].Text}\"];\n" +
                    $"                ret.{RankNode[i].Name}.RankName = \"{RankNode[i].Text}\";\n";
                    for(int j = 0 ; j < RankNode[i].ListNode.Count ;j++ )
                    {
                        str_content += $"                ret.{RankNode[i].Name}.{RankNode[i].ListNode[j].Name} = " + 
                        $"(string)result[\"{RankNode[i].ListNode[j].Text}\"];\n";
                    }
                }
                str_content += "            }" + "\n";
            }
            else if (operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Realtime Currency Exchange Rate Parsing
                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken currencyExchange = jsonInputParsed[\"Realtime Currency Exchange Rate\"];" + "\n";
                foreach (Node currencyExchangeItem in currencyExchangeNodes)
                {
                    str_content += $"                ret.{currencyExchangeItem.Name} = (string)currencyExchange[\"{currencyExchangeItem.Text}\"];" + "\n";
                }
                str_content += "            }" + "\n";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Meta Data Parsing
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken metaData = jsonInputParsed[\"Meta Data\"];" + "\n";
                foreach (Node metaDataItem in metaDataNodes)
                {
                    str_content += $"                ret.MetaData.{metaDataItem.Name} = (string)metaData[\"{metaDataItem.Text}\"];" + "\n";
                }

                // Time Series Parsing
                doc = XDocument.Parse(operation.Response.TimeSeries);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                string timeSeriesName = (string)doc.Element("timeseries").Attribute("name");
                string[] timeSeriesNames = timeSeriesName.Split('|');

                str_content += $"                string timeSeries = \"{timeSeriesNames[0]}\";" + "\n";
                if (timeSeriesNames.Length > 1)
                {
                    str_content += "                string[] timeSeriesIntervals =" + "\n";
                    str_content += "                {" + "\n";
                    int i = 0;
                    for (; i < timeSeriesNames.Length - 1; ++i)
                    {
                        str_content += $"                    \"{timeSeriesNames[i]}\"," + "\n";
                    }
                    str_content += $"                    \"{timeSeriesNames[i]}\"" + "\n";
                    str_content += "                };" + "\n";
                    str_content += "                for (int i = 0; i < timeSeriesIntervals.Length; ++i)" + "\n";
                    str_content += "                {" + "\n";
                    str_content += "                    if (jsonInputParsed[timeSeriesIntervals[i]] != null)" + "\n";
                    str_content += "                    {" + "\n";
                    str_content += "                        timeSeries = timeSeriesIntervals[i];" + "\n";
                    str_content += "                        break;" + "\n";
                    str_content += "                    }" + "\n";
                    str_content += "                }" + "\n";
                }

                str_content += $"                JEnumerable<JToken> results = jsonInputParsed[timeSeries].Children();" + "\n";
                str_content += "                foreach (JToken result in results)" + "\n";
                str_content += "                {" + "\n";
                str_content += $"                    TimeSeries_Type_{operation.Name} timeseries = new TimeSeries_Type_{operation.Name}" + "\n";
                str_content += "                    {" + "\n";
                str_content += "                        DateTime = ((JProperty)result).Name," + "\n";

                for (int i = 0; i < timeSeriesNodes.Count; ++i)
                {
                    if (i < timeSeriesNodes.Count - 1)
                    {
                        if(operation.Name == "DIGITAL_CURRENCY_INTRADAY")
                        {
                            if(timeSeriesNodes[i].Name == "Price")
                            {
                                str_content += $"                        Price = (string)result.First[\"1a. price (\" + ret.MetaData.MarketCode + \")\"]," + "\n";
                            }
                            else
                            {
                                str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]," + "\n";
                            }
                        }
                        else if (operation.Name == "DIGITAL_CURRENCY_DAILY" ||
                                 operation.Name == "DIGITAL_CURRENCY_WEEKLY" ||
                                 operation.Name == "DIGITAL_CURRENCY_MONTHLY")
                        {
                            if (timeSeriesNodes[i].Name == "Open")
                            {
                                str_content += $"                        Open = (string)result.First[\"1a. open (\" + ret.MetaData.MarketCode + \")\"]," + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "High")
                            {
                                str_content += $"                        High = (string)result.First[\"2a. high (\" + ret.MetaData.MarketCode + \")\"]," + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "Low")
                            {
                                str_content += $"                        Low = (string)result.First[\"3a. low (\" + ret.MetaData.MarketCode + \")\"]," + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "Close")
                            {
                                str_content += $"                        Close = (string)result.First[\"4a. close (\" + ret.MetaData.MarketCode + \")\"]," + "\n";
                            }
                            else
                            {
                                str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]," + "\n";
                            }

                        }
                        else
                        {
                            str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]," + "\n";
                        }
                    }
                    else
                    {
                        if (operation.Name == "DIGITAL_CURRENCY_INTRADAY")
                        {
                            if (timeSeriesNodes[i].Name == "Open")
                            {
                                str_content += $"                        Open = (string)result.First[\"1a. open (\" + ret.MetaData.MarketCode + \")\"]" + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "High")
                            {
                                str_content += $"                        High = (string)result.First[\"2a. high (\" + ret.MetaData.MarketCode + \")\"]" + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "Low")
                            {
                                str_content += $"                        Low = (string)result.First[\"3a. low (\" + ret.MetaData.MarketCode + \")\"]" + "\n";
                            }
                            else if (timeSeriesNodes[i].Name == "Close")
                            {
                                str_content += $"                        Close = (string)result.First[\"4a. close (\" + ret.MetaData.MarketCode + \")\"]" + "\n";
                            }
                            else
                            {
                                str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]" + "\n";
                            }
                        }
                        else if(operation.Name == "DIGITAL_CURRENCY_DAILY" ||
                                operation.Name == "DIGITAL_CURRENCY_WEEKLY" ||
                                operation.Name == "DIGITAL_CURRENCY_MONTHLY")
                        {
                            if (timeSeriesNodes[i].Name == "Open")
                            {
                                str_content += $"                        Price = (string)result.First[\"1b. open (\" + ret.MetaData.MarketCode + \")\"]" + "\n";
                            }
                            else
                            {
                                str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]" + "\n";
                            }
                        }

                        else
                        {
                            str_content += $"                        {timeSeriesNodes[i].Name} = (string)result.First[\"{timeSeriesNodes[i].Text}\"]" + "\n";
                        }
                    }
                }
                str_content += "                    };" + "\n";
                str_content += "                    ret.TimeSeries.Add(timeseries);" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += "            JObject jsonInputParsed = JObject.Parse(jsonInput);" + "\n";
                str_content += "            string errorMessage = (string)jsonInputParsed[\"Error Message\"];" + "\n";
                str_content += "            if (!string.IsNullOrEmpty(errorMessage))" + "\n";
                str_content += "            {" + "\n";
                str_content += "                ret.Error = true;" + "\n";
                str_content += "                ret.ErrorMessage = errorMessage;" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                // Meta Data Parsing
                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                JToken metaData = jsonInputParsed[\"Meta Data\"];" + "\n";
                foreach (Node metaDataItem in metaDataNodes)
                {
                    str_content += $"                ret.MetaData.{metaDataItem.Name} = (string)metaData[\"{metaDataItem.Text}\"];" + "\n";
                }
                    
                // Stock Quotes Parsing
                str_content += $"                ret.StockQuotes = JsonConvert.DeserializeObject<List<StockQuotes_Type_{operation.Name}>>(jsonInputParsed[\"Stock Quotes\"].ToString());" + "\n";
                str_content += "            }" + "\n";
            }

            str_content += "            return ret;" + "\n";
            str_content += "        }" + "\n";
        }

        // generate the implementation file cs related to an operation.
        public bool generate_implementation()
        {
            generate_avapiResponse();
            str_content += "\n";
            generate_avapiResponseClasses();
            str_content += "\n";
            generate_classImpl();
            str_content += "\n";
            generate_parsing();

            // close the class
            str_content +="\t}";

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
