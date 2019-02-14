using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED
{
    internal class AvapiResponse_TIME_SERIES_DAILY_ADJUSTED : IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED
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

        public IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_TIME_SERIES_DAILY_ADJUSTED
    {
        public string Information
        {
            internal set;
            get;
        }

        public string Symbol
        {
            internal set;
            get;
        }

        public string LastRefreshed
        {
            internal set;
            get;
        }

        public string OutputSize
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

    public class TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED
    {
        public string open
        {
            internal set;
            get;
        }

        public string high
        {
            internal set;
            get;
        }

        public string low
        {
            internal set;
            get;
        }

        public string close
        {
            internal set;
            get;
        }

        public string adjustedclose
        {
            internal set;
            get;
        }

        public string volume
        {
            internal set;
            get;
        }

        public string dividendamount
        {
            internal set;
            get;
        }

        public string splitcoefficient
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

    internal class AvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content : IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content
    {
        internal AvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content()
        {
           MetaData = new MetaData_Type_TIME_SERIES_DAILY_ADJUSTED();
           TimeSeries = new List<TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED>();
        }

       public MetaData_Type_TIME_SERIES_DAILY_ADJUSTED MetaData
        {
            internal set;
            get;
        }

       public IList<TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED> TimeSeries
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

	public class Impl_TIME_SERIES_DAILY_ADJUSTED : Int_TIME_SERIES_DAILY_ADJUSTED
	{
		const string s_function = "TIME_SERIES_DAILY_ADJUSTED";

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

		private static readonly Lazy<Impl_TIME_SERIES_DAILY_ADJUSTED> s_Impl_TIME_SERIES_DAILY_ADJUSTED =
			new Lazy<Impl_TIME_SERIES_DAILY_ADJUSTED>(() => new Impl_TIME_SERIES_DAILY_ADJUSTED());
		public static Impl_TIME_SERIES_DAILY_ADJUSTED Instance
		{
			get
			{
				return s_Impl_TIME_SERIES_DAILY_ADJUSTED.Value;
			}
		}
		private Impl_TIME_SERIES_DAILY_ADJUSTED()
		{
		}

		internal static readonly IDictionary s_TIME_SERIES_DAILY_ADJUSTED_outputsize_translation
			 = new Dictionary<Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize, string>()
		{
			{
				Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.none,
				null
			},
			{
				Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.compact,
				"compact"
			},
			{
				Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.full,
				"full"
			}
		};

		public IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED Query(
			string symbol,
			Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize outputsize = Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.none)
		{
			string current_outputsize = s_TIME_SERIES_DAILY_ADJUSTED_outputsize_translation[outputsize] as string;

			return QueryPrimitive(symbol,current_outputsize);
		}

		public async Task<IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED> QueryAsync(
			string symbol,
			Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize outputsize = Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.none)
		{
			string current_outputsize = s_TIME_SERIES_DAILY_ADJUSTED_outputsize_translation[outputsize] as string;

			return await QueryPrimitiveAsync(symbol,current_outputsize);
		}


		public IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED QueryPrimitive(
			string symbol,
			string outputsize = null)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("outputsize",outputsize));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED ret = new AvapiResponse_TIME_SERIES_DAILY_ADJUSTED
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED> QueryPrimitiveAsync(
			string symbol,
			string outputsize = null)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("outputsize",outputsize));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED ret = new AvapiResponse_TIME_SERIES_DAILY_ADJUSTED
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content ret = new AvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content();
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
                ret.MetaData.Symbol = (string)metaData["2. Symbol"];
                ret.MetaData.LastRefreshed = (string)metaData["3. Last Refreshed"];
                ret.MetaData.OutputSize = (string)metaData["4. Output Size"];
                ret.MetaData.TimeZone = (string)metaData["5. Time Zone"];
                string timeSeries = "Time Series (Daily)";
                JEnumerable<JToken> results = jsonInputParsed[timeSeries].Children();
                foreach (JToken result in results)
                {
                    TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED timeseries = new TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED
                    {
                        DateTime = ((JProperty)result).Name,
                        open = (string)result.First["1. open"],
                        high = (string)result.First["2. high"],
                        low = (string)result.First["3. low"],
                        close = (string)result.First["4. close"],
                        adjustedclose = (string)result.First["5. adjusted close"],
                        volume = (string)result.First["6. volume"],
                        dividendamount = (string)result.First["7. dividend amount"],
                        splitcoefficient = (string)result.First["8. split coefficient"]
                    };
                    ret.TimeSeries.Add(timeseries);
                }
            }
            return ret;
        }
	}
}