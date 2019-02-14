using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTIME_SERIES_DAILY
{
    public interface Int_TIME_SERIES_DAILY
    {
		IAvapiResponse_TIME_SERIES_DAILY Query(
			string symbol,
			Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize outputsize = Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize.none);

		Task<IAvapiResponse_TIME_SERIES_DAILY> QueryAsync(
			string symbol,
			Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize outputsize = Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize.none);


		IAvapiResponse_TIME_SERIES_DAILY QueryPrimitive(
			string symbol,
			string outputsize = null);

		Task<IAvapiResponse_TIME_SERIES_DAILY> QueryPrimitiveAsync(
			string symbol,
			string outputsize = null);

	}

    public interface IAvapiResponse_TIME_SERIES_DAILY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TIME_SERIES_DAILY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TIME_SERIES_DAILY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TIME_SERIES_DAILY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_TIME_SERIES_DAILY> TimeSeries
        {
            get;
        }
    }
}
