using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiHT_TRENDLINE
{
    internal class AvapiResponse_HT_TRENDLINE : IAvapiResponse_HT_TRENDLINE
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

        public IAvapiResponse_HT_TRENDLINE_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_HT_TRENDLINE
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

    public class TechnicalIndicator_Type_HT_TRENDLINE
    {
        public string HT_TRENDLINE
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

    internal class AvapiResponse_HT_TRENDLINE_Content : IAvapiResponse_HT_TRENDLINE_Content
    {
        internal AvapiResponse_HT_TRENDLINE_Content()
        {
           MetaData = new MetaData_Type_HT_TRENDLINE();
           TechnicalIndicator = new List<TechnicalIndicator_Type_HT_TRENDLINE>();
        }

       public MetaData_Type_HT_TRENDLINE MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_HT_TRENDLINE> TechnicalIndicator
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

	public class Impl_HT_TRENDLINE : Int_HT_TRENDLINE
	{
		const string s_function = "HT_TRENDLINE";

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

		private static readonly Lazy<Impl_HT_TRENDLINE> s_Impl_HT_TRENDLINE =
			new Lazy<Impl_HT_TRENDLINE>(() => new Impl_HT_TRENDLINE());
		public static Impl_HT_TRENDLINE Instance
		{
			get
			{
				return s_Impl_HT_TRENDLINE.Value;
			}
		}
		private Impl_HT_TRENDLINE()
		{
		}

		internal static readonly IDictionary s_HT_TRENDLINE_interval_translation
			 = new Dictionary<Const_HT_TRENDLINE.HT_TRENDLINE_interval, string>()
		{
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.none,
				null
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.n_1min,
				"1min"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.n_5min,
				"5min"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.n_15min,
				"15min"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.n_30min,
				"30min"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.n_60min,
				"60min"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.daily,
				"daily"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.weekly,
				"weekly"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_HT_TRENDLINE_series_type_translation
			 = new Dictionary<Const_HT_TRENDLINE.HT_TRENDLINE_series_type, string>()
		{
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_series_type.none,
				null
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_series_type.close,
				"close"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_series_type.open,
				"open"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_series_type.high,
				"high"
			},
			{
				Const_HT_TRENDLINE.HT_TRENDLINE_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_HT_TRENDLINE Query(
			string symbol,
			Const_HT_TRENDLINE.HT_TRENDLINE_interval interval,
			Const_HT_TRENDLINE.HT_TRENDLINE_series_type series_type)
		{
			string current_interval = s_HT_TRENDLINE_interval_translation[interval] as string;
			string current_series_type = s_HT_TRENDLINE_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,current_series_type);
		}

		public async Task<IAvapiResponse_HT_TRENDLINE> QueryAsync(
			string symbol,
			Const_HT_TRENDLINE.HT_TRENDLINE_interval interval,
			Const_HT_TRENDLINE.HT_TRENDLINE_series_type series_type)
		{
			string current_interval = s_HT_TRENDLINE_interval_translation[interval] as string;
			string current_series_type = s_HT_TRENDLINE_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type);
		}


		public IAvapiResponse_HT_TRENDLINE QueryPrimitive(
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

			IAvapiResponse_HT_TRENDLINE ret = new AvapiResponse_HT_TRENDLINE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_HT_TRENDLINE> QueryPrimitiveAsync(
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
			IAvapiResponse_HT_TRENDLINE ret = new AvapiResponse_HT_TRENDLINE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_HT_TRENDLINE_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_HT_TRENDLINE_Content ret = new AvapiResponse_HT_TRENDLINE_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: HT_TRENDLINE"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_HT_TRENDLINE technicalindicator = new TechnicalIndicator_Type_HT_TRENDLINE
                    {
                        DateTime = ((JProperty)result).Name,
                        HT_TRENDLINE = (string)result.First["HT_TRENDLINE"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}