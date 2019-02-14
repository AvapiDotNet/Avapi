using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMACD
{
    internal class AvapiResponse_MACD : IAvapiResponse_MACD
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

        public IAvapiResponse_MACD_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MACD
    {
        public string Symbol
        {
            internal set;
            get;
        }

        public string Indicator
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

        public string FastPeriod
        {
            internal set;
            get;
        }

        public string SlowPeriod
        {
            internal set;
            get;
        }

        public string SignalPeriod
        {
            internal set;
            get;
        }

        public string SeriesType
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

    public class TechnicalIndicator_Type_MACD
    {
        public string MACD_Hist
        {
            internal set;
            get;
        }

        public string MACD_Signal
        {
            internal set;
            get;
        }

        public string MACD
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

    internal class AvapiResponse_MACD_Content : IAvapiResponse_MACD_Content
    {
        internal AvapiResponse_MACD_Content()
        {
           MetaData = new MetaData_Type_MACD();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MACD>();
        }

       public MetaData_Type_MACD MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MACD> TechnicalIndicator
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

	public class Impl_MACD : Int_MACD
	{
		const string s_function = "MACD";

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

		private static readonly Lazy<Impl_MACD> s_Impl_MACD =
			new Lazy<Impl_MACD>(() => new Impl_MACD());
		public static Impl_MACD Instance
		{
			get
			{
				return s_Impl_MACD.Value;
			}
		}
		private Impl_MACD()
		{
		}

		internal static readonly IDictionary s_MACD_interval_translation
			 = new Dictionary<Const_MACD.MACD_interval, string>()
		{
			{
				Const_MACD.MACD_interval.none,
				null
			},
			{
				Const_MACD.MACD_interval.n_1min,
				"1min"
			},
			{
				Const_MACD.MACD_interval.n_5min,
				"5min"
			},
			{
				Const_MACD.MACD_interval.n_15min,
				"15min"
			},
			{
				Const_MACD.MACD_interval.n_30min,
				"30min"
			},
			{
				Const_MACD.MACD_interval.n_60min,
				"60min"
			},
			{
				Const_MACD.MACD_interval.daily,
				"daily"
			},
			{
				Const_MACD.MACD_interval.weekly,
				"weekly"
			},
			{
				Const_MACD.MACD_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_MACD_series_type_translation
			 = new Dictionary<Const_MACD.MACD_series_type, string>()
		{
			{
				Const_MACD.MACD_series_type.none,
				null
			},
			{
				Const_MACD.MACD_series_type.close,
				"close"
			},
			{
				Const_MACD.MACD_series_type.open,
				"open"
			},
			{
				Const_MACD.MACD_series_type.high,
				"high"
			},
			{
				Const_MACD.MACD_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_MACD Query(
			string symbol,
			Const_MACD.MACD_interval interval,
			Const_MACD.MACD_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1)
		{
			string current_interval = s_MACD_interval_translation[interval] as string;
			string current_series_type = s_MACD_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,current_series_type,fastperiod,slowperiod,signalperiod);
		}

		public async Task<IAvapiResponse_MACD> QueryAsync(
			string symbol,
			Const_MACD.MACD_interval interval,
			Const_MACD.MACD_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1)
		{
			string current_interval = s_MACD_interval_translation[interval] as string;
			string current_series_type = s_MACD_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type,fastperiod,slowperiod,signalperiod);
		}


		public IAvapiResponse_MACD QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			getParameters.Add(new KeyValuePair<string, string>("fastperiod",fastperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowperiod",slowperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("signalperiod",signalperiod.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_MACD ret = new AvapiResponse_MACD
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MACD> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			getParameters.Add(new KeyValuePair<string, string>("fastperiod",fastperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowperiod",slowperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("signalperiod",signalperiod.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_MACD ret = new AvapiResponse_MACD
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MACD_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MACD_Content ret = new AvapiResponse_MACD_Content();
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
                ret.MetaData.Symbol = (string)metaData["1: Symbol"];
                ret.MetaData.Indicator = (string)metaData["2: Indicator"];
                ret.MetaData.LastRefreshed = (string)metaData["3: Last Refreshed"];
                ret.MetaData.Interval = (string)metaData["4: Interval"];
                ret.MetaData.FastPeriod = (string)metaData["5.1: Fast Period"];
                ret.MetaData.SlowPeriod = (string)metaData["5.2: Slow Period"];
                ret.MetaData.SignalPeriod = (string)metaData["5.3: SignalPeriod"];
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MACD"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MACD technicalindicator = new TechnicalIndicator_Type_MACD
                    {
                        DateTime = ((JProperty)result).Name,
                        MACD_Hist = (string)result.First["MACD_Hist"],
                        MACD_Signal = (string)result.First["MACD_Signal"],
                        MACD = (string)result.First["MACD"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}