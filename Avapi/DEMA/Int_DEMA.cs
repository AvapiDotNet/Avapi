using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDEMA
{
    public interface Int_DEMA
    {
		IAvapiResponse_DEMA Query(
			string symbol,
			Const_DEMA.DEMA_interval interval,
			int time_period,
			Const_DEMA.DEMA_series_type series_type);

		Task<IAvapiResponse_DEMA> QueryAsync(
			string symbol,
			Const_DEMA.DEMA_interval interval,
			int time_period,
			Const_DEMA.DEMA_series_type series_type);


		IAvapiResponse_DEMA QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_DEMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_DEMA
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DEMA_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DEMA_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DEMA MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_DEMA> TechnicalIndicator
        {
            get;
        }
    }
}
