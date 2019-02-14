using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDIGITAL_CURRENCY_MONTHLY
{
    public interface Int_DIGITAL_CURRENCY_MONTHLY
    {

		IAvapiResponse_DIGITAL_CURRENCY_MONTHLY QueryPrimitive(
			string symbol,
			string market);

		Task<IAvapiResponse_DIGITAL_CURRENCY_MONTHLY> QueryPrimitiveAsync(
			string symbol,
			string market);

	}

    public interface IAvapiResponse_DIGITAL_CURRENCY_MONTHLY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DIGITAL_CURRENCY_MONTHLY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DIGITAL_CURRENCY_MONTHLY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DIGITAL_CURRENCY_MONTHLY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_DIGITAL_CURRENCY_MONTHLY> TimeSeries
        {
            get;
        }
    }
}
