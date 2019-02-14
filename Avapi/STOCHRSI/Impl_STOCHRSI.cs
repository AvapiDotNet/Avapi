using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSTOCHRSI
{
    internal class AvapiResponse_STOCHRSI : IAvapiResponse_STOCHRSI
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

        public IAvapiResponse_STOCHRSI_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_STOCHRSI
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

        public string FastKPeriod
        {
            internal set;
            get;
        }

        public string FastDPeriod
        {
            internal set;
            get;
        }

        public string FastDMAType
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

    public class TechnicalIndicator_Type_STOCHRSI
    {
        public string FastK
        {
            internal set;
            get;
        }

        public string FastD
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

    internal class AvapiResponse_STOCHRSI_Content : IAvapiResponse_STOCHRSI_Content
    {
        internal AvapiResponse_STOCHRSI_Content()
        {
           MetaData = new MetaData_Type_STOCHRSI();
           TechnicalIndicator = new List<TechnicalIndicator_Type_STOCHRSI>();
        }

       public MetaData_Type_STOCHRSI MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_STOCHRSI> TechnicalIndicator
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

	public class Impl_STOCHRSI : Int_STOCHRSI
	{
		const string s_function = "STOCHRSI";

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

		private static readonly Lazy<Impl_STOCHRSI> s_Impl_STOCHRSI =
			new Lazy<Impl_STOCHRSI>(() => new Impl_STOCHRSI());
		public static Impl_STOCHRSI Instance
		{
			get
			{
				return s_Impl_STOCHRSI.Value;
			}
		}
		private Impl_STOCHRSI()
		{
		}

		internal static readonly IDictionary s_STOCHRSI_interval_translation
			 = new Dictionary<Const_STOCHRSI.STOCHRSI_interval, string>()
		{
			{
				Const_STOCHRSI.STOCHRSI_interval.none,
				null
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.n_1min,
				"1min"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.n_5min,
				"5min"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.n_15min,
				"15min"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.n_30min,
				"30min"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.n_60min,
				"60min"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.daily,
				"daily"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.weekly,
				"weekly"
			},
			{
				Const_STOCHRSI.STOCHRSI_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_STOCHRSI_series_type_translation
			 = new Dictionary<Const_STOCHRSI.STOCHRSI_series_type, string>()
		{
			{
				Const_STOCHRSI.STOCHRSI_series_type.none,
				null
			},
			{
				Const_STOCHRSI.STOCHRSI_series_type.close,
				"close"
			},
			{
				Const_STOCHRSI.STOCHRSI_series_type.open,
				"open"
			},
			{
				Const_STOCHRSI.STOCHRSI_series_type.high,
				"high"
			},
			{
				Const_STOCHRSI.STOCHRSI_series_type.low,
				"low"
			}
		};

		internal static readonly IDictionary s_STOCHRSI_fastdmatype_translation
			 = new Dictionary<Const_STOCHRSI.STOCHRSI_fastdmatype, int>()
		{
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.none,
				-1
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_0,
				0
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_1,
				1
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_2,
				2
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_3,
				3
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_4,
				4
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_5,
				5
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_6,
				6
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_7,
				7
			},
			{
				Const_STOCHRSI.STOCHRSI_fastdmatype.n_8,
				8
			}
		};

		public IAvapiResponse_STOCHRSI Query(
			string symbol,
			Const_STOCHRSI.STOCHRSI_interval interval,
			int time_period,
			Const_STOCHRSI.STOCHRSI_series_type series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHRSI.STOCHRSI_fastdmatype fastdmatype = Const_STOCHRSI.STOCHRSI_fastdmatype.none)
		{
			string current_interval = s_STOCHRSI_interval_translation[interval] as string;
			string current_series_type = s_STOCHRSI_series_type_translation[series_type] as string;
			int current_fastdmatype = (int)s_STOCHRSI_fastdmatype_translation[fastdmatype];

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type,fastkperiod,fastdperiod,current_fastdmatype);
		}

		public async Task<IAvapiResponse_STOCHRSI> QueryAsync(
			string symbol,
			Const_STOCHRSI.STOCHRSI_interval interval,
			int time_period,
			Const_STOCHRSI.STOCHRSI_series_type series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHRSI.STOCHRSI_fastdmatype fastdmatype = Const_STOCHRSI.STOCHRSI_fastdmatype.none)
		{
			string current_interval = s_STOCHRSI_interval_translation[interval] as string;
			string current_series_type = s_STOCHRSI_series_type_translation[series_type] as string;
			int current_fastdmatype = (int)s_STOCHRSI_fastdmatype_translation[fastdmatype];

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type,fastkperiod,fastdperiod,current_fastdmatype);
		}


		public IAvapiResponse_STOCHRSI QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdperiod",fastdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdmatype",fastdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_STOCHRSI ret = new AvapiResponse_STOCHRSI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_STOCHRSI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdperiod",fastdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdmatype",fastdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_STOCHRSI ret = new AvapiResponse_STOCHRSI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_STOCHRSI_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_STOCHRSI_Content ret = new AvapiResponse_STOCHRSI_Content();
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
                ret.MetaData.FastKPeriod = (string)metaData["6.1: FastK Period"];
                ret.MetaData.FastDPeriod = (string)metaData["6.2: FastD Period"];
                ret.MetaData.FastDMAType = (string)metaData["6.3: FastD MA Type"];
                ret.MetaData.SeriesType = (string)metaData["7: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["8: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: STOCHRSI"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_STOCHRSI technicalindicator = new TechnicalIndicator_Type_STOCHRSI
                    {
                        DateTime = ((JProperty)result).Name,
                        FastK = (string)result.First["FastK"],
                        FastD = (string)result.First["FastD"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}