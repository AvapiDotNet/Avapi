using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiCURRENCY_EXCHANGE_RATE
{
    public interface Int_CURRENCY_EXCHANGE_RATE
    {

		IAvapiResponse_CURRENCY_EXCHANGE_RATE QueryPrimitive(
			string from_currency,
			string to_currency);

		Task<IAvapiResponse_CURRENCY_EXCHANGE_RATE> QueryPrimitiveAsync(
			string from_currency,
			string to_currency);

	}

    public interface IAvapiResponse_CURRENCY_EXCHANGE_RATE
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_CURRENCY_EXCHANGE_RATE_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_CURRENCY_EXCHANGE_RATE_Content
    {
        string FromCurrencyCode
        {
            get;
        }

        string FromCurrencyName
        {
            get;
        }

        string ToCurrencyCode
        {
            get;
        }

        string ToCurrencyName
        {
            get;
        }

        string ExchangeRate
        {
            get;
        }

        string LastRefreshed
        {
            get;
        }

        string TimeZone
        {
            get;
        }

        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

    }
}
