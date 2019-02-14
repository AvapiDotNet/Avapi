using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiTIME_SERIES_INTRADAY
{
    internal class AvapiResponse_TIME_SERIES_INTRADAY : IAvapiResponse_TIME_SERIES_INTRADAY
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

        public IAvapiResponse_TIME_SERIES_INTRADAY_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_TIME_SERIES_INTRADAY
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

        public string Interval
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

    public class TimeSeries_Type_TIME_SERIES_INTRADAY
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

        public string volume
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

    internal class AvapiResponse_TIME_SERIES_INTRADAY_Content : IAvapiResponse_TIME_SERIES_INTRADAY_Content
    {
        internal AvapiResponse_TIME_SERIES_INTRADAY_Content()
        {
           MetaData = new MetaData_Type_TIME_SERIES_INTRADAY();
           TimeSeries = new List<TimeSeries_Type_TIME_SERIES_INTRADAY>();
        }

       public MetaData_Type_TIME_SERIES_INTRADAY MetaData
        {
            internal set;
            get;
        }

       public IList<TimeSeries_Type_TIME_SERIES_INTRADAY> TimeSeries
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

	public class Impl_TIME_SERIES_INTRADAY : Int_TIME_SERIES_INTRADAY
	{
		const string s_function = "TIME_SERIES_INTRADAY";

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

		private static readonly Lazy<Impl_TIME_SERIES_INTRADAY> s_Impl_TIME_SERIES_INTRADAY =
			new Lazy<Impl_TIME_SERIES_INTRADAY>(() => new Impl_TIME_SERIES_INTRADAY());
		public static Impl_TIME_SERIES_INTRADAY Instance
		{
			get
			{
				return s_Impl_TIME_SERIES_INTRADAY.Value;
			}
		}
		private Impl_TIME_SERIES_INTRADAY()
		{
		}

		internal static readonly IDictionary s_TIME_SERIES_INTRADAY_interval_translation
			 = new Dictionary<Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval, string>()
		{
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.none,
				null
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_1min,
				"1min"
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_5min,
				"5min"
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_15min,
				"15min"
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_30min,
				"30min"
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min,
				"60min"
			}
		};

		internal static readonly IDictionary s_TIME_SERIES_INTRADAY_outputsize_translation
			 = new Dictionary<Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize, string>()
		{
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.none,
				null
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.compact,
				"compact"
			},
			{
				Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.full,
				"full"
			}
		};

		public IAvapiResponse_TIME_SERIES_INTRADAY Query(
			string symbol,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval interval,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize outputsize = Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.none)
		{
			string current_interval = s_TIME_SERIES_INTRADAY_interval_translation[interval] as string;
			string current_outputsize = s_TIME_SERIES_INTRADAY_outputsize_translation[outputsize] as string;

			return QueryPrimitive(symbol,current_interval,current_outputsize);
		}

		public async Task<IAvapiResponse_TIME_SERIES_INTRADAY> QueryAsync(
			string symbol,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval interval,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize outputsize = Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.none)
		{
			string current_interval = s_TIME_SERIES_INTRADAY_interval_translation[interval] as string;
			string current_outputsize = s_TIME_SERIES_INTRADAY_outputsize_translation[outputsize] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,current_outputsize);
		}


		public IAvapiResponse_TIME_SERIES_INTRADAY QueryPrimitive(
			string symbol,
			string interval,
			string outputsize = null)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("outputsize",outputsize));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_TIME_SERIES_INTRADAY ret = new AvapiResponse_TIME_SERIES_INTRADAY
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_TIME_SERIES_INTRADAY> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string outputsize = null)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("outputsize",outputsize));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_TIME_SERIES_INTRADAY ret = new AvapiResponse_TIME_SERIES_INTRADAY
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_TIME_SERIES_INTRADAY_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_TIME_SERIES_INTRADAY_Content ret = new AvapiResponse_TIME_SERIES_INTRADAY_Content();
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
                ret.MetaData.Interval = (string)metaData["4. Interval"];
                ret.MetaData.OutputSize = (string)metaData["5. Output Size"];
                ret.MetaData.TimeZone = (string)metaData["6. Time Zone"];
                string timeSeries = "Time Series (1min)";
                string[] timeSeriesIntervals =
                {
                    "Time Series (1min)",
                    "Time Series (5min)",
                    "Time Series (15min)",
                    "Time Series (30min)",
                    "Time Series (60min)"
                };
                for (int i = 0; i < timeSeriesIntervals.Length; ++i)
                {
                    if (jsonInputParsed[timeSeriesIntervals[i]] != null)
                    {
                        timeSeries = timeSeriesIntervals[i];
                        break;
                    }
                }
                JEnumerable<JToken> results = jsonInputParsed[timeSeries].Children();
                foreach (JToken result in results)
                {
                    TimeSeries_Type_TIME_SERIES_INTRADAY timeseries = new TimeSeries_Type_TIME_SERIES_INTRADAY
                    {
                        DateTime = ((JProperty)result).Name,
                        open = (string)result.First["1. open"],
                        high = (string)result.First["2. high"],
                        low = (string)result.First["3. low"],
                        close = (string)result.First["4. close"],
                        volume = (string)result.First["5. volume"]
                    };
                    ret.TimeSeries.Add(timeseries);
                }
            }
            return ret;
        }
	}
}