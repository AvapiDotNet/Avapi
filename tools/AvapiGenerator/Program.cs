using System; 
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AvapiGenerator
{   
    public class Parameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Mandatory { get; set; }
        public string DataType { get; set; }
        public IList<string> Items { get; set; }

    }

    public class Operation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public IList<Parameter> Parameters { get; set; }
        public Response Response { get; set; }
	}

    public class Response
    {
        public string TypeResponse { get; set; }
        public string MetaData { get; set; }
        public string TimeSeries { get; set; }
        public string TechnicalIndicator { get; set; }
        public string SectorP { get; set; }
        public string CurrencyExchange { get; set; }
        public string StockQuotes { get; set; }
    }

    public class Node
    {
        internal string Name { get; set; }
        internal string Text { get; set; }
    }

    public class Rank
    {
        internal string Name { get; set; }
        internal string Text { get; set; }
        public IList<Node> ListNode { get; set; }
    }

    class Program
    {
		static string xmlInputFile = "avapi.xml";
        static string path_version = "version.txt";
        static string destinationPath = @"C:\Avapi";
        static string examplePath = @"C:\AvapiIntegrationTest";
        static string documentationPath = @"C:\Avapi.wiki";

        
		static IList<Operation> operations {get;set;}

        internal static IList<Operation> ParseOperations(string path)
		{
			XDocument doc = null;
            
            try
            { 
              doc = XDocument.Load(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
				Environment.Exit(-1);
            }

            return doc.Root
                  .Elements("operation")
                  .Select(x => new Operation
                  {
                      Name = (string)x.Element("name"),
                      Description = (string)x.Element("description"),
                      Type = (string)x.Element("type"),
                      Parameters = x.Elements("parameter")
                        .Select(y => new Parameter
                        {
                            Name = (string)y.Attribute("name"),
                            Description = (string)y.Element("description"),
                            Mandatory = ToNullableBool((string)y.Element("mandatory")),
                            DataType = (string)y.Element("datatype"),
                            Items = y.Elements("items")
                                .Elements("item")
                                .Select(z => z.Value)
                                .ToList()
                        }).ToList(),
                      Response = x.Elements("response")
                        .Select(c => new Response
                        {
                            TypeResponse = (string)c.Element("typeresponse"),
                            MetaData = GetInnerXml(c.Element("metadata")),
                            TimeSeries = GetInnerXml(c.Element("timeseries")),
                            TechnicalIndicator = GetInnerXml(c.Element("technicalindicator")),
                            SectorP = GetInnerXml(c.Element("sectorperformances")),
                            CurrencyExchange = GetInnerXml(c.Element("currencyexchange")),
                            StockQuotes = GetInnerXml(c.Element("stockquotes"))
                        }).ToList()[0],
				  }).ToList();
		}


        internal static string GetInnerXml(XElement element)
        {
            if(element == null)
            {
                return null;
            }
            var reader = element.CreateReader();
            reader.MoveToContent();
            return reader.ReadOuterXml();
        }
        internal static bool? ToNullableBool(string str)
        {
            bool i;
            if (bool.TryParse(str, out i)) return i;
            return null;
        }

        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
	            xmlInputFile = args[0];
                path_version = args[1];
                destinationPath = args[2];
                examplePath = args[3];
                documentationPath = args[4];
	        }

            try
            { 
                operations = ParseOperations(xmlInputFile);
            
                if(operations == null)
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
				return;
            } 

            CodeGenerator.initCodeGenerator(destinationPath, operations, path_version);
			if (!CodeGenerator.GenerateCode())
			{
				// Delete folder destinationPath/AVAPI
			}

            DocGenerator.initDocGenerator(operations , documentationPath);
			if (!DocGenerator.GenerateDocumentation())
			{
                // todo the error handler
			}

            ExampleGenerator.initExampleGenerator(operations , examplePath);
			if (!ExampleGenerator.GenerateExample())
			{
                 // todo the error handler
			}
        }
    }
}
