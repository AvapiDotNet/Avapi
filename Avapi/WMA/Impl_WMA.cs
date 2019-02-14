using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiWMA
{
    internal class AvapiResponse_WMA : IAvapiResponse_WMA
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

        public IAvapiResponse_WMA_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_WMA
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

    public class TechnicalIndicator_Type_WMA
    {
        public string WMA
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

    internal class AvapiResponse_WMA_Content : IAvapiResponse_WMA_Content
    {
        internal AvapiResponse_WMA_Content()
        {
           MetaData = new MetaData_Type_WMA();
           TechnicalIndicator = new List<TechnicalIndicator_Type_WMA>();
        }

       public MetaData_Type_WMA MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_WMA> TechnicalIndicator
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

	public class Impl_WMA : Int_WMA
	{
		const string s_function = "WMA";

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

		private static readonly Lazy<Impl_WMA> s_Impl_WMA =
			new Lazy<Impl_WMA>(() => new Impl_WMA());
		public static Impl_WMA Instance
		{
			get
			{
				return s_Impl_WMA.Value;
			}
		}
		private Impl_WMA()
		{
		}

		internal static readonly IDictionary s_WMA_interval_translation
			 = new Dictionary<Const_WMA.WMA_interval, string>()
		{
			{
				Const_WMA.WMA_interval.none,
				null
			},
			{
				Const_WMA.WMA_interval.n_1min,
				"1min"
			},
			{
				Const_WMA.WMA_interval.n_5min,
				"5min"
			},
			{
				Const_WMA.WMA_interval.n_15min,
				"15min"
			},
			{
				Const_WMA.WMA_interval.n_30min,
				"30min"
			},
			{
				Const_WMA.WMA_interval.n_60min,
				"60min"
			},
			{
				Const_WMA.WMA_interval.daily,
				"daily"
			},
			{
				Const_WMA.WMA_interval.weekly,
				"weekly"
			},
			{
				Const_WMA.WMA_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_WMA_series_type_translation
			 = new Dictionary<Const_WMA.WMA_series_type, string>()
		{
			{
				Const_WMA.WMA_series_type.none,
				null
			},
			{
				Const_WMA.WMA_series_type.close,
				"close"
			},
			{
				Const_WMA.WMA_series_type.open,
				"open"
			},
			{
				Const_WMA.WMA_series_type.high,
				"high"
			},
			{
				Const_WMA.WMA_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_WMA Query(
			string symbol,
			Const_WMA.WMA_interval interval,
			int time_period,
			Const_WMA.WMA_series_type series_type)
		{
			string current_interval = s_WMA_interval_translation[interval] as string;
			string current_series_type = s_WMA_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_WMA> QueryAsync(
			string symbol,
			Const_WMA.WMA_interval interval,
			int time_period,
			Const_WMA.WMA_series_type series_type)
		{
			string current_interval = s_WMA_interval_translation[interval] as string;
			string current_series_type = s_WMA_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_WMA QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
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
			getParameters.Add(new KeyValuePair<string, string>("time_period",time_period.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_WMA ret = new AvapiResponse_WMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_WMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
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
			getParameters.Add(new KeyValuePair<string, string>("time_period",time_period.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_WMA ret = new AvapiResponse_WMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_WMA_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_WMA_Content ret = new AvapiResponse_WMA_Content();
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
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: WMA"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_WMA technicalindicator = new TechnicalIndicator_Type_WMA
                    {
                        DateTime = ((JProperty)result).Name,
                        WMA = (string)result.First["WMA"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}