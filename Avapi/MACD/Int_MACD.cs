using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMACD
{
    public interface Int_MACD
    {
		IAvapiResponse_MACD Query(
			string symbol,
			Const_MACD.MACD_interval interval,
			Const_MACD.MACD_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1);

		Task<IAvapiResponse_MACD> QueryAsync(
			string symbol,
			Const_MACD.MACD_interval interval,
			Const_MACD.MACD_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1);


		IAvapiResponse_MACD QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1);

		Task<IAvapiResponse_MACD> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1);

	}

    public interface IAvapiResponse_MACD
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MACD_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MACD_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MACD MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MACD> TechnicalIndicator
        {
            get;
        }
    }
}
