using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMACDEXT
{
    public interface Int_MACDEXT
    {
		IAvapiResponse_MACDEXT Query(
			string symbol,
			Const_MACDEXT.MACDEXT_interval interval,
			Const_MACDEXT.MACDEXT_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			Const_MACDEXT.MACDEXT_fastmatype fastmatype = Const_MACDEXT.MACDEXT_fastmatype.none,
			Const_MACDEXT.MACDEXT_slowmatype slowmatype = Const_MACDEXT.MACDEXT_slowmatype.none,
			Const_MACDEXT.MACDEXT_signalmatype signalmatype = Const_MACDEXT.MACDEXT_signalmatype.none);

		Task<IAvapiResponse_MACDEXT> QueryAsync(
			string symbol,
			Const_MACDEXT.MACDEXT_interval interval,
			Const_MACDEXT.MACDEXT_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			Const_MACDEXT.MACDEXT_fastmatype fastmatype = Const_MACDEXT.MACDEXT_fastmatype.none,
			Const_MACDEXT.MACDEXT_slowmatype slowmatype = Const_MACDEXT.MACDEXT_slowmatype.none,
			Const_MACDEXT.MACDEXT_signalmatype signalmatype = Const_MACDEXT.MACDEXT_signalmatype.none);


		IAvapiResponse_MACDEXT QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			int fastmatype = -1,
			int slowmatype = -1,
			int signalmatype = -1);

		Task<IAvapiResponse_MACDEXT> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int signalperiod = -1,
			int fastmatype = -1,
			int slowmatype = -1,
			int signalmatype = -1);

	}

    public interface IAvapiResponse_MACDEXT
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MACDEXT_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MACDEXT_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MACDEXT MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MACDEXT> TechnicalIndicator
        {
            get;
        }
    }
}
