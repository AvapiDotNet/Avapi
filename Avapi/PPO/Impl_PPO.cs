using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiPPO
{
    internal class AvapiResponse_PPO : IAvapiResponse_PPO
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

        public IAvapiResponse_PPO_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_PPO
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

        public string MAType
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

    public class TechnicalIndicator_Type_PPO
    {
        public string PPO
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

    internal class AvapiResponse_PPO_Content : IAvapiResponse_PPO_Content
    {
        internal AvapiResponse_PPO_Content()
        {
           MetaData = new MetaData_Type_PPO();
           TechnicalIndicator = new List<TechnicalIndicator_Type_PPO>();
        }

       public MetaData_Type_PPO MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_PPO> TechnicalIndicator
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

	public class Impl_PPO : Int_PPO
	{
		const string s_function = "PPO";

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

		private static readonly Lazy<Impl_PPO> s_Impl_PPO =
			new Lazy<Impl_PPO>(() => new Impl_PPO());
		public static Impl_PPO Instance
		{
			get
			{
				return s_Impl_PPO.Value;
			}
		}
		private Impl_PPO()
		{
		}

		internal static readonly IDictionary s_PPO_interval_translation
			 = new Dictionary<Const_PPO.PPO_interval, string>()
		{
			{
				Const_PPO.PPO_interval.none,
				null
			},
			{
				Const_PPO.PPO_interval.n_1min,
				"1min"
			},
			{
				Const_PPO.PPO_interval.n_5min,
				"5min"
			},
			{
				Const_PPO.PPO_interval.n_15min,
				"15min"
			},
			{
				Const_PPO.PPO_interval.n_30min,
				"30min"
			},
			{
				Const_PPO.PPO_interval.n_60min,
				"60min"
			},
			{
				Const_PPO.PPO_interval.daily,
				"daily"
			},
			{
				Const_PPO.PPO_interval.weekly,
				"weekly"
			},
			{
				Const_PPO.PPO_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_PPO_series_type_translation
			 = new Dictionary<Const_PPO.PPO_series_type, string>()
		{
			{
				Const_PPO.PPO_series_type.none,
				null
			},
			{
				Const_PPO.PPO_series_type.close,
				"close"
			},
			{
				Const_PPO.PPO_series_type.open,
				"open"
			},
			{
				Const_PPO.PPO_series_type.high,
				"high"
			},
			{
				Const_PPO.PPO_series_type.low,
				"low"
			}
		};

		internal static readonly IDictionary s_PPO_matype_translation
			 = new Dictionary<Const_PPO.PPO_matype, int>()
		{
			{
				Const_PPO.PPO_matype.none,
				-1
			},
			{
				Const_PPO.PPO_matype.n_0,
				0
			},
			{
				Const_PPO.PPO_matype.n_1,
				1
			},
			{
				Const_PPO.PPO_matype.n_2,
				2
			},
			{
				Const_PPO.PPO_matype.n_3,
				3
			},
			{
				Const_PPO.PPO_matype.n_4,
				4
			},
			{
				Const_PPO.PPO_matype.n_5,
				5
			},
			{
				Const_PPO.PPO_matype.n_6,
				6
			},
			{
				Const_PPO.PPO_matype.n_7,
				7
			},
			{
				Const_PPO.PPO_matype.n_8,
				8
			}
		};

		public IAvapiResponse_PPO Query(
			string symbol,
			Const_PPO.PPO_interval interval,
			Const_PPO.PPO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_PPO.PPO_matype matype = Const_PPO.PPO_matype.none)
		{
			string current_interval = s_PPO_interval_translation[interval] as string;
			string current_series_type = s_PPO_series_type_translation[series_type] as string;
			int current_matype = (int)s_PPO_matype_translation[matype];

			return QueryPrimitive(symbol,current_interval,current_series_type,fastperiod,slowperiod,current_matype);
		}

		public async Task<IAvapiResponse_PPO> QueryAsync(
			string symbol,
			Const_PPO.PPO_interval interval,
			Const_PPO.PPO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_PPO.PPO_matype matype = Const_PPO.PPO_matype.none)
		{
			string current_interval = s_PPO_interval_translation[interval] as string;
			string current_series_type = s_PPO_series_type_translation[series_type] as string;
			int current_matype = (int)s_PPO_matype_translation[matype];

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type,fastperiod,slowperiod,current_matype);
		}


		public IAvapiResponse_PPO QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("matype",matype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_PPO ret = new AvapiResponse_PPO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_PPO> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("matype",matype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_PPO ret = new AvapiResponse_PPO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_PPO_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_PPO_Content ret = new AvapiResponse_PPO_Content();
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
                ret.MetaData.FastPeriod = (string)metaData["5.1: FastPeriod"];
                ret.MetaData.SlowPeriod = (string)metaData["5.2: SlowPeriod"];
                ret.MetaData.MAType = (string)metaData["5.3: MA Type"];
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: PPO"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_PPO technicalindicator = new TechnicalIndicator_Type_PPO
                    {
                        DateTime = ((JProperty)result).Name,
                        PPO = (string)result.First["PPO"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}