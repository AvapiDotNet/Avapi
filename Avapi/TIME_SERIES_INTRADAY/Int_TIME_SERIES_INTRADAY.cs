using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTIME_SERIES_INTRADAY
{
    public interface Int_TIME_SERIES_INTRADAY
    {
		IAvapiResponse_TIME_SERIES_INTRADAY Query(
			string symbol,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval interval,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize outputsize = Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.none);

		Task<IAvapiResponse_TIME_SERIES_INTRADAY> QueryAsync(
			string symbol,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval interval,
			Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize outputsize = Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.none);


		IAvapiResponse_TIME_SERIES_INTRADAY QueryPrimitive(
			string symbol,
			string interval,
			string outputsize = null);

		Task<IAvapiResponse_TIME_SERIES_INTRADAY> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string outputsize = null);

	}

    public interface IAvapiResponse_TIME_SERIES_INTRADAY
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TIME_SERIES_INTRADAY_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TIME_SERIES_INTRADAY_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TIME_SERIES_INTRADAY MetaData
        {
            get;
        }

        IList <TimeSeries_Type_TIME_SERIES_INTRADAY> TimeSeries
        {
            get;
        }
    }
}
