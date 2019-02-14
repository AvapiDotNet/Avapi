using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiULTOSC
{
    internal class AvapiResponse_ULTOSC : IAvapiResponse_ULTOSC
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

        public IAvapiResponse_ULTOSC_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_ULTOSC
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

        public string TimePeriod1
        {
            internal set;
            get;
        }

        public string TimePeriod2
        {
            internal set;
            get;
        }

        public string TimePeriod3
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

    public class TechnicalIndicator_Type_ULTOSC
    {
        public string ULTOSC
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

    internal class AvapiResponse_ULTOSC_Content : IAvapiResponse_ULTOSC_Content
    {
        internal AvapiResponse_ULTOSC_Content()
        {
           MetaData = new MetaData_Type_ULTOSC();
           TechnicalIndicator = new List<TechnicalIndicator_Type_ULTOSC>();
        }

       public MetaData_Type_ULTOSC MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_ULTOSC> TechnicalIndicator
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

	public class Impl_ULTOSC : Int_ULTOSC
	{
		const string s_function = "ULTOSC";

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

		private static readonly Lazy<Impl_ULTOSC> s_Impl_ULTOSC =
			new Lazy<Impl_ULTOSC>(() => new Impl_ULTOSC());
		public static Impl_ULTOSC Instance
		{
			get
			{
				return s_Impl_ULTOSC.Value;
			}
		}
		private Impl_ULTOSC()
		{
		}

		internal static readonly IDictionary s_ULTOSC_interval_translation
			 = new Dictionary<Const_ULTOSC.ULTOSC_interval, string>()
		{
			{
				Const_ULTOSC.ULTOSC_interval.none,
				null
			},
			{
				Const_ULTOSC.ULTOSC_interval.n_1min,
				"1min"
			},
			{
				Const_ULTOSC.ULTOSC_interval.n_5min,
				"5min"
			},
			{
				Const_ULTOSC.ULTOSC_interval.n_15min,
				"15min"
			},
			{
				Const_ULTOSC.ULTOSC_interval.n_30min,
				"30min"
			},
			{
				Const_ULTOSC.ULTOSC_interval.n_60min,
				"60min"
			},
			{
				Const_ULTOSC.ULTOSC_interval.daily,
				"daily"
			},
			{
				Const_ULTOSC.ULTOSC_interval.weekly,
				"weekly"
			},
			{
				Const_ULTOSC.ULTOSC_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_ULTOSC Query(
			string symbol,
			Const_ULTOSC.ULTOSC_interval interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1)
		{
			string current_interval = s_ULTOSC_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,timeperiod1,timeperiod2,timeperiod3);
		}

		public async Task<IAvapiResponse_ULTOSC> QueryAsync(
			string symbol,
			Const_ULTOSC.ULTOSC_interval interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1)
		{
			string current_interval = s_ULTOSC_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,timeperiod1,timeperiod2,timeperiod3);
		}


		public IAvapiResponse_ULTOSC QueryPrimitive(
			string symbol,
			string interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod1",timeperiod1.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod2",timeperiod2.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod3",timeperiod3.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_ULTOSC ret = new AvapiResponse_ULTOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_ULTOSC> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod1",timeperiod1.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod2",timeperiod2.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("timeperiod3",timeperiod3.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_ULTOSC ret = new AvapiResponse_ULTOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_ULTOSC_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_ULTOSC_Content ret = new AvapiResponse_ULTOSC_Content();
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
                ret.MetaData.TimePeriod1 = (string)metaData["5.1: Time Period 1"];
                ret.MetaData.TimePeriod2 = (string)metaData["5.2: Time Period 2"];
                ret.MetaData.TimePeriod3 = (string)metaData["5.3: Time Period 3"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: ULTOSC"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_ULTOSC technicalindicator = new TechnicalIndicator_Type_ULTOSC
                    {
                        DateTime = ((JProperty)result).Name,
                        ULTOSC = (string)result.First["ULTOSC"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}