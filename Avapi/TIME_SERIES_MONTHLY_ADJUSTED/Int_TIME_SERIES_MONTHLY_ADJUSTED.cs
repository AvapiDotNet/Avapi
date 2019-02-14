using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTIME_SERIES_MONTHLY_ADJUSTED
{
    public interface Int_TIME_SERIES_MONTHLY_ADJUSTED
    {

		IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED QueryPrimitive(
			string symbol);

		Task<IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED> QueryPrimitiveAsync(
			string symbol);

	}

    public interface IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TIME_SERIES_MONTHLY_ADJUSTED MetaData
        {
            get;
        }

        IList <TimeSeries_Type_TIME_SERIES_MONTHLY_ADJUSTED> TimeSeries
        {
            get;
        }
    }
}
