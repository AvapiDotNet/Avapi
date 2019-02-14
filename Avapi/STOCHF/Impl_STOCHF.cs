using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSTOCHF
{
    internal class AvapiResponse_STOCHF : IAvapiResponse_STOCHF
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

        public IAvapiResponse_STOCHF_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_STOCHF
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

        public string FastDPeriod
        {
            internal set;
            get;
        }

        public string FastDMAType
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

    public class TechnicalIndicator_Type_STOCHF
    {
        public string FastK
        {
            internal set;
            get;
        }

        public string FastD
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

    internal class AvapiResponse_STOCHF_Content : IAvapiResponse_STOCHF_Content
    {
        internal AvapiResponse_STOCHF_Content()
        {
           MetaData = new MetaData_Type_STOCHF();
           TechnicalIndicator = new List<TechnicalIndicator_Type_STOCHF>();
        }

       public MetaData_Type_STOCHF MetaData
        {
            internal set;
            get;
        }

       public IList<TechnicalIndicator_Type_STOCHF> TechnicalIndicator
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

	public class Impl_STOCHF : Int_STOCHF
	{
		const string s_function = "STOCHF";

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

		private static readonly Lazy<Impl_STOCHF> s_Impl_STOCHF =
			new Lazy<Impl_STOCHF>(() => new Impl_STOCHF());
		public static Impl_STOCHF Instance
		{
			get
			{
				return s_Impl_STOCHF.Value;
			}
		}
		private Impl_STOCHF()
		{
		}

		internal static readonly IDictionary s_STOCHF_interval_translation
			 = new Dictionary<Const_STOCHF.STOCHF_interval, string>()
		{
			{
				Const_STOCHF.STOCHF_interval.none,
				null
			},
			{
				Const_STOCHF.STOCHF_interval.n_1min,
				"1min"
			},
			{
				Const_STOCHF.STOCHF_interval.n_5min,
				"5min"
			},
			{
				Const_STOCHF.STOCHF_interval.n_15min,
				"15min"
			},
			{
				Const_STOCHF.STOCHF_interval.n_30min,
				"30min"
			},
			{
				Const_STOCHF.STOCHF_interval.n_60min,
				"60min"
			},
			{
				Const_STOCHF.STOCHF_interval.daily,
				"daily"
			},
			{
				Const_STOCHF.STOCHF_interval.weekly,
				"weekly"
			},
			{
				Const_STOCHF.STOCHF_interval.monthly,
				"monthly"
			}
		};

		internal static readonly IDictionary s_STOCHF_fastdmatype_translation
			 = new Dictionary<Const_STOCHF.STOCHF_fastdmatype, int>()
		{
			{
				Const_STOCHF.STOCHF_fastdmatype.none,
				-1
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_0,
				0
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_1,
				1
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_2,
				2
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_3,
				3
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_4,
				4
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_5,
				5
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_6,
				6
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_7,
				7
			},
			{
				Const_STOCHF.STOCHF_fastdmatype.n_8,
				8
			}
		};

		public IAvapiResponse_STOCHF Query(
			string symbol,
			Const_STOCHF.STOCHF_interval interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHF.STOCHF_fastdmatype fastdmatype = Const_STOCHF.STOCHF_fastdmatype.none)
		{
			string current_interval = s_STOCHF_interval_translation[interval] as string;
			int current_fastdmatype = (int)s_STOCHF_fastdmatype_translation[fastdmatype];

			return QueryPrimitive(symbol,current_interval,fastkperiod,fastdperiod,current_fastdmatype);
		}

		public async Task<IAvapiResponse_STOCHF> QueryAsync(
			string symbol,
			Const_STOCHF.STOCHF_interval interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHF.STOCHF_fastdmatype fastdmatype = Const_STOCHF.STOCHF_fastdmatype.none)
		{
			string current_interval = s_STOCHF_interval_translation[interval] as string;
			int current_fastdmatype = (int)s_STOCHF_fastdmatype_translation[fastdmatype];

			return await QueryPrimitiveAsync(symbol,current_interval,fastkperiod,fastdperiod,current_fastdmatype);
		}


		public IAvapiResponse_STOCHF QueryPrimitive(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdperiod",fastdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdmatype",fastdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_STOCHF ret = new AvapiResponse_STOCHF
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_STOCHF> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("symbol",symbol));
			getParameters.Add(new KeyValuePair<string, string>("interval",interval));
			getParameters.Add(new KeyValuePair<string, string>("fastkperiod",fastkperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdperiod",fastdperiod.ToString()));
			getParameters.Add(new KeyValuePair<string, string>("fastdmatype",fastdmatype.ToString()));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_STOCHF ret = new AvapiResponse_STOCHF
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_STOCHF_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_STOCHF_Content ret = new AvapiResponse_STOCHF_Content();
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
                ret.MetaData.FastKPeriod = (string)metaData["5.1: FastK Period"];
                ret.MetaData.FastDPeriod = (string)metaData["5.2: FastD Period"];
                ret.MetaData.FastDMAType = (string)metaData["5.3: FastD MA Type"];
                ret.MetaData.TimeZone = (string)metaData["6: Time Zone"];
                JEnumerable<JToken> results = jsonInputParsed["Technical Analysis: STOCHF"].Children();
                foreach (JToken result in results)
                {
                    TechnicalIndicator_Type_STOCHF technicalindicator = new TechnicalIndicator_Type_STOCHF
                    {
                        DateTime = ((JProperty)result).Name,
                        FastK = (string)result.First["FastK"],
                        FastD = (string)result.First["FastD"]
                    };
                    ret.TechnicalIndicator.Add(technicalindicator);
                }
            }
            return ret;
        }
	}
}