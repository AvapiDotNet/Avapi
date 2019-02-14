using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiCURRENCY_EXCHANGE_RATE
{
    internal class AvapiResponse_CURRENCY_EXCHANGE_RATE : IAvapiResponse_CURRENCY_EXCHANGE_RATE
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

        public IAvapiResponse_CURRENCY_EXCHANGE_RATE_Content Data
        {
            get;
            internal set;
        }
    }

    internal class AvapiResponse_CURRENCY_EXCHANGE_RATE_Content : IAvapiResponse_CURRENCY_EXCHANGE_RATE_Content
    {
        internal AvapiResponse_CURRENCY_EXCHANGE_RATE_Content()
        {
        }

        public string FromCurrencyCode
        {
            internal set;
            get;
        }

        public string FromCurrencyName
        {
            internal set;
            get;
        }

        public string ToCurrencyCode
        {
            internal set;
            get;
        }

        public string ToCurrencyName
        {
            internal set;
            get;
        }

        public string ExchangeRate
        {
            internal set;
            get;
        }

        public string LastRefreshed
        {
            internal set;
            get;
        }

        public string TimeZone
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

	public class Impl_CURRENCY_EXCHANGE_RATE : Int_CURRENCY_EXCHANGE_RATE
	{
		const string s_function = "CURRENCY_EXCHANGE_RATE";

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

		private static readonly Lazy<Impl_CURRENCY_EXCHANGE_RATE> s_Impl_CURRENCY_EXCHANGE_RATE =
			new Lazy<Impl_CURRENCY_EXCHANGE_RATE>(() => new Impl_CURRENCY_EXCHANGE_RATE());
		public static Impl_CURRENCY_EXCHANGE_RATE Instance
		{
			get
			{
				return s_Impl_CURRENCY_EXCHANGE_RATE.Value;
			}
		}
		private Impl_CURRENCY_EXCHANGE_RATE()
		{
		}


		public IAvapiResponse_CURRENCY_EXCHANGE_RATE QueryPrimitive(
			string from_currency,
			string to_currency)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("from_currency",from_currency));
			getParameters.Add(new KeyValuePair<string, string>("to_currency",to_currency));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_CURRENCY_EXCHANGE_RATE ret = new AvapiResponse_CURRENCY_EXCHANGE_RATE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

		public async Task<IAvapiResponse_CURRENCY_EXCHANGE_RATE> QueryPrimitiveAsync(
			string from_currency,
			string to_currency)
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			getParameters.Add(new KeyValuePair<string, string>("from_currency",from_currency));
			getParameters.Add(new KeyValuePair<string, string>("to_currency",to_currency));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_CURRENCY_EXCHANGE_RATE ret = new AvapiResponse_CURRENCY_EXCHANGE_RATE
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_CURRENCY_EXCHANGE_RATE_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_CURRENCY_EXCHANGE_RATE_Content ret = new AvapiResponse_CURRENCY_EXCHANGE_RATE_Content();
            JObject jsonInputParsed = JObject.Parse(jsonInput);
            string errorMessage = (string)jsonInputParsed["Error Message"];
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ret.Error = true;
                ret.ErrorMessage = errorMessage;
            }
            else
            {
                JToken currencyExchange = jsonInputParsed["Realtime Currency Exchange Rate"];
                ret.FromCurrencyCode = (string)currencyExchange["1. From_Currency Code"];
                ret.FromCurrencyName = (string)currencyExchange["2. From_Currency Name"];
                ret.ToCurrencyCode = (string)currencyExchange["3. To_Currency Code"];
                ret.ToCurrencyName = (string)currencyExchange["4. To_Currency Name"];
                ret.ExchangeRate = (string)currencyExchange["5. Exchange Rate"];
                ret.LastRefreshed = (string)currencyExchange["6. Last Refreshed"];
                ret.TimeZone = (string)currencyExchange["7. Time Zone"];
            }
            return ret;
        }
	}
}