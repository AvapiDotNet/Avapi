using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDIGITAL_CURRENCY_INTRADAY
{
    public interface Int_DIGITAL_CURRENCY_INTRADAY
    {

		IAvapiResponse_DIGITAL_CURRENCY_INTRADAY QueryPrimitive(
			string symbol,
			string market);

		Task<IAvapiResponse_DIGITAL_CURRENCY_INTRADAY> QueryPrimitiveAsync(
			string symbol,
			string market);

	}

    public interface IAvapiResponse_DIGITAL_CURRENCY_INTRADAY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DIGITAL_CURRENCY_INTRADAY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DIGITAL_CURRENCY_INTRADAY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_DIGITAL_CURRENCY_INTRADAY> TimeSeries
        {
            get;
        }
    }
}
