using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiAPO
{
    internal class AvapiResponse_APO : IAvapiResponse_APO
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

        public IAvapiResponse_APO_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_APO
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

    public class TechnicalIndicator_Type_APO
    {
        public string APO
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

    internal class AvapiResponse_APO_Content : IAvapiResponse_APO_Content
    {
        internal AvapiResponse_APO_Content()
        {
           MetaData = new MetaData_Type_APO();
           TechnicalIndicator = new List<TechnicalIndicator_Type_APO>();
        }

       public MetaData_Type_APO MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_APO> TechnicalIndicator
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

	public class Impl_APO : Int_APO
	{
		const string s_function = "APO";

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

		private static readonly Lazy<Impl_APO> s_Impl_APO =
			new Lazy<Impl_APO>(() => new Impl_APO());
		public static Impl_APO Instance
		{
			get
			{
				return s_Impl_APO.Value;
			}
		}
		private Impl_APO()
		{
		}

		internal static readonly IDictionary s_APO_interval_translation
			 = new Dictionary<Const_APO.APO_interval, string>()
		{
			{
				Const_APO.APO_interval.none,
				null
			},
			{
				Const_APO.APO_interval.n_1min,
				"1min"
			},
			{
				Const_APO.APO_interval.n_5min,
				"5min"
			},
			{
				Const_APO.APO_interval.n_15min,
				"15min"
			},
			{
				Const_APO.APO_interval.n_30min,
				"30min"
			},
			{
				Const_APO.APO_interval.n_60min,
				"60min"
			},
			{
				Const_APO.APO_interval.daily,
				"daily"
			},
			{
				Const_APO.APO_interval.weekly,
				"weekly"
			},
			{
				Const_APO.APO_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_APO_series_type_translation
			 = new Dictionary<Const_APO.APO_series_type, string>()
		{
			{
				Const_APO.APO_series_type.none,
				null
			},
			{
				Const_APO.APO_series_type.close,
				"close"
			},
			{
				Const_APO.APO_series_type.open,
				"open"
			},
			{
				Const_APO.APO_series_type.high,
				"high"
			},
			{
				Const_APO.APO_series_type.low,
				"low"
			}
		};

		internal static readonly IDictionary s_APO_matype_translation
			 = new Dictionary<Const_APO.APO_matype, int>()
		{
			{
				Const_APO.APO_matype.none,
				-1
			},
			{
				Const_APO.APO_matype.n_0,
				0
			},
			{
				Const_APO.APO_matype.n_1,
				1
			},
			{
				Const_APO.APO_matype.n_2,
				2
			},
			{
				Const_APO.APO_matype.n_3,
				3
			},
			{
				Const_APO.APO_matype.n_4,
				4
			},
			{
				Const_APO.APO_matype.n_5,
				5
			},
			{
				Const_APO.APO_matype.n_6,
				6
			},
			{
				Const_APO.APO_matype.n_7,
				7
			},
			{
				Const_APO.APO_matype.n_8,
				8
			}
		};

		public IAvapiResponse_APO Query(
			string symbol,
			Const_APO.APO_interval interval,
			Const_APO.APO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_APO.APO_matype matype = Const_APO.APO_matype.none)
		{
			string current_interval = s_APO_interval_translation[interval] as string;
			string current_series_type = s_APO_series_type_translation[series_type] as string;
			int current_matype = (int)s_APO_matype_translation[matype];

			return QueryPrimitive(symbol,current_interval,current_series_type,fastperiod,slowperiod,current_matype);
		}

		public async Task<IAvapiResponse_APO> QueryAsync(
			string symbol,
			Const_APO.APO_interval interval,
			Const_APO.APO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_APO.APO_matype matype = Const_APO.APO_matype.none)
		{
			string current_interval = s_APO_interval_translation[interval] as string;
			string current_series_type = s_APO_series_type_translation[series_type] as string;
			int current_matype = (int)s_APO_matype_translation[matype];

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type,fastperiod,slowperiod,current_matype);
		}


		public IAvapiResponse_APO QueryPrimitive(
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

			IAvapiResponse_APO ret = new AvapiResponse_APO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_APO> QueryPrimitiveAsync(
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
			IAvapiResponse_APO ret = new AvapiResponse_APO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_APO_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_APO_Content ret = new AvapiResponse_APO_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: APO"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_APO technicalindicator = new TechnicalIndicator_Type_APO
                    {
                        DateTime = ((JProperty)result).Name,
                        APO = (string)result.First["APO"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}