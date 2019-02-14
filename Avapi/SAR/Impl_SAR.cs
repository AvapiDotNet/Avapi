using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSAR
{
    internal class AvapiResponse_SAR : IAvapiResponse_SAR
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

        public IAvapiResponse_SAR_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_SAR
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

        public string Acceleration
        {
            internal set;
            get;
        }

        public string Maximum
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

    public class TechnicalIndicator_Type_SAR
    {
        public string SAR
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

    internal class AvapiResponse_SAR_Content : IAvapiResponse_SAR_Content
    {
        internal AvapiResponse_SAR_Content()
        {
           MetaData = new MetaData_Type_SAR();
           TechnicalIndicator = new List<TechnicalIndicator_Type_SAR>();
        }

       public MetaData_Type_SAR MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_SAR> TechnicalIndicator
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

	public class Impl_SAR : Int_SAR
	{
		const string s_function = "SAR";

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

		private static readonly Lazy<Impl_SAR> s_Impl_SAR =
			new Lazy<Impl_SAR>(() => new Impl_SAR());
		public static Impl_SAR Instance
		{
			get
			{
				return s_Impl_SAR.Value;
			}
		}
		private Impl_SAR()
		{
		}

		internal static readonly IDictionary s_SAR_interval_translation
			 = new Dictionary<Const_SAR.SAR_interval, string>()
		{
			{
				Const_SAR.SAR_interval.none,
				null
			},
			{
				Const_SAR.SAR_interval.n_1min,
				"1min"
			},
			{
				Const_SAR.SAR_interval.n_5min,
				"5min"
			},
			{
				Const_SAR.SAR_interval.n_15min,
				"15min"
			},
			{
				Const_SAR.SAR_interval.n_30min,
				"30min"
			},
			{
				Const_SAR.SAR_interval.n_60min,
				"60min"
			},
			{
				Const_SAR.SAR_interval.daily,
				"daily"
			},
			{
				Const_SAR.SAR_interval.weekly,
				"weekly"
			},
			{
				Const_SAR.SAR_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_SAR Query(
			string symbol,
			Const_SAR.SAR_interval interval,
			float acceleration = -1,
			float maximum = -1)
		{
			string current_interval = s_SAR_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval,acceleration,maximum);
		}

		public async Task<IAvapiResponse_SAR> QueryAsync(
			string symbol,
			Const_SAR.SAR_interval interval,
			float acceleration = -1,
			float maximum = -1)
		{
			string current_interval = s_SAR_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,acceleration,maximum);
		}


		public IAvapiResponse_SAR QueryPrimitive(
			string symbol,
			string interval,
			float acceleration = -1,
			float maximum = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("acceleration",acceleration.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("maximum",maximum.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_SAR ret = new AvapiResponse_SAR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_SAR> QueryPrimitiveAsync(
			string symbol,
			string interval,
			float acceleration = -1,
			float maximum = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("acceleration",acceleration.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("maximum",maximum.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_SAR ret = new AvapiResponse_SAR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_SAR_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_SAR_Content ret = new AvapiResponse_SAR_Content();
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
                ret.MetaData.Acceleration = (string)metaData["5.1: Acceleration"];
                ret.MetaData.Maximum = (string)metaData["5.2: Maximum"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: SAR"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_SAR technicalindicator = new TechnicalIndicator_Type_SAR
                    {
                        DateTime = ((JProperty)result).Name,
                        SAR = (string)result.First["SAR"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}