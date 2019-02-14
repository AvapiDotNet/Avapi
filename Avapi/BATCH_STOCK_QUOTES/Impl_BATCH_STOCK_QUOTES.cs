using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiBATCH_STOCK_QUOTES
{
    internal class AvapiResponse_BATCH_STOCK_QUOTES : IAvapiResponse_BATCH_STOCK_QUOTES
    {
        public string LastHttpRequest
        {
            get;
            internal set;

        }
        public string RawData
        {
            get;
            internal set;
        }

        public IAvapiResponse_BATCH_STOCK_QUOTES_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_BATCH_STOCK_QUOTES
    {
        public string Information
        {
            internal set;
            get;
        }

        public string Notes
        {
            internal set;
            get;
        }

        public string TimeZone
        {
            internal set;
            get;
        }

    }

    public class StockQuotes_Type_BATCH_STOCK_QUOTES
    {
        [JsonProperty("1. symbol")]
        public string Symbol
        {
            internal set;
            get;
        }

        [JsonProperty("2. price")]
        public string Price
        {
            internal set;
            get;
        }

        [JsonProperty("3. volume")]
        public string Volume
        {
            internal set;
            get;
        }

        [JsonProperty("4. timestamp")]
        public string TimeStamp
        {
            internal set;
            get;
        }

    }

    internal class AvapiResponse_BATCH_STOCK_QUOTES_Content : IAvapiResponse_BATCH_STOCK_QUOTES_Content
    {
        internal AvapiResponse_BATCH_STOCK_QUOTES_Content()
        {
           MetaData = new MetaData_Type_BATCH_STOCK_QUOTES();
           StockQuotes = new List<StockQuotes_Type_BATCH_STOCK_QUOTES>();
        }

       public MetaData_Type_BATCH_STOCK_QUOTES MetaData
        {
            internal set;
            get;
        }

       public IList<StockQuotes_Type_BATCH_STOCK_QUOTES> StockQuotes
        {
            internal set;
            get;
        }

        public bool Error
        {
            internal set;
            get;
        }

        public string ErrorMessage
        {
            internal set;
            get;
        }
    }

	public class Impl_BATCH_STOCK_QUOTES : Int_BATCH_STOCK_QUOTES
	{
		const string s_function = "BATCH_STOCK_QUOTES";

		internal static string ApiKey
		{
			get;
			set;
		}

		internal static HttpClient RestClient
		{
			get;
			set;
		}

		internal static string AvapiUrl
		{
			get;
			set;
		}

		private static readonly Lazy<Impl_BATCH_STOCK_QUOTES> s_Impl_BATCH_STOCK_QUOTES =
			new Lazy<Impl_BATCH_STOCK_QUOTES>(() => new Impl_BATCH_STOCK_QUOTES());
		public static Impl_BATCH_STOCK_QUOTES Instance
		{
			get
			{
				return s_Impl_BATCH_STOCK_QUOTES.Value;
			}
		}
		private Impl_BATCH_STOCK_QUOTES()
		{
		}


		public IAvapiResponse_BATCH_STOCK_QUOTES QueryPrimitive(
			string symbols)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbols",symbols));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_BATCH_STOCK_QUOTES ret = new AvapiResponse_BATCH_STOCK_QUOTES
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_BATCH_STOCK_QUOTES> QueryPrimitiveAsync(
			string symbols)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbols",symbols));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_BATCH_STOCK_QUOTES ret = new AvapiResponse_BATCH_STOCK_QUOTES
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_BATCH_STOCK_QUOTES_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_BATCH_STOCK_QUOTES_Content ret = new AvapiResponse_BATCH_STOCK_QUOTES_Content();
            JObject jsonInputParsed = JObject.Parse(jsonInput);
            string errorMessage = (string)jsonInputParsed["Error Message"];
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ret.Error = true;
                ret.ErrorMessage = errorMessage;
            }
            else
            {
                JToken metaData = jsonInputParsed["Meta Data"];
                ret.MetaData.Information = (string)metaData["1. Information"];
                ret.MetaData.Notes = (string)metaData["2. Notes"];
                ret.MetaData.TimeZone = (string)metaData["3. Time Zone"];
                ret.StockQuotes = JsonConvert.DeserializeObject<List<StockQuotes_Type_BATCH_STOCK_QUOTES>>(jsonInputParsed["Stock Quotes"].ToString());
            }
            return ret;
        }
	}
}