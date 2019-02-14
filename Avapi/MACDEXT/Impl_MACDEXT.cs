using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMACDEXT
{
    internal class AvapiResponse_MACDEXT : IAvapiResponse_MACDEXT
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

        public IAvapiResponse_MACDEXT_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MACDEXT
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

        public string FastMAType
        {
            internal set;
            get;
        }

        public string SlowMAType
        {
            internal set;
            get;
        }

        public string SignalMAType
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

    public class TechnicalIndicator_Type_MACDEXT
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

    internal class AvapiResponse_MACDEXT_Content : IAvapiResponse_MACDEXT_Content
    {
        internal AvapiResponse_MACDEXT_Content()
        {
           MetaData = new MetaData_Type_MACDEXT();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MACDEXT>();
        }

       public MetaData_Type_MACDEXT MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MACDEXT> TechnicalIndicator
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

	public class Impl_MACDEXT : Int_MACDEXT
	{
		const string s_function = "MACDEXT";

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

		private static readonly Lazy<Impl_MACDEXT> s_Impl_MACDEXT =
			new Lazy<Impl_MACDEXT>(() => new Impl_MACDEXT());
		public static Impl_MACDEXT Instance
		{
			get
			{
				return s_Impl_MACDEXT.Value;
			}
		}
		private Impl_MACDEXT()
		{
		}

		internal static readonly IDictionary s_MACDEXT_interval_translation
			 = new Dictionary<Const_MACDEXT.MACDEXT_interval, string>()
		{
			{
				Const_MACDEXT.MACDEXT_interval.none,
				null
			},
			{
				Const_MACDEXT.MACDEXT_interval.n_1min,
				"1min"
			},
			{
				Const_MACDEXT.MACDEXT_interval.n_5min,
				"5min"
			},
			{
				Const_MACDEXT.MACDEXT_interval.n_15min,
				"15min"
			},
			{
				Const_MACDEXT.MACDEXT_interval.n_30min,
				"30min"
			},
			{
				Const_MACDEXT.MACDEXT_interval.n_60min,
				"60min"
			},
			{
				Const_MACDEXT.MACDEXT_interval.daily,
				"daily"
			},
			{
				Const_MACDEXT.MACDEXT_interval.weekly,
				"weekly"
			},
			{
				Const_MACDEXT.MACDEXT_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_MACDEXT_series_type_translation
			 = new Dictionary<Const_MACDEXT.MACDEXT_series_type, string>()
		{
			{
				Const_MACDEXT.MACDEXT_series_type.none,
				null
			},
			{
				Const_MACDEXT.MACDEXT_series_type.close,
				"close"
			},
			{
				Const_MACDEXT.MACDEXT_series_type.open,
				"open"
			},
			{
				Const_MACDEXT.MACDEXT_series_type.high,
				"high"
			},
			{
				Const_MACDEXT.MACDEXT_series_type.low,
				"low"
			}
		};

		internal static readonly IDictionary s_MACDEXT_fastmatype_translation
			 = new Dictionary<Const_MACDEXT.MACDEXT_fastmatype, int>()
		{
			{
				Const_MACDEXT.MACDEXT_fastmatype.none,
				-1
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_0,
				0
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_1,
				1
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_2,
				2
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_3,
				3
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_4,
				4
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_5,
				5
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_6,
				6
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_7,
				7
			},
			{
				Const_MACDEXT.MACDEXT_fastmatype.n_8,
				8
			}
		};

		internal static readonly IDictionary s_MACDEXT_slowmatype_translation
			 = new Dictionary<Const_MACDEXT.MACDEXT_slowmatype, int>()
		{
			{
				Const_MACDEXT.MACDEXT_slowmatype.none,
				-1
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_0,
				0
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_1,
				1
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_2,
				2
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_3,
				3
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_4,
				4
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_5,
				5
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_6,
				6
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_7,
				7
			},
			{
				Const_MACDEXT.MACDEXT_slowmatype.n_8,
				8
			}
		};

		internal static readonly IDictionary s_MACDEXT_signalmatype_translation
			 = new Dictionary<Const_MACDEXT.MACDEXT_signalmatype, int>()
		{
			{
				Const_MACDEXT.MACDEXT_signalmatype.none,
				-1
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_0,
				0
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_1,
				1
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_2,
				2
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_3,
				3
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_4,
				4
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_5,
				5
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_6,
				6
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_7,
				7
			},
			{
				Const_MACDEXT.MACDEXT_signalmatype.n_8,
				8
			}
		};

		public IAvapiResponse_MACDEXT Query(
			string symbol,
			Const_MACDEXT.MACDEXT_interval interval,
			Const_MACDEXT.MACDEXT_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			Const_MACDEXT.MACDEXT_fastmatype fastmatype = Const_MACDEXT.MACDEXT_fastmatype.none,
			Const_MACDEXT.MACDEXT_slowmatype slowmatype = Const_MACDEXT.MACDEXT_slowmatype.none,
			Const_MACDEXT.MACDEXT_signalmatype signalmatype = Const_MACDEXT.MACDEXT_signalmatype.none)
		{
			string current_interval = s_MACDEXT_interval_translation[interval] as string;
			string current_series_type = s_MACDEXT_series_type_translation[series_type] as string;
			int current_fastmatype = (int)s_MACDEXT_fastmatype_translation[fastmatype];
			int current_slowmatype = (int)s_MACDEXT_slowmatype_translation[slowmatype];
			int current_signalmatype = (int)s_MACDEXT_signalmatype_translation[signalmatype];

			return QueryPrimitive(symbol,current_interval,current_series_type,fastperiod,slowperiod,signalperiod,current_fastmatype,current_slowmatype,current_signalmatype);
		}

		public async Task<IAvapiResponse_MACDEXT> QueryAsync(
			string symbol,
			Const_MACDEXT.MACDEXT_interval interval,
			Const_MACDEXT.MACDEXT_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			Const_MACDEXT.MACDEXT_fastmatype fastmatype = Const_MACDEXT.MACDEXT_fastmatype.none,
			Const_MACDEXT.MACDEXT_slowmatype slowmatype = Const_MACDEXT.MACDEXT_slowmatype.none,
			Const_MACDEXT.MACDEXT_signalmatype signalmatype = Const_MACDEXT.MACDEXT_signalmatype.none)
		{
			string current_interval = s_MACDEXT_interval_translation[interval] as string;
			string current_series_type = s_MACDEXT_series_type_translation[series_type] as string;
			int current_fastmatype = (int)s_MACDEXT_fastmatype_translation[fastmatype];
			int current_slowmatype = (int)s_MACDEXT_slowmatype_translation[slowmatype];
			int current_signalmatype = (int)s_MACDEXT_signalmatype_translation[signalmatype];

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type,fastperiod,slowperiod,signalperiod,current_fastmatype,current_slowmatype,current_signalmatype);
		}


		public IAvapiResponse_MACDEXT QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			int fastmatype = -1,
			int slowmatype = -1,
			int signalmatype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastmatype",fastmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowmatype",slowmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("signalmatype",signalmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_MACDEXT ret = new AvapiResponse_MACDEXT
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MACDEXT> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			int fastmatype = -1,
			int slowmatype = -1,
			int signalmatype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastmatype",fastmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowmatype",slowmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("signalmatype",signalmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_MACDEXT ret = new AvapiResponse_MACDEXT
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MACDEXT_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MACDEXT_Content ret = new AvapiResponse_MACDEXT_Content();
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
                ret.MetaData.FastMAType = (string)metaData["5.4: Fast MA Type"];
                ret.MetaData.SlowMAType = (string)metaData["5.5: SlowMAType"];
                ret.MetaData.SignalMAType = (string)metaData["5.6: Signal MA Type"];
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MACDEXT"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MACDEXT technicalindicator = new TechnicalIndicator_Type_MACDEXT
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