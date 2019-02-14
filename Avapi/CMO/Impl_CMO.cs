using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiCMO
{
    internal class AvapiResponse_CMO : IAvapiResponse_CMO
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

        public IAvapiResponse_CMO_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_CMO
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

    public class TechnicalIndicator_Type_CMO
    {
        public string CMO
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

    internal class AvapiResponse_CMO_Content : IAvapiResponse_CMO_Content
    {
        internal AvapiResponse_CMO_Content()
        {
           MetaData = new MetaData_Type_CMO();
           TechnicalIndicator = new List<TechnicalIndicator_Type_CMO>();
        }

       public MetaData_Type_CMO MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_CMO> TechnicalIndicator
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

	public class Impl_CMO : Int_CMO
	{
		const string s_function = "CMO";

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

		private static readonly Lazy<Impl_CMO> s_Impl_CMO =
			new Lazy<Impl_CMO>(() => new Impl_CMO());
		public static Impl_CMO Instance
		{
			get
			{
				return s_Impl_CMO.Value;
			}
		}
		private Impl_CMO()
		{
		}

		internal static readonly IDictionary s_CMO_interval_translation
			 = new Dictionary<Const_CMO.CMO_interval, string>()
		{
			{
				Const_CMO.CMO_interval.none,
				null
			},
			{
				Const_CMO.CMO_interval.n_1min,
				"1min"
			},
			{
				Const_CMO.CMO_interval.n_5min,
				"5min"
			},
			{
				Const_CMO.CMO_interval.n_15min,
				"15min"
			},
			{
				Const_CMO.CMO_interval.n_30min,
				"30min"
			},
			{
				Const_CMO.CMO_interval.n_60min,
				"60min"
			},
			{
				Const_CMO.CMO_interval.daily,
				"daily"
			},
			{
				Const_CMO.CMO_interval.weekly,
				"weekly"
			},
			{
				Const_CMO.CMO_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_CMO_series_type_translation
			 = new Dictionary<Const_CMO.CMO_series_type, string>()
		{
			{
				Const_CMO.CMO_series_type.none,
				null
			},
			{
				Const_CMO.CMO_series_type.close,
				"close"
			},
			{
				Const_CMO.CMO_series_type.open,
				"open"
			},
			{
				Const_CMO.CMO_series_type.high,
				"high"
			},
			{
				Const_CMO.CMO_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_CMO Query(
			string symbol,
			Const_CMO.CMO_interval interval,
			int time_period,
			Const_CMO.CMO_series_type series_type)
		{
			string current_interval = s_CMO_interval_translation[interval] as string;
			string current_series_type = s_CMO_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_CMO> QueryAsync(
			string symbol,
			Const_CMO.CMO_interval interval,
			int time_period,
			Const_CMO.CMO_series_type series_type)
		{
			string current_interval = s_CMO_interval_translation[interval] as string;
			string current_series_type = s_CMO_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_CMO QueryPrimitive(
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

			IAvapiResponse_CMO ret = new AvapiResponse_CMO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_CMO> QueryPrimitiveAsync(
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
			IAvapiResponse_CMO ret = new AvapiResponse_CMO
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_CMO_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_CMO_Content ret = new AvapiResponse_CMO_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: CMO"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_CMO technicalindicator = new TechnicalIndicator_Type_CMO
                    {
                        DateTime = ((JProperty)result).Name,
                        CMO = (string)result.First["CMO"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}