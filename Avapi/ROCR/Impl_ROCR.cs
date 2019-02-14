using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiROCR
{
    internal class AvapiResponse_ROCR : IAvapiResponse_ROCR
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

        public IAvapiResponse_ROCR_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_ROCR
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

    public class TechnicalIndicator_Type_ROCR
    {
        public string ROCR
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

    internal class AvapiResponse_ROCR_Content : IAvapiResponse_ROCR_Content
    {
        internal AvapiResponse_ROCR_Content()
        {
           MetaData = new MetaData_Type_ROCR();
           TechnicalIndicator = new List<TechnicalIndicator_Type_ROCR>();
        }

       public MetaData_Type_ROCR MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_ROCR> TechnicalIndicator
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

	public class Impl_ROCR : Int_ROCR
	{
		const string s_function = "ROCR";

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

		private static readonly Lazy<Impl_ROCR> s_Impl_ROCR =
			new Lazy<Impl_ROCR>(() => new Impl_ROCR());
		public static Impl_ROCR Instance
		{
			get
			{
				return s_Impl_ROCR.Value;
			}
		}
		private Impl_ROCR()
		{
		}

		internal static readonly IDictionary s_ROCR_interval_translation
			 = new Dictionary<Const_ROCR.ROCR_interval, string>()
		{
			{
				Const_ROCR.ROCR_interval.none,
				null
			},
			{
				Const_ROCR.ROCR_interval.n_1min,
				"1min"
			},
			{
				Const_ROCR.ROCR_interval.n_5min,
				"5min"
			},
			{
				Const_ROCR.ROCR_interval.n_15min,
				"15min"
			},
			{
				Const_ROCR.ROCR_interval.n_30min,
				"30min"
			},
			{
				Const_ROCR.ROCR_interval.n_60min,
				"60min"
			},
			{
				Const_ROCR.ROCR_interval.daily,
				"daily"
			},
			{
				Const_ROCR.ROCR_interval.weekly,
				"weekly"
			},
			{
				Const_ROCR.ROCR_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_ROCR_series_type_translation
			 = new Dictionary<Const_ROCR.ROCR_series_type, string>()
		{
			{
				Const_ROCR.ROCR_series_type.none,
				null
			},
			{
				Const_ROCR.ROCR_series_type.close,
				"close"
			},
			{
				Const_ROCR.ROCR_series_type.open,
				"open"
			},
			{
				Const_ROCR.ROCR_series_type.high,
				"high"
			},
			{
				Const_ROCR.ROCR_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_ROCR Query(
			string symbol,
			Const_ROCR.ROCR_interval interval,
			int time_period,
			Const_ROCR.ROCR_series_type series_type)
		{
			string current_interval = s_ROCR_interval_translation[interval] as string;
			string current_series_type = s_ROCR_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_ROCR> QueryAsync(
			string symbol,
			Const_ROCR.ROCR_interval interval,
			int time_period,
			Const_ROCR.ROCR_series_type series_type)
		{
			string current_interval = s_ROCR_interval_translation[interval] as string;
			string current_series_type = s_ROCR_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_ROCR QueryPrimitive(
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

			IAvapiResponse_ROCR ret = new AvapiResponse_ROCR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_ROCR> QueryPrimitiveAsync(
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
			IAvapiResponse_ROCR ret = new AvapiResponse_ROCR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_ROCR_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_ROCR_Content ret = new AvapiResponse_ROCR_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: ROCR"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_ROCR technicalindicator = new TechnicalIndicator_Type_ROCR
                    {
                        DateTime = ((JProperty)result).Name,
                        ROCR = (string)result.First["ROCR"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}