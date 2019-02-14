using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTIME_SERIES_MONTHLY
{
    public interface Int_TIME_SERIES_MONTHLY
    {

		IAvapiResponse_TIME_SERIES_MONTHLY QueryPrimitive(
			string symbol);

		Task<IAvapiResponse_TIME_SERIES_MONTHLY> QueryPrimitiveAsync(
			string symbol);

	}

    public interface IAvapiResponse_TIME_SERIES_MONTHLY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TIME_SERIES_MONTHLY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TIME_SERIES_MONTHLY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TIME_SERIES_MONTHLY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_TIME_SERIES_MONTHLY> TimeSeries
        {
            get;
        }
    }
}
