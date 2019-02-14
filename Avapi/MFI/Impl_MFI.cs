using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMFI
{
    internal class AvapiResponse_MFI : IAvapiResponse_MFI
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

        public IAvapiResponse_MFI_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MFI
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

    public class TechnicalIndicator_Type_MFI
    {
        public string MFI
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

    internal class AvapiResponse_MFI_Content : IAvapiResponse_MFI_Content
    {
        internal AvapiResponse_MFI_Content()
        {
           MetaData = new MetaData_Type_MFI();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MFI>();
        }

       public MetaData_Type_MFI MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MFI> TechnicalIndicator
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

	public class Impl_MFI : Int_MFI
	{
		const string s_function = "MFI";

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

		private static readonly Lazy<Impl_MFI> s_Impl_MFI =
			new Lazy<Impl_MFI>(() => new Impl_MFI());
		public static Impl_MFI Instance
		{
			get
			{
				return s_Impl_MFI.Value;
			}
		}
		private Impl_MFI()
		{
		}

		internal static readonly IDictionary s_MFI_interval_translation
			 = new Dictionary<Const_MFI.MFI_interval, string>()
		{
			{
				Const_MFI.MFI_interval.none,
				null
			},
			{
				Const_MFI.MFI_interval.n_1min,
				"1min"
			},
			{
				Const_MFI.MFI_interval.n_5min,
				"5min"
			},
			{
				Const_MFI.MFI_interval.n_15min,
				"15min"
			},
			{
				Const_MFI.MFI_interval.n_30min,
				"30min"
			},
			{
				Const_MFI.MFI_interval.n_60min,
				"60min"
			},
			{
				Const_MFI.MFI_interval.daily,
				"daily"
			},
			{
				Const_MFI.MFI_interval.weekly,
				"weekly"
			},
			{
				Const_MFI.MFI_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_MFI Query(
			string symbol,
			Const_MFI.MFI_interval interval,
			int time_period)
		{
			string current_interval = s_MFI_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,time_period);
		}

		public async Task<IAvapiResponse_MFI> QueryAsync(
			string symbol,
			Const_MFI.MFI_interval interval,
			int time_period)
		{
			string current_interval = s_MFI_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period);
		}


		public IAvapiResponse_MFI QueryPrimitive(
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

			IAvapiResponse_MFI ret = new AvapiResponse_MFI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MFI> QueryPrimitiveAsync(
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
			IAvapiResponse_MFI ret = new AvapiResponse_MFI
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MFI_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MFI_Content ret = new AvapiResponse_MFI_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MFI"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MFI technicalindicator = new TechnicalIndicator_Type_MFI
                    {
                        DateTime = ((JProperty)result).Name,
                        MFI = (string)result.First["MFI"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}