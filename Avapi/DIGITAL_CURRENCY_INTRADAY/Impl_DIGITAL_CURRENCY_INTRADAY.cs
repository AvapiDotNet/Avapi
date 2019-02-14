using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiDIGITAL_CURRENCY_INTRADAY
{
    internal class AvapiResponse_DIGITAL_CURRENCY_INTRADAY : IAvapiResponse_DIGITAL_CURRENCY_INTRADAY
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

        public IAvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_DIGITAL_CURRENCY_INTRADAY
    {
        public string Information
        {
            internal set;
            get;
        }

        public string DigitalCurrencyCode
        {
            internal set;
            get;
        }

        public string DigitalCurrencyName
        {
            internal set;
            get;
        }

        public string MarketCode
        {
            internal set;
            get;
        }

        public string MarketName
        {
            internal set;
            get;
        }

        public string Interval
        {
            internal set;
            get;
        }

        public string LastRefreshed
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

    public class TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY
    {
        public string Price
        {
            internal set;
            get;
        }

        public string PriceUSD
        {
            internal set;
            get;
        }

        public string Volume
        {
            internal set;
            get;
        }

        public string MarketCapUSD
        {
            internal set;
            get;
        }

        public string DateTime
        {
            internal set;
            get;
        }

    }

    internal class AvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content : IAvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content
    {
        internal AvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content()
        {
           MetaData = new MetaData_Type_DIGITAL_CURRENCY_INTRADAY();
           TimeSeries = new List<TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY>();
        }

       public MetaData_Type_DIGITAL_CURRENCY_INTRADAY MetaData
        {
            internal set;
            get;
        }

       public IList<TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY> TimeSeries
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

	public class Impl_DIGITAL_CURRENCY_INTRADAY : Int_DIGITAL_CURRENCY_INTRADAY
	{
		const string s_function = "DIGITAL_CURRENCY_INTRADAY";

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

		private static readonly Lazy<Impl_DIGITAL_CURRENCY_INTRADAY> s_Impl_DIGITAL_CURRENCY_INTRADAY =
			new Lazy<Impl_DIGITAL_CURRENCY_INTRADAY>(() => new Impl_DIGITAL_CURRENCY_INTRADAY());
		public static Impl_DIGITAL_CURRENCY_INTRADAY Instance
		{
			get
			{
				return s_Impl_DIGITAL_CURRENCY_INTRADAY.Value;
			}
		}
		private Impl_DIGITAL_CURRENCY_INTRADAY()
		{
		}


		public IAvapiResponse_DIGITAL_CURRENCY_INTRADAY QueryPrimitive(
			string symbol,
			string market)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("market",market));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_DIGITAL_CURRENCY_INTRADAY ret = new AvapiResponse_DIGITAL_CURRENCY_INTRADAY
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_DIGITAL_CURRENCY_INTRADAY> QueryPrimitiveAsync(
			string symbol,
			string market)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("market",market));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_DIGITAL_CURRENCY_INTRADAY ret = new AvapiResponse_DIGITAL_CURRENCY_INTRADAY
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content ret = new AvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content();
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
                ret.MetaData.DigitalCurrencyCode = (string)metaData["2. Digital Currency Code"];
                ret.MetaData.DigitalCurrencyName = (string)metaData["3. Digital Currency Name"];
                ret.MetaData.MarketCode = (string)metaData["4. Market Code"];
                ret.MetaData.MarketName = (string)metaData["5. Market Name"];
                ret.MetaData.Interval = (string)metaData["6. Interval"];
                ret.MetaData.LastRefreshed = (string)metaData["7. Last Refreshed"];
                ret.MetaData.TimeZone = (string)metaData["8. Time Zone"];
                string timeSeries = "Time Series (Digital Currency Intraday)";
                JEnumerable<JToken> results = jsonInputParsed[timeSeries].Children();
                foreach (JToken result in results)
                {
                    TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY timeseries = new TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY
                    {
                        DateTime = ((JProperty)result).Name,
                        Price = (string)result.First["1a. price (" + ret.MetaData.MarketCode + ")"],
                        PriceUSD = (string)result.First["1b. price (USD)"],
                        Volume = (string)result.First["2. volume"],
                        MarketCapUSD = (string)result.First["3. market cap (USD)"]
                    };
                    ret.TimeSeries.Add(timeseries);
                }
            }
            return ret;
        }
	}
}