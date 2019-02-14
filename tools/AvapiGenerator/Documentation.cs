using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AvapiGenerator
{
    public class Documentation
    {
        private string str_content;
        private string path;    
        private Operation operation;    
        
        public Documentation(string pathFolder , Operation operation)
        {
            str_content = "";
            path = pathFolder;
            this.operation = operation;            

        }

        private static string NewLine(){
            return "  " + "\n";
        }

        private static string DoubleNewLine(){
            return "  " + "\n\n";
        }

        private static string EndSection(bool withHorizontalLine = true)
        {
            string ret = "  " + "\n\n";
            if(withHorizontalLine)
            {
                ret += "***" + "\n";
            }
            return ret;
        }

                private static string printDescription(Parameter parameter)
        {
            return parameter.Description ?? "No description available";
        }

        private static string printParameterNameAndOPTIONAL(Parameter parameter)
        {
            if(parameter.Mandatory.Value)
            {
                return $"{parameter.Name}";
            }
            else
            {
                return $"{parameter.Name} [OPTIONAL]";
            }
        }

        private static string BuildEnumBlock(Operation operation, Parameter parameter)
        {
            string enumBlock = string.Empty;
            enumBlock += $"public enum {operation.Name}_{parameter.Name}" + "\n";
            enumBlock += "{" + "\n";
            int i = 0;
            
            foreach (string item in parameter.Items)
            {
                // If the const name starts with number then prepend n_
                string strItem = string.Empty;
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
                enumBlock += $"\t{strItem}";
                
                if (i != (parameter.Items.Count - 1))
                {
                    enumBlock +=  "," + "\n";
                }
                else
                {
                    enumBlock += "\n";
                }
                
                ++i;
            }
            enumBlock += "}" + "\n";
            return enumBlock;
        }
    
        // Section 1
        private void section1()
        {
            // Section 1
            // e.g.
            // ## What TIME_SERIES_INTRADAY does?
            str_content += $"## What {operation.Name} does?" + "\n";

            // e.g.
            // This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified.
            // The related REST API documentation is [here](https://www.alphavantage.co/documentation/#intraday)
            str_content += operation.Description + EndSection(true);
        }
        
        // Section 2
        private void section2()
        {
            // ## First thing to do when using TIME_SERIES_INTRADAY
            str_content += $"## Including the {operation.Name} namespace" + "\n";

            //e.g
            // The very first to do before diving into TIME_SERIES_INTRADAY calls is to include the right namespace.  
            str_content += $"The very first thing to do before diving into {operation.Name} calls is to include the right namespace." + DoubleNewLine();

            // e.g.
            // ```
            //
            // using Avapi.AvapiTIME_SERIES_INTRADAY;
            //
            // ```
            //
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += $"using Avapi.Avapi{operation.Name}" + "\n";
            str_content += "\n";
            str_content += "```" + "\n";
            str_content += "\n";
        }

        // Section 3
        private void section3()
        {
            // e.g. 
            // ## How to get a TIME_SERIES_INTRADAY object?
            str_content += $"## How to get a {operation.Name} object?" + "\n";
            str_content += $"The {operation.Name} object is retrieved from the Connection object." + DoubleNewLine();
            str_content += "The snippet below shows how to get the Connection object:" + "\n";
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += "..." + "\n";
            str_content += "IAvapiConnection connection = AvapiConnection.Instance" + "\n";
            str_content += "connection.Connect(\"Your Alpha Vantage API Key !!!!\");" + "\n";
            str_content += "..." + "\n";
            str_content += "\n";
            str_content += "```" + "\n";
            str_content += $"Once you got the Connection object you can extract the {operation.Name} from it." + "\n";

            // e.g.
            //
            // ```
            //
            // ...
            // Int_TIME_SERIES_INTRADAY timeseriesintraday =
            // connection.GetQueryObject_TIME_SERIES_INTRADAY();
            //
            // ```
            //
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += "..." + "\n";
            str_content += $"Int_{operation.Name} {operation.Name.Replace("_", "").ToLower()} = " + "\n";
            str_content += $"\tconnection.GetQueryObject_{operation.Name}();" + "\n";
            str_content += "\n";
            str_content += "```" + "\n";
            str_content += "\n";
        }

        // Section 4
        private void section4()
        {
            // e.g.
            // ## Perform a TIME_SERIES_INTRADAY synchronous Request
            str_content += $"## Perform a {operation.Name} Synchronous Request" + "\n";
            
            if(operation.Parameters.Count > 0)
            {
                // e.g.
                // To perform a TIME_SERIES_INTRADAY request you have 2 options:
                str_content += $"To perform a {operation.Name} request you have 2 options:" + "\n";

                // e.g.
                // 1. The request with constants  
                //
                str_content += $"1. **The request with constants**:" + "\n";
                str_content += "\n";

                // e.g.                
                // ```
                //
                // IAvapiResponse_TIME_SERIES_INTRADAY Query(string symbol,
                // TIME_SERIES_INTRADAY_interval interval,
                // TIME_SERIES_INTRADAY_outputsize size [OPTIONAL], 
                // TIME_SERIES_INTRADAY_datatype type [OPTIONAL]);
                // 
                // ```
                //
                str_content += "```" + "\n";
                str_content += "\n";
                str_content += $"IAvapiResponse_{operation.Name} Query(";
                for (int i = 0; i < operation.Parameters.Count; ++i)
                {
                    if (i > 0)
                    {
                        str_content += "\t\t";
                    }

                    Parameter parameter = operation.Parameters[i];
                    string nameParameter = parameter.Name;
                    bool? mandatoryParameter = parameter.Mandatory;
                    if (parameter.Items.Count != 0)
                    {
                        str_content += $"{operation.Name}_{nameParameter} {nameParameter}";
                    }
                    else
                    {
                        str_content += $"{parameter.DataType} {nameParameter}";
                    }
                    if (!mandatoryParameter.Value)
                    {
                        str_content += " [OPTIONAL]";
                    }
                    if (i < operation.Parameters.Count - 1)
                    {
                        str_content += "," + "\n";
                    }
                }
                str_content += ");" + "\n";
                str_content += "\n";
                str_content += "```  " + "\n";
                str_content += "\n";
            }

            // e.g.
            // 2. The request without constants:
            // ```
            //
            
            if(operation.Parameters.Count > 0)
            {
               str_content += $"2. **The request without constants**:" + "\n";
            }
            else
            {
               str_content +=  $"To perform a {operation.Name} request:" + "\n";
            }
            str_content += "\n";

            // e.g.                
            // ```
            //
            // IAvapiResponse_TIME_SERIES_INTRADAY Query(string symbol,
            // TIME_SERIES_INTRADAY_interval interval,
            // TIME_SERIES_INTRADAY_outputsize size [OPTIONAL], 
            // TIME_SERIES_INTRADAY_datatype type [OPTIONAL]);
            // 
            // ```
            //
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += $"IAvapiResponse_{operation.Name} QueryPrimitive(";
            for (int i = 0; i < operation.Parameters.Count; ++i)
            {
                if (i > 0)
                {
                    str_content += "\t\t";
                }

                Parameter parameter = operation.Parameters[i];
                string nameParameter = parameter.Name;
                bool? mandatoryParameter = parameter.Mandatory;
                str_content += $"string {nameParameter}";
                if (!mandatoryParameter.Value)
                {
                    str_content += " [OPTIONAL]";
                }
                if (i < operation.Parameters.Count - 1)
                {
                    str_content += "," + "\n";
                }
            }
            str_content += ");" + "\n";
            str_content += "\n";
            str_content += "```  " + "\n";
            str_content += "\n";
        }

        // Section 5
        private void section5()
        {
            // e.g.
            // ## Perform a TIME_SERIES_INTRADAY asynchronous Request
            str_content += $"## Perform an {operation.Name} Asynchronous Request" + "\n";

            if (operation.Parameters.Count > 0)
            {
                // e.g.
                // To perform a TIME_SERIES_INTRADAY request you have 2 options:
                str_content += $"To perform an {operation.Name} asynchronous request you have 2 options:" + "\n";

                // e.g.
                // 1. The request with constants  
                //
                str_content += $"1. **The request with constants**:" + "\n";
                str_content += "\n";

                str_content += "```" + "\n";
                str_content += "\n";
                str_content += $"async Task<IAvapiResponse_{operation.Name}> QueryAsync(";
                for (int i = 0; i < operation.Parameters.Count; ++i)
                {
                    if (i > 0)
                    {
                        str_content += "\t\t";
                    }

                    Parameter parameter = operation.Parameters[i];
                    string nameParameter = parameter.Name;
                    bool? mandatoryParameter = parameter.Mandatory;
                    if (parameter.Items.Count != 0)
                    {
                        str_content += $"{operation.Name}_{nameParameter} {nameParameter}";
                    }
                    else
                    {
                        str_content += $"{parameter.DataType} {nameParameter}";
                    }
                    if (!mandatoryParameter.Value)
                    {
                        str_content += " [OPTIONAL]";
                    }
                    if (i < operation.Parameters.Count - 1)
                    {
                        str_content += "," + "\n";
                    }
                }
                str_content += ");" + "\n";
                str_content += "\n";
                str_content += "```  " + "\n";
                str_content += "\n";
            }

             if (operation.Parameters.Count > 0)
            {
                str_content += $"2. **The request without constants**:" + "\n";
            }
            else
            {
                str_content += $"To perform a {operation.Name} request:" + "\n";
            }
            str_content += "\n";

            str_content += "```" + "\n";
            str_content += "\n";
            str_content += $"async Task<IAvapiResponse_{operation.Name}> QueryAsync(";
            for (int i = 0; i < operation.Parameters.Count; ++i)
            {
                if (i > 0)
                {
                    str_content += "\t\t";
                }

                Parameter parameter = operation.Parameters[i];
                string nameParameter = parameter.Name;
                bool? mandatoryParameter = parameter.Mandatory;
                str_content += $"string {nameParameter}";
                if (!mandatoryParameter.Value)
                {
                    str_content += " [OPTIONAL]";
                }
                if (i < operation.Parameters.Count - 1)
                {
                    str_content += "," + "\n";
                }
            }
            str_content += ");" + "\n";
            str_content += "\n";
            str_content += "```  " + "\n";
            str_content += "\n";
        }

        // Section 5
        private void section6()
        {
            // ### Parameters
            str_content += "### Parameters" + "\n";
            if (operation.Parameters.Count == 0)
            {
                // e.g.
                // For the {operation.Name} request no parameters are needed." + NewLine();  
                str_content += $"For the {operation.Name} request no parameters are needed." + NewLine();
            }
            else
            {
                // e.g.
                // The parameters below are needed to perform the TIME_SERIES_INTRADAY request.
                str_content += $"The parameters below are needed to perform the {operation.Name} request." + NewLine();
                foreach (Parameter parameter in operation.Parameters)
                {
                    // e.g.
                    // * **symbol**: The name of the equity of your choice.
                    // * **interval [OPTIONAL]**: The time interval between two consecutive data points in the time series.
                    // * **size [OPTIONAL]**: It is a optional value; compact and full are accepted with the following specifications: compact returns only the latest 100 data points in the intraday time series; full returns the full-length intraday time series. The "compact" option is recommended if you would like to reduce the data size of each API call.
                    // * **type [OPTIONAL]**: It is a optional value; json and csv are accepted with the following specifications: json returns the intraday time series in JSON format; csv returns the time series as a CSV (comma separated value) file.
                    str_content += $"* **{printParameterNameAndOPTIONAL(parameter)}**: {printDescription(parameter)}" + "\n";
                }

                // e.g.
                // Please notice that the info above are copied from the official documentation, that you can find [here](https://www.alphavantage.co/documentation/).
                //
                str_content += "\n";
                str_content += "Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).";
            }
            str_content += EndSection();
        }

        // Section 7
        private void section7()
        {
            if(operation.Parameters.Count > 0)
            {
                // ## The request with constants 
                // The request with constants implies the use of different enums:
                str_content += "## The request with constants" + "\n";
                str_content += "The request with constants implies the use of different enums:" + "\n";

                // e.g. 
                // * TIME_SERIES_INTRADAY_interval
                // * TIME_SERIES_INTRADAY_outputsize
                // * TIME_SERIES_INTRADAY_datatype
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Items.Count > 0)
                    {
                        str_content += $"* **{operation.Name}_{parameter.Name}**" + "\n";
                    }
                }
                
                str_content += "\n";

                foreach (Parameter parameter in operation.Parameters)
                {
                    string enumBlock = string.Empty;
                    if (parameter.Items.Count > 0)
                    {
                        // e.g.
                        // **TIME_SERIES_INTRADAY_interval**: 
                        str_content += $"**{operation.Name}_{parameter.Name}**: ";

                        // e.g.
                        // The time interval between two consecutive data points in the time series
                        // ```
                        // 
                        str_content += $"{parameter.Description}" + "\n";

                        // e.g.
                        // ```
                        // public enum TIME_SERIES_INTRADAY_datatype
                        // {
                        //     none,
                        //     json,
                        //     csv
                        // }
                        //
                        // ```  
                        str_content += "```" + DoubleNewLine();
                        str_content += BuildEnumBlock(operation, parameter);
                        str_content += "\n";
                        str_content += "```" + "  " + "\n";
                    }
                }
                // ***
                str_content += EndSection();
            }
        }

        // Section 8
        private void section8()
        {
            // ## TIME_SERIES_INTRADAY Response?
            str_content += $"## {operation.Name} Response" + "\n";
            str_content += $"The response of a {operation.Name} request is an object that implements the **IAvapiResponse_{operation.Name}** interface." + "\n";
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += $"public interface IAvapiResponse_{operation.Name}" + "\n";
            str_content += "{" + "\n";
            str_content += "    string RawData" + "\n";
            str_content += "    {" + "\n";
            str_content += "        get;" + "\n";
            str_content += "    }" + "\n";
            str_content += $"    IAvapiResponse_{operation.Name}_Content Data" + "\n";
            str_content += "    {" + "\n";
            str_content += "        get;" + "\n";
            str_content += "    }" + "\n";
            str_content += "}" + "\n";
            str_content += "\n";
            str_content += "```" + "\n";
            str_content += $"The **IAvapiResponse_{operation.Name}** interface has two members: RawData and Data." + "\n";
            str_content += "* **RawData**: represents the json response in string format." + "\n";
            str_content += $"* **Data**: It represents the parsed response in an object implementing the interface **IAvapiResponse_{operation.Name}_Content**." + "\n";
            str_content += EndSection();
        }

        // Section 9
        private void section9()
        {
            str_content += $"## Complete Example of a Console App: Display the result of a {operation.Name} request by using the method **Query** (synchronous request)" + "\n";
            str_content += "```" + "\n";
            str_content += "\n";
            str_content += "using System;" + "\n";
            str_content += "using System.IO;" + "\n";
            str_content +=$"using Avapi.Avapi{operation.Name};" + "\n";
            str_content += "" + "\n";
            str_content += "namespace Avapi" + "\n";
            str_content += "{" + "\n";
            str_content += "    public class Example" + "\n";
            str_content += "    {" + "\n";
            str_content += "        static void Main()" + "\n";
            str_content += "        {" + "\n";
            str_content += "            // Creating the connection object" + "\n";
            str_content += "            IAvapiConnection connection = AvapiConnection.Instance;" + "\n";
            str_content += "\n";
            str_content += "            // Set up the connection and pass the API_KEY provided by alphavantage.co" + "\n";
            str_content += "            connection.Connect(\"Your Alpha Vantage API Key !!!!\");" + "\n";
            str_content += "\n";
            str_content += $"            // Get the {operation.Name} query object" + "\n";
            str_content += $"            Int_{operation.Name} {operation.Name.ToLower()} =" + "\n";
            str_content += $"                connection.GetQueryObject_{operation.Name}();" + "\n";
            str_content += "\n";
            str_content += $"            // Perform the {operation.Name} request and get the result" + "\n";
            str_content += $"            IAvapiResponse_{operation.Name} {operation.Name.ToLower()}Response = " + "\n";
            str_content += $"            {operation.Name.ToLower()}.Query(";

            if (operation.Type == "FX_RESPONSE")
            {
                str_content += "\n                 \"GBP\",\"EUR\"";
            }
            else if (operation.Type == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += "\n                 \"BTC\", \"CNY\", null";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += "\n                \"MSFT,FB\", null";
            }
            else if (operation.Parameters.Count > 0)
            {
                // We should distinguish between type of requests SECTOR, TIMESERIES and TECHNICAL HERE (as it is done above for FX)
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Name.Equals("symbol"))
                    {
                        str_content += "\n                 \"MSFT\",";
                        continue;
                    }
                    if (parameter.Items.Count > 0)
                    {
                        str_content += $"\n                 Const_{operation.Name}.{operation.Name}_{parameter.Name}.";

                        if (parameter.Items.Count > 1)
                        {
                            string item = parameter.Items.ElementAt(1);
                            if (Char.IsDigit(item[0]))
                            {
                                str_content += "n_";
                            }
                            str_content += item + ",";
                        }
                        else
                        {
                            string item = parameter.Items.ElementAt(0);
                            if (Char.IsDigit(item[0]))
                            {
                                str_content += "n_";
                            }
                            str_content += item + ",";
                        }
                    }
                    else
                    {
                        switch (parameter.DataType)
                        {
                            case "float":
                                str_content += "\n                 0.2f,";
                                break;
                            case "int":
                                str_content += "\n                 10,";
                                break;

                            default:
                                str_content += "\n                 null,";
                                break;
                        }
                    }
                }
                str_content = str_content.Remove(str_content.Length - 1);
            }
            str_content += ");" + "\n";
            str_content += "\n";
            str_content += "            // Printout the results" + "\n";
            str_content +=$"            Console.WriteLine(\"******** RAW DATA {operation.Name} ********\");" + "\n";
            str_content +=$"            Console.WriteLine({operation.Name.ToLower()}Response.RawData);" + "\n";
            str_content += "" + "\n";


            if(operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content +=$"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content +=$"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content +=$"                Console.WriteLine(\"{node.Name}: \" + data.MetaData.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "                Console.WriteLine(\"========================\");" + "\n";

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
                str_content += "                foreach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content +=$"                    Console.WriteLine(\"{node.Name}: \" + timeseries.{node.Name});" + "\n";
                }
                str_content += "                    Console.WriteLine(\"========================\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content +=$"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content +=$"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                str_content +=$"                Console.WriteLine(\"{node.Name}: \" + data.MetaData.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "                Console.WriteLine(\"========================\");" + "\n";

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
                str_content += "                foreach (var technical in data.TechnicalIndicator)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in technicalIndicatorNodes)
                {
                str_content +=$"                    Console.WriteLine(\"{node.Name}: \" + technical.{node.Name});" + "\n";
                }
                str_content += "                    Console.WriteLine(\"========================\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";                
            }
            else if (operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content +=$"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content +=$"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content +=$"                Console.WriteLine(\"{node.Name}: \" + data.MetaData.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                
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
                    str_content += "                Console.WriteLine(\"RankName: \" + "
                    + $"data.{RankNode[i].Name}.RankName"+ ");" + "\n";

                    for(int j = 0 ; j < RankNode[i].ListNode.Count ;j++ )
                    {
                        str_content += "                Console.WriteLine(\""
                        +$"{RankNode[i].ListNode[j].Name} : \" + "
                        + $"data.{RankNode[i].Name}.{RankNode[i].ListNode[j].Name}"+ ");" + "\n";
                    }
                    str_content += "                Console.WriteLine(\"========================\");" + "\n";
                }

                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n"; 
            }
            else if(operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += $"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content += $"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in currencyExchangeNode)
                {
                    str_content += $"                Console.WriteLine(\"{node.Name}: \" + data.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if(operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += $"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content += $"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                Console.WriteLine(\"{node.Name}: \" + data.MetaData.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "                Console.WriteLine(\"========================\");" + "\n";

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
                str_content += "                foreach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"                    Console.WriteLine(\"{node.Name}: \" + timeseries.{node.Name});" + "\n";
                }
                str_content += "                    Console.WriteLine(\"========================\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += $"            Console.WriteLine(\"******** STRUCTURED DATA {operation.Name} ********\");" + "\n";
                str_content += $"            var data = {operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                Console.WriteLine(data.ErrorMessage);" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                Console.WriteLine(\"{node.Name}: \" + data.MetaData.{node.Name});" + "\n";
                }
                str_content += "                Console.WriteLine(\"========================\");" + "\n";
                str_content += "                Console.WriteLine(\"========================\");" + "\n";

                doc = XDocument.Parse(operation.Response.StockQuotes);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                foreach (var stockQuotes in data.StockQuotes)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"                    Console.WriteLine(\"{node.Name}: \" + stockQuotes.{node.Name});" + "\n";
                }
                str_content += "                    Console.WriteLine(\"========================\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }

            str_content += "```" + "\n";
        }

        // Section 10
        private void section10()
        {
            str_content += $"## Complete Example of a Windows Form App: Display the result of a {operation.Name} request by using the method **QueryAsync** (asynchronous request)" + "\n";
            str_content += "```" + "\n";
            str_content += "\n";

            str_content += "using Avapi;" + "\n";
            str_content +=$"using Avapi.Avapi{operation.Name}" + "\n";
            str_content += "using System;" + "\n";
            str_content += "using System.Windows.Forms;" + "\n";
            str_content += "" + "\n";
            str_content += "namespace WindowsFormsApp1" + "\n";
            str_content += "{" + "\n";
            str_content += "    public partial class Form1 : Form" + "\n";
            str_content += "    {" + "\n";
            str_content += "        private IAvapiConnection m_connection = AvapiConnection.Instance;" + "\n";
            str_content +=$"        private Int_{operation.Name} m_{operation.Name.ToLower()};" + "\n";
            str_content +=$"        private IAvapiResponse_{operation.Name} m_{operation.Name.ToLower()}Response;" + "\n";
            str_content += "" + "\n";
            str_content += "        public Form1()" + "\n";
            str_content += "        {" + "\n";
            str_content += "            InitializeComponent();" + "\n";
            str_content += "        }" + "\n";
            str_content += "" + "\n";
            str_content += "        protected override void OnLoad(EventArgs e)" + "\n";
            str_content += "        {" + "\n";
            str_content += "            // Set up the connection and pass the API_KEY provided by alphavantage.co" + "\n";
            str_content += "            m_connection.Connect(\"Your Alpha Vantage Key\");" + "\n";
            str_content += "" + "\n";
            str_content +=$"            // Get the {operation.Name} query object" + "\n";
            str_content +=$"            m_{operation.Name.ToLower()} = m_connection.GetQueryObject_{operation.Name}();" + "\n";
            str_content += "" + "\n";
            str_content += "            base.OnLoad(e);" + "\n";
            str_content += "        }" + "\n\n";
            str_content +=$"        private async void {operation.Name}AsyncButton_Click(object sender, EventArgs e)" + "\n";
            str_content += "        {" + "\n";
            str_content += $"            // Perform the {operation.Name} request and get the result" + "\n";
            str_content += $"            m_{operation.Name.ToLower()}Response = " + "\n";
            str_content += $"                await m_{operation.Name.ToLower()}.QueryAsync(";

            if (operation.Type == "FX_RESPONSE")
            {
                str_content += "\n                    \"GBP\",\"EUR\"";
            }
            else if (operation.Type == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += "\n                     \"BTC\", \"CNY\", null";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += "\n                    \"MSFT,FB\", null";
            }
            else if (operation.Parameters.Count > 0)
            {
                // We should distinguish between type of requests SECTOR, TIMESERIES and TECHNICAL HERE (as it is done above for FX)
                foreach (Parameter parameter in operation.Parameters)
                {
                    if (parameter.Name.Equals("symbol"))
                    {
                        str_content += "\n                     \"MSFT\",";
                        continue;
                    }
                    if (parameter.Items.Count > 0)
                    {
                        str_content += $"\n                     Const_{operation.Name}.{operation.Name}_{parameter.Name}.";

                        if (parameter.Items.Count > 1)
                        {
                            string item = parameter.Items.ElementAt(1);
                            if (Char.IsDigit(item[0]))
                            {
                                str_content += "n_";
                            }
                            str_content += item + ",";
                        }
                        else
                        {
                            string item = parameter.Items.ElementAt(0);
                            if (Char.IsDigit(item[0]))
                            {
                                str_content += "n_";
                            }
                            str_content += item + ",";
                        }
                    }
                    else
                    {
                        switch (parameter.DataType)
                        {
                            case "float":
                                str_content += "\n                     0.2f,";
                                break;
                            case "int":
                                str_content += "\n                     10,";
                                break;

                            default:
                                str_content += "\n                     null,";
                                break;
                        }
                    }
                }
                str_content = str_content.Remove(str_content.Length - 1);
            }
            str_content += ");" + "\n";
            str_content += "\n";

            str_content += "             // Show the results" + "\n";
            str_content += $"            resultTextBox.AppendText(\"******** RAW DATA {operation.Name} ********\" + \"\\n\");" + "\n";
            str_content += $"            resultTextBox.AppendText(m_{operation.Name.ToLower()}Response.RawData + \"\\n\");" + "\n";
            str_content += "" + "\n";


            if (operation.Response.TypeResponse == "TIME_SERIES_DATA_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.MetaData.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";

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
                str_content += "                foreach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"                    resultTextBox.AppendText(\"{node.Name}: \" + timeseries.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                    resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "TECHNICAL_INDICATOR_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.MetaData.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";

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
                str_content += "                foreach (var technical in data.TechnicalIndicator)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in technicalIndicatorNodes)
                {
                    str_content += $"                    resultTextBox.AppendText(\"{node.Name}: \" + technical.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                    resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "SECTOR_PERFORMANCES_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";

                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.MetaData.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";

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

                for (int i = 0; i < RankNode.Count; ++i)
                {
                    str_content += "                resultTextBox.AppendText(\"RankName: \" + "
                    + $"data.{RankNode[i].Name}.RankName" + " + \"\\n\");" + "\n";

                    for (int j = 0; j < RankNode[i].ListNode.Count; j++)
                    {
                        str_content += "                resultTextBox.AppendText(\""
                        + $"{RankNode[i].ListNode[j].Name} : \" + "
                        + $"data.{RankNode[i].Name}.{RankNode[i].ListNode[j].Name}" + " + \"\\n\");" + "\n";
                    }
                    str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                }

                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "FX_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.CurrencyExchange);
                IList<Node> currencyExchangeNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in currencyExchangeNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "DIGITAL_CRYPTO_TIME_SERIES_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.MetaData.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";

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
                str_content += "                foreach (var timeseries in data.TimeSeries)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"                    resultTextBox.AppendText(\"{node.Name}: \" + timeseries.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                    resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }
            else if (operation.Response.TypeResponse == "BATCH_STOCK_QUOTES_RESPONSE")
            {
                str_content += $"            resultTextBox.AppendText(\"******** STRUCTURED DATA {operation.Name} ********\" + \"\\n\");" + "\n";
                str_content += $"            var data = m_{operation.Name.ToLower()}Response.Data;" + "\n";
                str_content += "            if (data.Error)" + "\n";
                str_content += "            {" + "\n";
                str_content += "                resultTextBox.AppendText(data.ErrorMessage + \"\\n\");" + "\n";
                str_content += "            }" + "\n";
                str_content += "            else" + "\n";
                str_content += "            {" + "\n";


                XDocument doc = XDocument.Parse(operation.Response.MetaData);
                IList<Node> metaDataNode = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                foreach (Node node in metaDataNode)
                {
                    str_content += $"                resultTextBox.AppendText(\"{node.Name}: \" + data.MetaData.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";

                doc = XDocument.Parse(operation.Response.StockQuotes);
                IList<Node> timeSeriesNodes = doc.Root.Elements("node")
                    .Select(x => new Node
                    {
                        Name = (string)x.Element("name"),
                        Text = (string)x.Element("text")
                    }).ToList();

                str_content += "                foreach (var stockQuotes in data.StockQuotes)" + "\n";
                str_content += "                {" + "\n";

                foreach (Node node in timeSeriesNodes)
                {
                    str_content += $"                    resultTextBox.AppendText(\"{node.Name}: \" + stockQuotes.{node.Name} + \"\\n\");" + "\n";
                }
                str_content += "                    resultTextBox.AppendText(\"========================\" + \"\\n\");" + "\n";
                str_content += "                }" + "\n";
                str_content += "            }" + "\n";
                str_content += "        }" + "\n";
                str_content += "    }" + "\n";
                str_content += "}" + "\n";
                str_content += "\n";
            }

            str_content += "```" + "\n";
        }


        // generate the documentation related one operation.
        public bool generate_documentation()
        {
            section1();
            section2();
            section3();
            section4();
            section5();
            section6();
            section7();
            section8();
            section9();
            section10();


            create();

            return true;
        }

        // create the Interface .cs file
        private int create()
        {
            using (var fileStream = new FileStream($"{path}/{operation.Name}.md", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(str_content);
            }
            return 0;
        } 
    }
}




















 