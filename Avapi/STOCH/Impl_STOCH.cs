using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSTOCH
{
    internal class AvapiResponse_STOCH : IAvapiResponse_STOCH
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

        public IAvapiResponse_STOCH_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_STOCH
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

        public string FastKPeriod
        {
            internal set;
            get;
        }

        public string SlowKPeriod
        {
            internal set;
            get;
        }

        public string SlowKMAType
        {
            internal set;
            get;
        }

        public string SlowDPeriod
        {
            internal set;
            get;
        }

        public string SlowDMAType
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

    public class TechnicalIndicator_Type_STOCH
    {
        public string SlowK
        {
            internal set;
            get;
        }

        public string SlowD
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

    internal class AvapiResponse_STOCH_Content : IAvapiResponse_STOCH_Content
    {
        internal AvapiResponse_STOCH_Content()
        {
           MetaData = new MetaData_Type_STOCH();
           TechnicalIndicator = new List<TechnicalIndicator_Type_STOCH>();
        }

       public MetaData_Type_STOCH MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_STOCH> TechnicalIndicator
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

	public class Impl_STOCH : Int_STOCH
	{
		const string s_function = "STOCH";

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

		private static readonly Lazy<Impl_STOCH> s_Impl_STOCH =
			new Lazy<Impl_STOCH>(() => new Impl_STOCH());
		public static Impl_STOCH Instance
		{
			get
			{
				return s_Impl_STOCH.Value;
			}
		}
		private Impl_STOCH()
		{
		}

		internal static readonly IDictionary s_STOCH_interval_translation
			 = new Dictionary<Const_STOCH.STOCH_interval, string>()
		{
			{
				Const_STOCH.STOCH_interval.none,
				null
			},
			{
				Const_STOCH.STOCH_interval.n_1min,
				"1min"
			},
			{
				Const_STOCH.STOCH_interval.n_5min,
				"5min"
			},
			{
				Const_STOCH.STOCH_interval.n_15min,
				"15min"
			},
			{
				Const_STOCH.STOCH_interval.n_30min,
				"30min"
			},
			{
				Const_STOCH.STOCH_interval.n_60min,
				"60min"
			},
			{
				Const_STOCH.STOCH_interval.daily,
				"daily"
			},
			{
				Const_STOCH.STOCH_interval.weekly,
				"weekly"
			},
			{
				Const_STOCH.STOCH_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_STOCH_slowkmatype_translation
			 = new Dictionary<Const_STOCH.STOCH_slowkmatype, int>()
		{
			{
				Const_STOCH.STOCH_slowkmatype.none,
				-1
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_0,
				0
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_1,
				1
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_2,
				2
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_3,
				3
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_4,
				4
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_5,
				5
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_6,
				6
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_7,
				7
			},
			{
				Const_STOCH.STOCH_slowkmatype.n_8,
				8
			}
		};

		internal static readonly IDictionary s_STOCH_slowdmatype_translation
			 = new Dictionary<Const_STOCH.STOCH_slowdmatype, int>()
		{
			{
				Const_STOCH.STOCH_slowdmatype.none,
				-1
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_0,
				0
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_1,
				1
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_2,
				2
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_3,
				3
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_4,
				4
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_5,
				5
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_6,
				6
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_7,
				7
			},
			{
				Const_STOCH.STOCH_slowdmatype.n_8,
				8
			}
		};

		public IAvapiResponse_STOCH Query(
			string symbol,
			Const_STOCH.STOCH_interval interval,
			int fastkperiod = -1,
			int slowkperiod = -1,
			int slowdperiod = -1,
			Const_STOCH.STOCH_slowkmatype slowkmatype = Const_STOCH.STOCH_slowkmatype.none,
			Const_STOCH.STOCH_slowdmatype slowdmatype = Const_STOCH.STOCH_slowdmatype.none)
		{
			string current_interval = s_STOCH_interval_translation[interval] as string;
			int current_slowkmatype = (int)s_STOCH_slowkmatype_translation[slowkmatype];
			int current_slowdmatype = (int)s_STOCH_slowdmatype_translation[slowdmatype];

			return QueryPrimitive(symbol,current_interval,fastkperiod,slowkperiod,slowdperiod,current_slowkmatype,current_slowdmatype);
		}

		public async Task<IAvapiResponse_STOCH> QueryAsync(
			string symbol,
			Const_STOCH.STOCH_interval interval,
			int fastkperiod = -1,
			int slowkperiod = -1,
			int slowdperiod = -1,
			Const_STOCH.STOCH_slowkmatype slowkmatype = Const_STOCH.STOCH_slowkmatype.none,
			Const_STOCH.STOCH_slowdmatype slowdmatype = Const_STOCH.STOCH_slowdmatype.none)
		{
			string current_interval = s_STOCH_interval_translation[interval] as string;
			int current_slowkmatype = (int)s_STOCH_slowkmatype_translation[slowkmatype];
			int current_slowdmatype = (int)s_STOCH_slowdmatype_translation[slowdmatype];

			return await QueryPrimitiveAsync(symbol,current_interval,fastkperiod,slowkperiod,slowdperiod,current_slowkmatype,current_slowdmatype);
		}


		public IAvapiResponse_STOCH QueryPrimitive(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int slowkperiod = -1,
			int slowdperiod = -1,
			int slowkmatype = -1,
			int slowdmatype = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowkperiod",slowkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowdperiod",slowdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowkmatype",slowkmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowdmatype",slowdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_STOCH ret = new AvapiResponse_STOCH
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_STOCH> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int slowkperiod = -1,
			int slowdperiod = -1,
			int slowkmatype = -1,
			int slowdmatype = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowkperiod",slowkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowdperiod",slowdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowkmatype",slowkmatype.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowdmatype",slowdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_STOCH ret = new AvapiResponse_STOCH
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_STOCH_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_STOCH_Content ret = new AvapiResponse_STOCH_Content();
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
                ret.MetaData.FastKPeriod = (string)metaData["5.1: FastK Period"];
                ret.MetaData.SlowKPeriod = (string)metaData["5.2: SlowK Period"];
                ret.MetaData.SlowKMAType = (string)metaData["5.3: SlowK MA Type"];
                ret.MetaData.SlowDPeriod = (string)metaData["5.4: SlowD Period"];
                ret.MetaData.SlowDMAType = (string)metaData["5.5: SlowD MA Type"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: STOCH"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_STOCH technicalindicator = new TechnicalIndicator_Type_STOCH
                    {
                        DateTime = ((JProperty)result).Name,
                        SlowK = (string)result.First["SlowK"],
                        SlowD = (string)result.First["SlowD"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}