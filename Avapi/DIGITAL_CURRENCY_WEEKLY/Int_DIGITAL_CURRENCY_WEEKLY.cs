using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDIGITAL_CURRENCY_WEEKLY
{
    public interface Int_DIGITAL_CURRENCY_WEEKLY
    {

		IAvapiResponse_DIGITAL_CURRENCY_WEEKLY QueryPrimitive(
			string symbol,
			string market);

		Task<IAvapiResponse_DIGITAL_CURRENCY_WEEKLY> QueryPrimitiveAsync(
			string symbol,
			string market);

	}

    public interface IAvapiResponse_DIGITAL_CURRENCY_WEEKLY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DIGITAL_CURRENCY_WEEKLY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DIGITAL_CURRENCY_WEEKLY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DIGITAL_CURRENCY_WEEKLY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_DIGITAL_CURRENCY_WEEKLY> TimeSeries
        {
            get;
        }
    }
}
