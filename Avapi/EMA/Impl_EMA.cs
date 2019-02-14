using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiEMA
{
    internal class AvapiResponse_EMA : IAvapiResponse_EMA
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

        public IAvapiResponse_EMA_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_EMA
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

    public class TechnicalIndicator_Type_EMA
    {
        public string EMA
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

    internal class AvapiResponse_EMA_Content : IAvapiResponse_EMA_Content
    {
        internal AvapiResponse_EMA_Content()
        {
           MetaData = new MetaData_Type_EMA();
           TechnicalIndicator = new List<TechnicalIndicator_Type_EMA>();
        }

       public MetaData_Type_EMA MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_EMA> TechnicalIndicator
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

	public class Impl_EMA : Int_EMA
	{
		const string s_function = "EMA";

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

		private static readonly Lazy<Impl_EMA> s_Impl_EMA =
			new Lazy<Impl_EMA>(() => new Impl_EMA());
		public static Impl_EMA Instance
		{
			get
			{
				return s_Impl_EMA.Value;
			}
		}
		private Impl_EMA()
		{
		}

		internal static readonly IDictionary s_EMA_interval_translation
			 = new Dictionary<Const_EMA.EMA_interval, string>()
		{
			{
				Const_EMA.EMA_interval.none,
				null
			},
			{
				Const_EMA.EMA_interval.n_1min,
				"1min"
			},
			{
				Const_EMA.EMA_interval.n_5min,
				"5min"
			},
			{
				Const_EMA.EMA_interval.n_15min,
				"15min"
			},
			{
				Const_EMA.EMA_interval.n_30min,
				"30min"
			},
			{
				Const_EMA.EMA_interval.n_60min,
				"60min"
			},
			{
				Const_EMA.EMA_interval.daily,
				"daily"
			},
			{
				Const_EMA.EMA_interval.weekly,
				"weekly"
			},
			{
				Const_EMA.EMA_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_EMA_series_type_translation
			 = new Dictionary<Const_EMA.EMA_series_type, string>()
		{
			{
				Const_EMA.EMA_series_type.none,
				null
			},
			{
				Const_EMA.EMA_series_type.close,
				"close"
			},
			{
				Const_EMA.EMA_series_type.open,
				"open"
			},
			{
				Const_EMA.EMA_series_type.high,
				"high"
			},
			{
				Const_EMA.EMA_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_EMA Query(
			string symbol,
			Const_EMA.EMA_interval interval,
			int time_period,
			Const_EMA.EMA_series_type series_type)
		{
			string current_interval = s_EMA_interval_translation[interval] as string;
			string current_series_type = s_EMA_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_EMA> QueryAsync(
			string symbol,
			Const_EMA.EMA_interval interval,
			int time_period,
			Const_EMA.EMA_series_type series_type)
		{
			string current_interval = s_EMA_interval_translation[interval] as string;
			string current_series_type = s_EMA_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_EMA QueryPrimitive(
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

			IAvapiResponse_EMA ret = new AvapiResponse_EMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_EMA> QueryPrimitiveAsync(
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
			IAvapiResponse_EMA ret = new AvapiResponse_EMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_EMA_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_EMA_Content ret = new AvapiResponse_EMA_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: EMA"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_EMA technicalindicator = new TechnicalIndicator_Type_EMA
                    {
                        DateTime = ((JProperty)result).Name,
                        EMA = (string)result.First["EMA"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}