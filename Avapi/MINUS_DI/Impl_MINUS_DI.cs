using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMINUS_DI
{
    internal class AvapiResponse_MINUS_DI : IAvapiResponse_MINUS_DI
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

        public IAvapiResponse_MINUS_DI_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MINUS_DI
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

    public class TechnicalIndicator_Type_MINUS_DI
    {
        public string MINUS_DI
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

    internal class AvapiResponse_MINUS_DI_Content : IAvapiResponse_MINUS_DI_Content
    {
        internal AvapiResponse_MINUS_DI_Content()
        {
           MetaData = new MetaData_Type_MINUS_DI();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MINUS_DI>();
        }

       public MetaData_Type_MINUS_DI MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MINUS_DI> TechnicalIndicator
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

	public class Impl_MINUS_DI : Int_MINUS_DI
	{
		const string s_function = "MINUS_DI";

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

		private static readonly Lazy<Impl_MINUS_DI> s_Impl_MINUS_DI =
			new Lazy<Impl_MINUS_DI>(() => new Impl_MINUS_DI());
		public static Impl_MINUS_DI Instance
		{
			get
			{
				return s_Impl_MINUS_DI.Value;
			}
		}
		private Impl_MINUS_DI()
		{
		}

		internal static readonly IDictionary s_MINUS_DI_interval_translation
			 = new Dictionary<Const_MINUS_DI.MINUS_DI_interval, string>()
		{
			{
				Const_MINUS_DI.MINUS_DI_interval.none,
				null
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.n_1min,
				"1min"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.n_5min,
				"5min"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.n_15min,
				"15min"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.n_30min,
				"30min"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.n_60min,
				"60min"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.daily,
				"daily"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.weekly,
				"weekly"
			},
			{
				Const_MINUS_DI.MINUS_DI_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_MINUS_DI Query(
			string symbol,
			Const_MINUS_DI.MINUS_DI_interval interval,
			int time_period)
		{
			string current_interval = s_MINUS_DI_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_MINUS_DI> QueryAsync(
			string symbol,
			Const_MINUS_DI.MINUS_DI_interval interval,
			int time_period)
		{
			string current_interval = s_MINUS_DI_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_MINUS_DI QueryPrimitive(
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

			IAvapiResponse_MINUS_DI ret = new AvapiResponse_MINUS_DI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MINUS_DI> QueryPrimitiveAsync(
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
			IAvapiResponse_MINUS_DI ret = new AvapiResponse_MINUS_DI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MINUS_DI_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MINUS_DI_Content ret = new AvapiResponse_MINUS_DI_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MINUS_DI"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MINUS_DI technicalindicator = new TechnicalIndicator_Type_MINUS_DI
                    {
                        DateTime = ((JProperty)result).Name,
                        MINUS_DI = (string)result.First["MINUS_DI"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}