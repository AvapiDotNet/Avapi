using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiAROONOSC
{
    internal class AvapiResponse_AROONOSC : IAvapiResponse_AROONOSC
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

        public IAvapiResponse_AROONOSC_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_AROONOSC
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

    public class TechnicalIndicator_Type_AROONOSC
    {
        public string AROONOSC
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

    internal class AvapiResponse_AROONOSC_Content : IAvapiResponse_AROONOSC_Content
    {
        internal AvapiResponse_AROONOSC_Content()
        {
           MetaData = new MetaData_Type_AROONOSC();
           TechnicalIndicator = new List<TechnicalIndicator_Type_AROONOSC>();
        }

       public MetaData_Type_AROONOSC MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_AROONOSC> TechnicalIndicator
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

	public class Impl_AROONOSC : Int_AROONOSC
	{
		const string s_function = "AROONOSC";

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

		private static readonly Lazy<Impl_AROONOSC> s_Impl_AROONOSC =
			new Lazy<Impl_AROONOSC>(() => new Impl_AROONOSC());
		public static Impl_AROONOSC Instance
		{
			get
			{
				return s_Impl_AROONOSC.Value;
			}
		}
		private Impl_AROONOSC()
		{
		}

		internal static readonly IDictionary s_AROONOSC_interval_translation
			 = new Dictionary<Const_AROONOSC.AROONOSC_interval, string>()
		{
			{
				Const_AROONOSC.AROONOSC_interval.none,
				null
			},
			{
				Const_AROONOSC.AROONOSC_interval.n_1min,
				"1min"
			},
			{
				Const_AROONOSC.AROONOSC_interval.n_5min,
				"5min"
			},
			{
				Const_AROONOSC.AROONOSC_interval.n_15min,
				"15min"
			},
			{
				Const_AROONOSC.AROONOSC_interval.n_30min,
				"30min"
			},
			{
				Const_AROONOSC.AROONOSC_interval.n_60min,
				"60min"
			},
			{
				Const_AROONOSC.AROONOSC_interval.daily,
				"daily"
			},
			{
				Const_AROONOSC.AROONOSC_interval.weekly,
				"weekly"
			},
			{
				Const_AROONOSC.AROONOSC_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_AROONOSC Query(
			string symbol,
			Const_AROONOSC.AROONOSC_interval interval,
			int time_period)
		{
			string current_interval = s_AROONOSC_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_AROONOSC> QueryAsync(
			string symbol,
			Const_AROONOSC.AROONOSC_interval interval,
			int time_period)
		{
			string current_interval = s_AROONOSC_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_AROONOSC QueryPrimitive(
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

			IAvapiResponse_AROONOSC ret = new AvapiResponse_AROONOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_AROONOSC> QueryPrimitiveAsync(
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
			IAvapiResponse_AROONOSC ret = new AvapiResponse_AROONOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_AROONOSC_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_AROONOSC_Content ret = new AvapiResponse_AROONOSC_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: AROONOSC"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_AROONOSC technicalindicator = new TechnicalIndicator_Type_AROONOSC
                    {
                        DateTime = ((JProperty)result).Name,
                        AROONOSC = (string)result.First["AROONOSC"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}