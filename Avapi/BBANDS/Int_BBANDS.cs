using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiBBANDS
{
    public interface Int_BBANDS
    {
		IAvapiResponse_BBANDS Query(
			string symbol,
			Const_BBANDS.BBANDS_interval interval,
			int time_period,
			Const_BBANDS.BBANDS_series_type series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			Const_BBANDS.BBANDS_matype matype = Const_BBANDS.BBANDS_matype.none);

		Task<IAvapiResponse_BBANDS> QueryAsync(
			string symbol,
			Const_BBANDS.BBANDS_interval interval,
			int time_period,
			Const_BBANDS.BBANDS_series_type series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			Const_BBANDS.BBANDS_matype matype = Const_BBANDS.BBANDS_matype.none);


		IAvapiResponse_BBANDS QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			int matype = -1);

		Task<IAvapiResponse_BBANDS> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type,
			int nbdevup = -1,
			int nbdevdn = -1,
			int matype = -1);

	}

    public interface IAvapiResponse_BBANDS
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_BBANDS_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_BBANDS_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_BBANDS MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_BBANDS> TechnicalIndicator
        {
            get;
        }
    }
}
