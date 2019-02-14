using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiAD
{
    internal class AvapiResponse_AD : IAvapiResponse_AD
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

        public IAvapiResponse_AD_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_AD
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

    public class TechnicalIndicator_Type_AD
    {
        public string ChaikinAD
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

    internal class AvapiResponse_AD_Content : IAvapiResponse_AD_Content
    {
        internal AvapiResponse_AD_Content()
        {
           MetaData = new MetaData_Type_AD();
           TechnicalIndicator = new List<TechnicalIndicator_Type_AD>();
        }

       public MetaData_Type_AD MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_AD> TechnicalIndicator
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

	public class Impl_AD : Int_AD
	{
		const string s_function = "AD";

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

		private static readonly Lazy<Impl_AD> s_Impl_AD =
			new Lazy<Impl_AD>(() => new Impl_AD());
		public static Impl_AD Instance
		{
			get
			{
				return s_Impl_AD.Value;
			}
		}
		private Impl_AD()
		{
		}

		internal static readonly IDictionary s_AD_interval_translation
			 = new Dictionary<Const_AD.AD_interval, string>()
		{
			{
				Const_AD.AD_interval.none,
				null
			},
			{
				Const_AD.AD_interval.n_1min,
				"1min"
			},
			{
				Const_AD.AD_interval.n_5min,
				"5min"
			},
			{
				Const_AD.AD_interval.n_15min,
				"15min"
			},
			{
				Const_AD.AD_interval.n_30min,
				"30min"
			},
			{
				Const_AD.AD_interval.n_60min,
				"60min"
			},
			{
				Const_AD.AD_interval.daily,
				"daily"
			},
			{
				Const_AD.AD_interval.weekly,
				"weekly"
			},
			{
				Const_AD.AD_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_AD Query(
			string symbol,
			Const_AD.AD_interval interval)
		{
			string current_interval = s_AD_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval);
		}

		public async Task<IAvapiResponse_AD> QueryAsync(
			string symbol,
			Const_AD.AD_interval interval)
		{
			string current_interval = s_AD_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval);
		}


		public IAvapiResponse_AD QueryPrimitive(
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

			IAvapiResponse_AD ret = new AvapiResponse_AD
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_AD> QueryPrimitiveAsync(
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
			IAvapiResponse_AD ret = new AvapiResponse_AD
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_AD_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_AD_Content ret = new AvapiResponse_AD_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: Chaikin A/D"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_AD technicalindicator = new TechnicalIndicator_Type_AD
                    {
                        DateTime = ((JProperty)result).Name,
                        ChaikinAD = (string)result.First["Chaikin A/D"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}