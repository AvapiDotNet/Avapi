using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMAMA
{
    internal class AvapiResponse_MAMA : IAvapiResponse_MAMA
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

        public IAvapiResponse_MAMA_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MAMA
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

        public string FastLimit
        {
            internal set;
            get;
        }

        public string SlowLimit
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

    public class TechnicalIndicator_Type_MAMA
    {
        public string MAMA
        {
            internal set;
            get;
        }

        public string FAMA
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

    internal class AvapiResponse_MAMA_Content : IAvapiResponse_MAMA_Content
    {
        internal AvapiResponse_MAMA_Content()
        {
           MetaData = new MetaData_Type_MAMA();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MAMA>();
        }

       public MetaData_Type_MAMA MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MAMA> TechnicalIndicator
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

	public class Impl_MAMA : Int_MAMA
	{
		const string s_function = "MAMA";

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

		private static readonly Lazy<Impl_MAMA> s_Impl_MAMA =
			new Lazy<Impl_MAMA>(() => new Impl_MAMA());
		public static Impl_MAMA Instance
		{
			get
			{
				return s_Impl_MAMA.Value;
			}
		}
		private Impl_MAMA()
		{
		}

		internal static readonly IDictionary s_MAMA_interval_translation
			 = new Dictionary<Const_MAMA.MAMA_interval, string>()
		{
			{
				Const_MAMA.MAMA_interval.none,
				null
			},
			{
				Const_MAMA.MAMA_interval.n_1min,
				"1min"
			},
			{
				Const_MAMA.MAMA_interval.n_5min,
				"5min"
			},
			{
				Const_MAMA.MAMA_interval.n_15min,
				"15min"
			},
			{
				Const_MAMA.MAMA_interval.n_30min,
				"30min"
			},
			{
				Const_MAMA.MAMA_interval.n_60min,
				"60min"
			},
			{
				Const_MAMA.MAMA_interval.daily,
				"daily"
			},
			{
				Const_MAMA.MAMA_interval.weekly,
				"weekly"
			},
			{
				Const_MAMA.MAMA_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_MAMA_series_type_translation
			 = new Dictionary<Const_MAMA.MAMA_series_type, string>()
		{
			{
				Const_MAMA.MAMA_series_type.none,
				null
			},
			{
				Const_MAMA.MAMA_series_type.close,
				"close"
			},
			{
				Const_MAMA.MAMA_series_type.open,
				"open"
			},
			{
				Const_MAMA.MAMA_series_type.high,
				"high"
			},
			{
				Const_MAMA.MAMA_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_MAMA Query(
			string symbol,
			Const_MAMA.MAMA_interval interval,
			Const_MAMA.MAMA_series_type series_type,
			float fastlimit = -1,
			float slowlimit = -1)
		{
			string current_interval = s_MAMA_interval_translation[interval] as string;
			string current_series_type = s_MAMA_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,current_series_type,fastlimit,slowlimit);
		}

		public async Task<IAvapiResponse_MAMA> QueryAsync(
			string symbol,
			Const_MAMA.MAMA_interval interval,
			Const_MAMA.MAMA_series_type series_type,
			float fastlimit = -1,
			float slowlimit = -1)
		{
			string current_interval = s_MAMA_interval_translation[interval] as string;
			string current_series_type = s_MAMA_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,current_series_type,fastlimit,slowlimit);
		}


		public IAvapiResponse_MAMA QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			float fastlimit = -1,
			float slowlimit = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastlimit",fastlimit.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowlimit",slowlimit.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_MAMA ret = new AvapiResponse_MAMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MAMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			float fastlimit = -1,
			float slowlimit = -1)
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
			getParameters.Add(new KeyValuePair<string, string>("fastlimit",fastlimit.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowlimit",slowlimit.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_MAMA ret = new AvapiResponse_MAMA
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MAMA_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MAMA_Content ret = new AvapiResponse_MAMA_Content();
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
                ret.MetaData.FastLimit = (string)metaData["5.1: Fast Limit"];
                ret.MetaData.SlowLimit = (string)metaData["5.2: Slow Limit"];
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MAMA"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MAMA technicalindicator = new TechnicalIndicator_Type_MAMA
                    {
                        DateTime = ((JProperty)result).Name,
                        MAMA = (string)result.First["MAMA"],
                        FAMA = (string)result.First["FAMA"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}