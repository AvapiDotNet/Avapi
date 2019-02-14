using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiADXR
{
    internal class AvapiResponse_ADXR : IAvapiResponse_ADXR
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

        public IAvapiResponse_ADXR_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_ADXR
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

        public string TimeZone
        {
            internal set;
            get;
        }

    }

    public class TechnicalIndicator_Type_ADXR
    {
        public string ADXR
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

    internal class AvapiResponse_ADXR_Content : IAvapiResponse_ADXR_Content
    {
        internal AvapiResponse_ADXR_Content()
        {
           MetaData = new MetaData_Type_ADXR();
           TechnicalIndicator = new List<TechnicalIndicator_Type_ADXR>();
        }

       public MetaData_Type_ADXR MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_ADXR> TechnicalIndicator
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

	public class Impl_ADXR : Int_ADXR
	{
		const string s_function = "ADXR";

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

		private static readonly Lazy<Impl_ADXR> s_Impl_ADXR =
			new Lazy<Impl_ADXR>(() => new Impl_ADXR());
		public static Impl_ADXR Instance
		{
			get
			{
				return s_Impl_ADXR.Value;
			}
		}
		private Impl_ADXR()
		{
		}

		internal static readonly IDictionary s_ADXR_interval_translation
			 = new Dictionary<Const_ADXR.ADXR_interval, string>()
		{
			{
				Const_ADXR.ADXR_interval.none,
				null
			},
			{
				Const_ADXR.ADXR_interval.n_1min,
				"1min"
			},
			{
				Const_ADXR.ADXR_interval.n_5min,
				"5min"
			},
			{
				Const_ADXR.ADXR_interval.n_15min,
				"15min"
			},
			{
				Const_ADXR.ADXR_interval.n_30min,
				"30min"
			},
			{
				Const_ADXR.ADXR_interval.n_60min,
				"60min"
			},
			{
				Const_ADXR.ADXR_interval.daily,
				"daily"
			},
			{
				Const_ADXR.ADXR_interval.weekly,
				"weekly"
			},
			{
				Const_ADXR.ADXR_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_ADXR Query(
			string symbol,
			Const_ADXR.ADXR_interval interval,
			int time_period)
		{
			string current_interval = s_ADXR_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_ADXR> QueryAsync(
			string symbol,
			Const_ADXR.ADXR_interval interval,
			int time_period)
		{
			string current_interval = s_ADXR_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_ADXR QueryPrimitive(
			string symbol,
			string interval,
			int time_period)
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
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_ADXR ret = new AvapiResponse_ADXR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_ADXR> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period)
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
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_ADXR ret = new AvapiResponse_ADXR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_ADXR_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_ADXR_Content ret = new AvapiResponse_ADXR_Content();
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
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: ADXR"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_ADXR technicalindicator = new TechnicalIndicator_Type_ADXR
                    {
                        DateTime = ((JProperty)result).Name,
                        ADXR = (string)result.First["ADXR"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}