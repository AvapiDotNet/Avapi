using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiTRANGE
{
    internal class AvapiResponse_TRANGE : IAvapiResponse_TRANGE
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

        public IAvapiResponse_TRANGE_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_TRANGE
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

    public class TechnicalIndicator_Type_TRANGE
    {
        public string TRANGE
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

    internal class AvapiResponse_TRANGE_Content : IAvapiResponse_TRANGE_Content
    {
        internal AvapiResponse_TRANGE_Content()
        {
           MetaData = new MetaData_Type_TRANGE();
           TechnicalIndicator = new List<TechnicalIndicator_Type_TRANGE>();
        }

       public MetaData_Type_TRANGE MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_TRANGE> TechnicalIndicator
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

	public class Impl_TRANGE : Int_TRANGE
	{
		const string s_function = "TRANGE";

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

		private static readonly Lazy<Impl_TRANGE> s_Impl_TRANGE =
			new Lazy<Impl_TRANGE>(() => new Impl_TRANGE());
		public static Impl_TRANGE Instance
		{
			get
			{
				return s_Impl_TRANGE.Value;
			}
		}
		private Impl_TRANGE()
		{
		}

		internal static readonly IDictionary s_TRANGE_interval_translation
			 = new Dictionary<Const_TRANGE.TRANGE_interval, string>()
		{
			{
				Const_TRANGE.TRANGE_interval.none,
				null
			},
			{
				Const_TRANGE.TRANGE_interval.n_1min,
				"1min"
			},
			{
				Const_TRANGE.TRANGE_interval.n_5min,
				"5min"
			},
			{
				Const_TRANGE.TRANGE_interval.n_15min,
				"15min"
			},
			{
				Const_TRANGE.TRANGE_interval.n_30min,
				"30min"
			},
			{
				Const_TRANGE.TRANGE_interval.n_60min,
				"60min"
			},
			{
				Const_TRANGE.TRANGE_interval.daily,
				"daily"
			},
			{
				Const_TRANGE.TRANGE_interval.weekly,
				"weekly"
			},
			{
				Const_TRANGE.TRANGE_interval.monthly,
				"monthly"
			}
		};

		public IAvapiResponse_TRANGE Query(
			string symbol,
			Const_TRANGE.TRANGE_interval interval)
		{
			string current_interval = s_TRANGE_interval_translation[interval] as string;

			return QueryPrimitive(symbol,current_interval);
		}

		public async Task<IAvapiResponse_TRANGE> QueryAsync(
			string symbol,
			Const_TRANGE.TRANGE_interval interval)
		{
			string current_interval = s_TRANGE_interval_translation[interval] as string;

			return await QueryPrimitiveAsync(symbol,current_interval);
		}


		public IAvapiResponse_TRANGE QueryPrimitive(
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

			IAvapiResponse_TRANGE ret = new AvapiResponse_TRANGE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_TRANGE> QueryPrimitiveAsync(
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
			IAvapiResponse_TRANGE ret = new AvapiResponse_TRANGE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_TRANGE_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_TRANGE_Content ret = new AvapiResponse_TRANGE_Content();
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
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: TRANGE"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_TRANGE technicalindicator = new TechnicalIndicator_Type_TRANGE
                    {
                        DateTime = ((JProperty)result).Name,
                        TRANGE = (string)result.First["TRANGE"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}