using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiSTOCHRSI
{
    public interface Int_STOCHRSI
    {
		IAvapiResponse_STOCHRSI Query(
			string symbol,
			Const_STOCHRSI.STOCHRSI_interval interval,
			int time_period,
			Const_STOCHRSI.STOCHRSI_series_type series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHRSI.STOCHRSI_fastdmatype fastdmatype = Const_STOCHRSI.STOCHRSI_fastdmatype.none);

		Task<IAvapiResponse_STOCHRSI> QueryAsync(
			string symbol,
			Const_STOCHRSI.STOCHRSI_interval interval,
			int time_period,
			Const_STOCHRSI.STOCHRSI_series_type series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHRSI.STOCHRSI_fastdmatype fastdmatype = Const_STOCHRSI.STOCHRSI_fastdmatype.none);


		IAvapiResponse_STOCHRSI QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1);

		Task<IAvapiResponse_STOCHRSI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1);

	}

    public interface IAvapiResponse_STOCHRSI
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_STOCHRSI_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_STOCHRSI_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_STOCHRSI MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_STOCHRSI> TechnicalIndicator
        {
            get;
        }
    }
}
