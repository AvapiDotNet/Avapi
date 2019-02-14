using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiOBV
{
    internal class AvapiResponse_OBV : IAvapiResponse_OBV
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

        public IAvapiResponse_OBV_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_OBV
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

        public string TimeZone
        {
            internal set;
            get;
        }

    }

    public class TechnicalIndicator_Type_OBV
    {
        public string OBV
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

    internal class AvapiResponse_OBV_Content : IAvapiResponse_OBV_Content
    {
        internal AvapiResponse_OBV_Content()
        {
           MetaData = new MetaData_Type_OBV();
           TechnicalIndicator = new List<TechnicalIndicator_Type_OBV>();
        }

       public MetaData_Type_OBV MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_OBV> TechnicalIndicator
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

	public class Impl_OBV : Int_OBV
	{
		const string s_function = "OBV";

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

		private static readonly Lazy<Impl_OBV> s_Impl_OBV =
			new Lazy<Impl_OBV>(() => new Impl_OBV());
		public static Impl_OBV Instance
		{
			get
			{
				return s_Impl_OBV.Value;
			}
		}
		private Impl_OBV()
		{
		}

		internal static readonly IDictionary s_OBV_interval_translation
			 = new Dictionary<Const_OBV.OBV_interval, string>()
		{
			{
				Const_OBV.OBV_interval.none,
				null
			},
			{
				Const_OBV.OBV_interval.n_1min,
				"1min"
			},
			{
				Const_OBV.OBV_interval.n_5min,
				"5min"
			},
			{
				Const_OBV.OBV_interval.n_15min,
				"15min"
			},
			{
				Const_OBV.OBV_interval.n_30min,
				"30min"
			},
			{
				Const_OBV.OBV_interval.n_60min,
				"60min"
			},
			{
				Const_OBV.OBV_interval.daily,
				"daily"
			},
			{
				Const_OBV.OBV_interval.weekly,
				"weekly"
			},
			{
				Const_OBV.OBV_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_OBV Query(
			string symbol,
			Const_OBV.OBV_interval interval)
		{
			string current_interval = s_OBV_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval);
		}

		public async Task<IAvapiResponse_OBV> QueryAsync(
			string symbol,
			Const_OBV.OBV_interval interval)
		{
			string current_interval = s_OBV_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval);
		}


		public IAvapiResponse_OBV QueryPrimitive(
			string symbol,
			string interval)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_OBV ret = new AvapiResponse_OBV
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_OBV> QueryPrimitiveAsync(
			string symbol,
			string interval)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_OBV ret = new AvapiResponse_OBV
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_OBV_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_OBV_Content ret = new AvapiResponse_OBV_Content();
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
                ret.MetaData.TimeZone = (string)metaData["5: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: OBV"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_OBV technicalindicator = new TechnicalIndicator_Type_OBV
                    {
                        DateTime = ((JProperty)result).Name,
                        OBV = (string)result.First["OBV"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}