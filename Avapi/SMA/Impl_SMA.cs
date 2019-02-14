using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSMA
{
    internal class AvapiResponse_SMA : IAvapiResponse_SMA
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

        public IAvapiResponse_SMA_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_SMA
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

    public class TechnicalIndicator_Type_SMA
    {
        public string SMA
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

    internal class AvapiResponse_SMA_Content : IAvapiResponse_SMA_Content
    {
        internal AvapiResponse_SMA_Content()
        {
           MetaData = new MetaData_Type_SMA();
           TechnicalIndicator = new List<TechnicalIndicator_Type_SMA>();
        }

       public MetaData_Type_SMA MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_SMA> TechnicalIndicator
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

	public class Impl_SMA : Int_SMA
	{
		const string s_function = "SMA";

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

		private static readonly Lazy<Impl_SMA> s_Impl_SMA =
			new Lazy<Impl_SMA>(() => new Impl_SMA());
		public static Impl_SMA Instance
		{
			get
			{
				return s_Impl_SMA.Value;
			}
		}
		private Impl_SMA()
		{
		}

		internal static readonly IDictionary s_SMA_interval_translation
			 = new Dictionary<Const_SMA.SMA_interval, string>()
		{
			{
				Const_SMA.SMA_interval.none,
				null
			},
			{
				Const_SMA.SMA_interval.n_1min,
				"1min"
			},
			{
				Const_SMA.SMA_interval.n_5min,
				"5min"
			},
			{
				Const_SMA.SMA_interval.n_15min,
				"15min"
			},
			{
				Const_SMA.SMA_interval.n_30min,
				"30min"
			},
			{
				Const_SMA.SMA_interval.n_60min,
				"60min"
			},
			{
				Const_SMA.SMA_interval.daily,
				"daily"
			},
			{
				Const_SMA.SMA_interval.weekly,
				"weekly"
			},
			{
				Const_SMA.SMA_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_SMA_series_type_translation
			 = new Dictionary<Const_SMA.SMA_series_type, string>()
		{
			{
				Const_SMA.SMA_series_type.none,
				null
			},
			{
				Const_SMA.SMA_series_type.close,
				"close"
			},
			{
				Const_SMA.SMA_series_type.open,
				"open"
			},
			{
				Const_SMA.SMA_series_type.high,
				"high"
			},
			{
				Const_SMA.SMA_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_SMA Query(
			string symbol,
			Const_SMA.SMA_interval interval,
			int time_period,
			Const_SMA.SMA_series_type series_type)
		{
			string current_interval = s_SMA_interval_translation[interval] as string;
			string current_series_type = s_SMA_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_SMA> QueryAsync(
			string symbol,
			Const_SMA.SMA_interval interval,
			int time_period,
			Const_SMA.SMA_series_type series_type)
		{
			string current_interval = s_SMA_interval_translation[interval] as string;
			string current_series_type = s_SMA_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_SMA QueryPrimitive(
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

			IAvapiResponse_SMA ret = new AvapiResponse_SMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_SMA> QueryPrimitiveAsync(
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
			IAvapiResponse_SMA ret = new AvapiResponse_SMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_SMA_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_SMA_Content ret = new AvapiResponse_SMA_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: SMA"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_SMA technicalindicator = new TechnicalIndicator_Type_SMA
                    {
                        DateTime = ((JProperty)result).Name,
                        SMA = (string)result.First["SMA"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}