using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiDX
{
    internal class AvapiResponse_DX : IAvapiResponse_DX
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

        public IAvapiResponse_DX_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_DX
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

    public class TechnicalIndicator_Type_DX
    {
        public string DX
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

    internal class AvapiResponse_DX_Content : IAvapiResponse_DX_Content
    {
        internal AvapiResponse_DX_Content()
        {
           MetaData = new MetaData_Type_DX();
           TechnicalIndicator = new List<TechnicalIndicator_Type_DX>();
        }

       public MetaData_Type_DX MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_DX> TechnicalIndicator
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

	public class Impl_DX : Int_DX
	{
		const string s_function = "DX";

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

		private static readonly Lazy<Impl_DX> s_Impl_DX =
			new Lazy<Impl_DX>(() => new Impl_DX());
		public static Impl_DX Instance
		{
			get
			{
				return s_Impl_DX.Value;
			}
		}
		private Impl_DX()
		{
		}

		internal static readonly IDictionary s_DX_interval_translation
			 = new Dictionary<Const_DX.DX_interval, string>()
		{
			{
				Const_DX.DX_interval.none,
				null
			},
			{
				Const_DX.DX_interval.n_1min,
				"1min"
			},
			{
				Const_DX.DX_interval.n_5min,
				"5min"
			},
			{
				Const_DX.DX_interval.n_15min,
				"15min"
			},
			{
				Const_DX.DX_interval.n_30min,
				"30min"
			},
			{
				Const_DX.DX_interval.n_60min,
				"60min"
			},
			{
				Const_DX.DX_interval.daily,
				"daily"
			},
			{
				Const_DX.DX_interval.weekly,
				"weekly"
			},
			{
				Const_DX.DX_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_DX Query(
			string symbol,
			Const_DX.DX_interval interval,
			int time_period)
		{
			string current_interval = s_DX_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_DX> QueryAsync(
			string symbol,
			Const_DX.DX_interval interval,
			int time_period)
		{
			string current_interval = s_DX_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_DX QueryPrimitive(
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

			IAvapiResponse_DX ret = new AvapiResponse_DX
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_DX> QueryPrimitiveAsync(
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
			IAvapiResponse_DX ret = new AvapiResponse_DX
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_DX_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_DX_Content ret = new AvapiResponse_DX_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: DX"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_DX technicalindicator = new TechnicalIndicator_Type_DX
                    {
                        DateTime = ((JProperty)result).Name,
                        DX = (string)result.First["DX"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}