using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiHT_TRENDMODE
{
    internal class AvapiResponse_HT_TRENDMODE : IAvapiResponse_HT_TRENDMODE
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

        public IAvapiResponse_HT_TRENDMODE_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_HT_TRENDMODE
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

    public class TechnicalIndicator_Type_HT_TRENDMODE
    {
        public string TRENDMODE
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

    internal class AvapiResponse_HT_TRENDMODE_Content : IAvapiResponse_HT_TRENDMODE_Content
    {
        internal AvapiResponse_HT_TRENDMODE_Content()
        {
           MetaData = new MetaData_Type_HT_TRENDMODE();
           TechnicalIndicator = new List<TechnicalIndicator_Type_HT_TRENDMODE>();
        }

       public MetaData_Type_HT_TRENDMODE MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_HT_TRENDMODE> TechnicalIndicator
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

	public class Impl_HT_TRENDMODE : Int_HT_TRENDMODE
	{
		const string s_function = "HT_TRENDMODE";

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

		private static readonly Lazy<Impl_HT_TRENDMODE> s_Impl_HT_TRENDMODE =
			new Lazy<Impl_HT_TRENDMODE>(() => new Impl_HT_TRENDMODE());
		public static Impl_HT_TRENDMODE Instance
		{
			get
			{
				return s_Impl_HT_TRENDMODE.Value;
			}
		}
		private Impl_HT_TRENDMODE()
		{
		}

		internal static readonly IDictionary s_HT_TRENDMODE_interval_translation
			 = new Dictionary<Const_HT_TRENDMODE.HT_TRENDMODE_interval, string>()
		{
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.none,
				null
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_1min,
				"1min"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_5min,
				"5min"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_15min,
				"15min"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_30min,
				"30min"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_60min,
				"60min"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.daily,
				"daily"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.weekly,
				"weekly"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_HT_TRENDMODE_series_type_translation
			 = new Dictionary<Const_HT_TRENDMODE.HT_TRENDMODE_series_type, string>()
		{
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_series_type.none,
				null
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_series_type.close,
				"close"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_series_type.open,
				"open"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_series_type.high,
				"high"
			},
			{
				Const_HT_TRENDMODE.HT_TRENDMODE_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_HT_TRENDMODE Query(
			string symbol,
			Const_HT_TRENDMODE.HT_TRENDMODE_interval interval,
			Const_HT_TRENDMODE.HT_TRENDMODE_series_type series_type)
		{
			string current_interval = s_HT_TRENDMODE_interval_translation[interval] as string;
			string current_series_type = s_HT_TRENDMODE_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,current_series_type);
		}

		public async Task<IAvapiResponse_HT_TRENDMODE> QueryAsync(
			string symbol,
			Const_HT_TRENDMODE.HT_TRENDMODE_interval interval,
			Const_HT_TRENDMODE.HT_TRENDMODE_series_type series_type)
		{
			string current_interval = s_HT_TRENDMODE_interval_translation[interval] as string;
			string current_series_type = s_HT_TRENDMODE_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type);
		}


		public IAvapiResponse_HT_TRENDMODE QueryPrimitive(
			string symbol,
			string interval,
			string series_type)
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
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_HT_TRENDMODE ret = new AvapiResponse_HT_TRENDMODE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_HT_TRENDMODE> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type)
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
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_HT_TRENDMODE ret = new AvapiResponse_HT_TRENDMODE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_HT_TRENDMODE_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_HT_TRENDMODE_Content ret = new AvapiResponse_HT_TRENDMODE_Content();
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
                ret.MetaData.SeriesType = (string)metaData["5: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: HT_TRENDMODE"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_HT_TRENDMODE technicalindicator = new TechnicalIndicator_Type_HT_TRENDMODE
                    {
                        DateTime = ((JProperty)result).Name,
                        TRENDMODE = (string)result.First["TRENDMODE"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}