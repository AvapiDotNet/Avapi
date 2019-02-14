using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDIGITAL_CURRENCY_DAILY
{
    public interface Int_DIGITAL_CURRENCY_DAILY
    {

		IAvapiResponse_DIGITAL_CURRENCY_DAILY QueryPrimitive(
			string symbol,
			string market);

		Task<IAvapiResponse_DIGITAL_CURRENCY_DAILY> QueryPrimitiveAsync(
			string symbol,
			string market);

	}

    public interface IAvapiResponse_DIGITAL_CURRENCY_DAILY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DIGITAL_CURRENCY_DAILY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DIGITAL_CURRENCY_DAILY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DIGITAL_CURRENCY_DAILY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_DIGITAL_CURRENCY_DAILY> TimeSeries
        {
            get;
        }
    }
}
