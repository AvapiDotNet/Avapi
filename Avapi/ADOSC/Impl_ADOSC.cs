using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiADOSC
{
    internal class AvapiResponse_ADOSC : IAvapiResponse_ADOSC
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

        public IAvapiResponse_ADOSC_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_ADOSC
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

        public string FastKPeriod
        {
            internal set;
            get;
        }

        public string SlowKPeriod
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

    public class TechnicalIndicator_Type_ADOSC
    {
        public string ADOSC
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

    internal class AvapiResponse_ADOSC_Content : IAvapiResponse_ADOSC_Content
    {
        internal AvapiResponse_ADOSC_Content()
        {
           MetaData = new MetaData_Type_ADOSC();
           TechnicalIndicator = new List<TechnicalIndicator_Type_ADOSC>();
        }

       public MetaData_Type_ADOSC MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_ADOSC> TechnicalIndicator
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

	public class Impl_ADOSC : Int_ADOSC
	{
		const string s_function = "ADOSC";

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

		private static readonly Lazy<Impl_ADOSC> s_Impl_ADOSC =
			new Lazy<Impl_ADOSC>(() => new Impl_ADOSC());
		public static Impl_ADOSC Instance
		{
			get
			{
				return s_Impl_ADOSC.Value;
			}
		}
		private Impl_ADOSC()
		{
		}

		internal static readonly IDictionary s_ADOSC_interval_translation
			 = new Dictionary<Const_ADOSC.ADOSC_interval, string>()
		{
			{
				Const_ADOSC.ADOSC_interval.none,
				null
			},
			{
				Const_ADOSC.ADOSC_interval.n_1min,
				"1min"
			},
			{
				Const_ADOSC.ADOSC_interval.n_5min,
				"5min"
			},
			{
				Const_ADOSC.ADOSC_interval.n_15min,
				"15min"
			},
			{
				Const_ADOSC.ADOSC_interval.n_30min,
				"30min"
			},
			{
				Const_ADOSC.ADOSC_interval.n_60min,
				"60min"
			},
			{
				Const_ADOSC.ADOSC_interval.daily,
				"daily"
			},
			{
				Const_ADOSC.ADOSC_interval.weekly,
				"weekly"
			},
			{
				Const_ADOSC.ADOSC_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_ADOSC Query(
			string symbol,
			Const_ADOSC.ADOSC_interval interval,
			int fastperiod = -1,
			int slowperiod = -1)
		{
			string current_interval = s_ADOSC_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,fastperiod,slowperiod);
		}

		public async Task<IAvapiResponse_ADOSC> QueryAsync(
			string symbol,
			Const_ADOSC.ADOSC_interval interval,
			int fastperiod = -1,
			int slowperiod = -1)
		{
			string current_interval = s_ADOSC_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,fastperiod,slowperiod);
		}


		public IAvapiResponse_ADOSC QueryPrimitive(
			string symbol,
			string interval,
			int fastperiod = -1,
			int slowperiod = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastperiod",fastperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowperiod",slowperiod.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_ADOSC ret = new AvapiResponse_ADOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_ADOSC> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int fastperiod = -1,
			int slowperiod = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastperiod",fastperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("slowperiod",slowperiod.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_ADOSC ret = new AvapiResponse_ADOSC
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_ADOSC_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_ADOSC_Content ret = new AvapiResponse_ADOSC_Content();
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
                ret.MetaData.FastKPeriod = (string)metaData["5.1: FastK Perio"];
                ret.MetaData.SlowKPeriod = (string)metaData["5.2: SlowK Period"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: ADOSC"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_ADOSC technicalindicator = new TechnicalIndicator_Type_ADOSC
                    {
                        DateTime = ((JProperty)result).Name,
                        ADOSC = (string)result.First["ADOSC"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}