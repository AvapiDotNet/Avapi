using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED
{
    public interface Int_TIME_SERIES_DAILY_ADJUSTED
    {
		IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED Query(
			string symbol,
			Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize outputsize = Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.none);

		Task<IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED> QueryAsync(
			string symbol,
			Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize outputsize = Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.none);


		IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED QueryPrimitive(
			string symbol,
			string outputsize = null);

		Task<IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED> QueryPrimitiveAsync(
			string symbol,
			string outputsize = null);

	}

    public interface IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TIME_SERIES_DAILY_ADJUSTED MetaData
        {
            get;
        }

        IList <TimeSeries_Type_TIME_SERIES_DAILY_ADJUSTED> TimeSeries
        {
            get;
        }
    }
}
