using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiBBANDS
{
    internal class AvapiResponse_BBANDS : IAvapiResponse_BBANDS
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

        public IAvapiResponse_BBANDS_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_BBANDS
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

        public string TimePeriod
        {
            internal set;
            get;
        }

        public string DeviationUpper
        {
            internal set;
            get;
        }

        public string DeviationLower
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

    public class TechnicalIndicator_Type_BBANDS
    {
        public string RealLowerBand
        {
            internal set;
            get;
        }

        public string RealUpperBand
        {
            internal set;
            get;
        }

        public string RealMiddleBand
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

    internal class AvapiResponse_BBANDS_Content : IAvapiResponse_BBANDS_Content
    {
        internal AvapiResponse_BBANDS_Content()
        {
           MetaData = new MetaData_Type_BBANDS();
           TechnicalIndicator = new List<TechnicalIndicator_Type_BBANDS>();
        }

       public MetaData_Type_BBANDS MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_BBANDS> TechnicalIndicator
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

	public class Impl_BBANDS : Int_BBANDS
	{
		const string s_function = "BBANDS";

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

		private static readonly Lazy<Impl_BBANDS> s_Impl_BBANDS =
			new Lazy<Impl_BBANDS>(() => new Impl_BBANDS());
		public static Impl_BBANDS Instance
		{
			get
			{
				return s_Impl_BBANDS.Value;
			}
		}
		private Impl_BBANDS()
		{
		}

		internal static readonly IDictionary s_BBANDS_interval_translation
			 = new Dictionary<Const_BBANDS.BBANDS_interval, string>()
		{
			{
				Const_BBANDS.BBANDS_interval.none,
				null
			},
			{
				Const_BBANDS.BBANDS_interval.n_1min,
				"1min"
			},
			{
				Const_BBANDS.BBANDS_interval.n_5min,
				"5min"
			},
			{
				Const_BBANDS.BBANDS_interval.n_15min,
				"15min"
			},
			{
				Const_BBANDS.BBANDS_interval.n_30min,
				"30min"
			},
			{
				Const_BBANDS.BBANDS_interval.n_60min,
				"60min"
			},
			{
				Const_BBANDS.BBANDS_interval.daily,
				"daily"
			},
			{
				Const_BBANDS.BBANDS_interval.weekly,
				"weekly"
			},
			{
				Const_BBANDS.BBANDS_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_BBANDS_series_type_translation
			 = new Dictionary<Const_BBANDS.BBANDS_series_type, string>()
		{
			{
				Const_BBANDS.BBANDS_series_type.none,
				null
			},
			{
				Const_BBANDS.BBANDS_series_type.close,
				"close"
			},
			{
				Const_BBANDS.BBANDS_series_type.open,
				"open"
			},
			{
				Const_BBANDS.BBANDS_series_type.high,
				"high"
			},
			{
				Const_BBANDS.BBANDS_series_type.low,
				"low"
			}
		};

		internal static readonly IDictionary s_BBANDS_matype_translation
			 = new Dictionary<Const_BBANDS.BBANDS_matype, int>()
		{
			{
				Const_BBANDS.BBANDS_matype.none,
				-1
			},
			{
				Const_BBANDS.BBANDS_matype.n_0,
				0
			},
			{
				Const_BBANDS.BBANDS_matype.n_1,
				1
			},
			{
				Const_BBANDS.BBANDS_matype.n_2,
				2
			},
			{
				Const_BBANDS.BBANDS_matype.n_3,
				3
			},
			{
				Const_BBANDS.BBANDS_matype.n_4,
				4
			},
			{
				Const_BBANDS.BBANDS_matype.n_5,
				5
			},
			{
				Const_BBANDS.BBANDS_matype.n_6,
				6
			},
			{
				Const_BBANDS.BBANDS_matype.n_7,
				7
			},
			{
				Const_BBANDS.BBANDS_matype.n_8,
				8
			}
		};

		public IAvapiResponse_BBANDS Query(
			string symbol,
			Const_BBANDS.BBANDS_interval interval,
			int time_period,
			Const_BBANDS.BBANDS_series_type series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			Const_BBANDS.BBANDS_matype matype = Const_BBANDS.BBANDS_matype.none)
		{
			string current_interval = s_BBANDS_interval_translation[interval] as string;
			string current_series_type = s_BBANDS_series_type_translation[series_type] as string;
			int current_matype = (int)s_BBANDS_matype_translation[matype];

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type,nbdevup,nbdevdn,current_matype);
		}

		public async Task<IAvapiResponse_BBANDS> QueryAsync(
			string symbol,
			Const_BBANDS.BBANDS_interval interval,
			int time_period,
			Const_BBANDS.BBANDS_series_type series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			Const_BBANDS.BBANDS_matype matype = Const_BBANDS.BBANDS_matype.none)
		{
			string current_interval = s_BBANDS_interval_translation[interval] as string;
			string current_series_type = s_BBANDS_series_type_translation[series_type] as string;
			int current_matype = (int)s_BBANDS_matype_translation[matype];

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type,nbdevup,nbdevdn,current_matype);
		}


		public IAvapiResponse_BBANDS QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
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
			getParameters.Add(new KeyValuePair<string, string>("time_period",time_period.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			getParameters.Add(new KeyValuePair<string, string>("nbdevup",nbdevup.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("nbdevdn",nbdevdn.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("matype",matype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_BBANDS ret = new AvapiResponse_BBANDS
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_BBANDS> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
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
			getParameters.Add(new KeyValuePair<string, string>("time_period",time_period.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			getParameters.Add(new KeyValuePair<string, string>("nbdevup",nbdevup.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("nbdevdn",nbdevdn.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("matype",matype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_BBANDS ret = new AvapiResponse_BBANDS
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_BBANDS_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_BBANDS_Content ret = new AvapiResponse_BBANDS_Content();
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
                ret.MetaData.TimePeriod = (string)metaData["5: Time Period"];
                ret.MetaData.DeviationUpper = (string)metaData["6.1: Deviation multiplier for upper band Type"];
                ret.MetaData.DeviationLower = (string)metaData["6.2: Deviation multiplier for lower band"];
                ret.MetaData.MAType = (string)metaData["6.3: MA Type"];
                ret.MetaData.SeriesType = (string)metaData["7: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["8: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: BBANDS"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_BBANDS technicalindicator = new TechnicalIndicator_Type_BBANDS
                    {
                        DateTime = ((JProperty)result).Name,
                        RealLowerBand = (string)result.First["Real Lower Band"],
                        RealUpperBand = (string)result.First["Real Upper Band"],
                        RealMiddleBand = (string)result.First["Real Middle Band"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}