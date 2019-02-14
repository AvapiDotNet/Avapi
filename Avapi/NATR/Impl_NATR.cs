using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiNATR
{
    internal class AvapiResponse_NATR : IAvapiResponse_NATR
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

        public IAvapiResponse_NATR_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_NATR
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

    public class TechnicalIndicator_Type_NATR
    {
        public string NATR
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

    internal class AvapiResponse_NATR_Content : IAvapiResponse_NATR_Content
    {
        internal AvapiResponse_NATR_Content()
        {
           MetaData = new MetaData_Type_NATR();
           TechnicalIndicator = new List<TechnicalIndicator_Type_NATR>();
        }

       public MetaData_Type_NATR MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_NATR> TechnicalIndicator
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

	public class Impl_NATR : Int_NATR
	{
		const string s_function = "NATR";

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

		private static readonly Lazy<Impl_NATR> s_Impl_NATR =
			new Lazy<Impl_NATR>(() => new Impl_NATR());
		public static Impl_NATR Instance
		{
			get
			{
				return s_Impl_NATR.Value;
			}
		}
		private Impl_NATR()
		{
		}

		internal static readonly IDictionary s_NATR_interval_translation
			 = new Dictionary<Const_NATR.NATR_interval, string>()
		{
			{
				Const_NATR.NATR_interval.none,
				null
			},
			{
				Const_NATR.NATR_interval.n_1min,
				"1min"
			},
			{
				Const_NATR.NATR_interval.n_5min,
				"5min"
			},
			{
				Const_NATR.NATR_interval.n_15min,
				"15min"
			},
			{
				Const_NATR.NATR_interval.n_30min,
				"30min"
			},
			{
				Const_NATR.NATR_interval.n_60min,
				"60min"
			},
			{
				Const_NATR.NATR_interval.daily,
				"daily"
			},
			{
				Const_NATR.NATR_interval.weekly,
				"weekly"
			},
			{
				Const_NATR.NATR_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_NATR Query(
			string symbol,
			Const_NATR.NATR_interval interval,
			int time_period)
		{
			string current_interval = s_NATR_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_NATR> QueryAsync(
			string symbol,
			Const_NATR.NATR_interval interval,
			int time_period)
		{
			string current_interval = s_NATR_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_NATR QueryPrimitive(
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

			IAvapiResponse_NATR ret = new AvapiResponse_NATR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_NATR> QueryPrimitiveAsync(
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
			IAvapiResponse_NATR ret = new AvapiResponse_NATR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_NATR_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_NATR_Content ret = new AvapiResponse_NATR_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: NATR"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_NATR technicalindicator = new TechnicalIndicator_Type_NATR
                    {
                        DateTime = ((JProperty)result).Name,
                        NATR = (string)result.First["NATR"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}