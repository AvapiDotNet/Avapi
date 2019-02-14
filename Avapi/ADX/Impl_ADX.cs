using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiADX
{
    internal class AvapiResponse_ADX : IAvapiResponse_ADX
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

        public IAvapiResponse_ADX_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_ADX
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

    public class TechnicalIndicator_Type_ADX
    {
        public string ADX
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

    internal class AvapiResponse_ADX_Content : IAvapiResponse_ADX_Content
    {
        internal AvapiResponse_ADX_Content()
        {
           MetaData = new MetaData_Type_ADX();
           TechnicalIndicator = new List<TechnicalIndicator_Type_ADX>();
        }

       public MetaData_Type_ADX MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_ADX> TechnicalIndicator
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

	public class Impl_ADX : Int_ADX
	{
		const string s_function = "ADX";

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

		private static readonly Lazy<Impl_ADX> s_Impl_ADX =
			new Lazy<Impl_ADX>(() => new Impl_ADX());
		public static Impl_ADX Instance
		{
			get
			{
				return s_Impl_ADX.Value;
			}
		}
		private Impl_ADX()
		{
		}

		internal static readonly IDictionary s_ADX_interval_translation
			 = new Dictionary<Const_ADX.ADX_interval, string>()
		{
			{
				Const_ADX.ADX_interval.none,
				null
			},
			{
				Const_ADX.ADX_interval.n_1min,
				"1min"
			},
			{
				Const_ADX.ADX_interval.n_5min,
				"5min"
			},
			{
				Const_ADX.ADX_interval.n_15min,
				"15min"
			},
			{
				Const_ADX.ADX_interval.n_30min,
				"30min"
			},
			{
				Const_ADX.ADX_interval.n_60min,
				"60min"
			},
			{
				Const_ADX.ADX_interval.daily,
				"daily"
			},
			{
				Const_ADX.ADX_interval.weekly,
				"weekly"
			},
			{
				Const_ADX.ADX_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_ADX Query(
			string symbol,
			Const_ADX.ADX_interval interval,
			int time_period)
		{
			string current_interval = s_ADX_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_ADX> QueryAsync(
			string symbol,
			Const_ADX.ADX_interval interval,
			int time_period)
		{
			string current_interval = s_ADX_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_ADX QueryPrimitive(
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

			IAvapiResponse_ADX ret = new AvapiResponse_ADX
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_ADX> QueryPrimitiveAsync(
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
			IAvapiResponse_ADX ret = new AvapiResponse_ADX
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_ADX_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_ADX_Content ret = new AvapiResponse_ADX_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: ADX"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_ADX technicalindicator = new TechnicalIndicator_Type_ADX
                    {
                        DateTime = ((JProperty)result).Name,
                        ADX = (string)result.First["ADX"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}