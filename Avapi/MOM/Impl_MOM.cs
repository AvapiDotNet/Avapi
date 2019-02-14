using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiMOM
{
    internal class AvapiResponse_MOM : IAvapiResponse_MOM
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

        public IAvapiResponse_MOM_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_MOM
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

        public string SeriesType
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

    public class TechnicalIndicator_Type_MOM
    {
        public string MOM
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

    internal class AvapiResponse_MOM_Content : IAvapiResponse_MOM_Content
    {
        internal AvapiResponse_MOM_Content()
        {
           MetaData = new MetaData_Type_MOM();
           TechnicalIndicator = new List<TechnicalIndicator_Type_MOM>();
        }

       public MetaData_Type_MOM MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_MOM> TechnicalIndicator
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

	public class Impl_MOM : Int_MOM
	{
		const string s_function = "MOM";

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

		private static readonly Lazy<Impl_MOM> s_Impl_MOM =
			new Lazy<Impl_MOM>(() => new Impl_MOM());
		public static Impl_MOM Instance
		{
			get
			{
				return s_Impl_MOM.Value;
			}
		}
		private Impl_MOM()
		{
		}

		internal static readonly IDictionary s_MOM_interval_translation
			 = new Dictionary<Const_MOM.MOM_interval, string>()
		{
			{
				Const_MOM.MOM_interval.none,
				null
			},
			{
				Const_MOM.MOM_interval.n_1min,
				"1min"
			},
			{
				Const_MOM.MOM_interval.n_5min,
				"5min"
			},
			{
				Const_MOM.MOM_interval.n_15min,
				"15min"
			},
			{
				Const_MOM.MOM_interval.n_30min,
				"30min"
			},
			{
				Const_MOM.MOM_interval.n_60min,
				"60min"
			},
			{
				Const_MOM.MOM_interval.daily,
				"daily"
			},
			{
				Const_MOM.MOM_interval.weekly,
				"weekly"
			},
			{
				Const_MOM.MOM_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_MOM_series_type_translation
			 = new Dictionary<Const_MOM.MOM_series_type, string>()
		{
			{
				Const_MOM.MOM_series_type.none,
				null
			},
			{
				Const_MOM.MOM_series_type.close,
				"close"
			},
			{
				Const_MOM.MOM_series_type.open,
				"open"
			},
			{
				Const_MOM.MOM_series_type.high,
				"high"
			},
			{
				Const_MOM.MOM_series_type.low,
				"low"
			}
		};

		public IAvapiResponse_MOM Query(
			string symbol,
			Const_MOM.MOM_interval interval,
			int time_period,
			Const_MOM.MOM_series_type series_type)
		{
			string current_interval = s_MOM_interval_translation[interval] as string;
			string current_series_type = s_MOM_series_type_translation[series_type] as string;

			return QueryPrimitive(symbol,current_interval,time_period,current_series_type);
		}

		public async Task<IAvapiResponse_MOM> QueryAsync(
			string symbol,
			Const_MOM.MOM_interval interval,
			int time_period,
			Const_MOM.MOM_series_type series_type)
		{
			string current_interval = s_MOM_interval_translation[interval] as string;
			string current_series_type = s_MOM_series_type_translation[series_type] as string;

			return await QueryPrimitiveAsync(symbol,current_interval,time_period,current_series_type);
		}


		public IAvapiResponse_MOM QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type)
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
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_MOM ret = new AvapiResponse_MOM
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_MOM> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type)
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
			getParameters.Add(new KeyValuePair<string, string>("series_type",series_type));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_MOM ret = new AvapiResponse_MOM
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_MOM_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_MOM_Content ret = new AvapiResponse_MOM_Content();
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
                ret.MetaData.SeriesType = (string)metaData["6: Series Type"];
                ret.MetaData.TimeZone = (string)metaData["7: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: MOM"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_MOM technicalindicator = new TechnicalIndicator_Type_MOM
                    {
                        DateTime = ((JProperty)result).Name,
                        MOM = (string)result.First["MOM"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}