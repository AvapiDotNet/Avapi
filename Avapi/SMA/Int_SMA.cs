using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiSMA
{
    public interface Int_SMA
    {
		IAvapiResponse_SMA Query(
			string symbol,
			Const_SMA.SMA_interval interval,
			int time_period,
			Const_SMA.SMA_series_type series_type);

		Task<IAvapiResponse_SMA> QueryAsync(
			string symbol,
			Const_SMA.SMA_interval interval,
			int time_period,
			Const_SMA.SMA_series_type series_type);


		IAvapiResponse_SMA QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_SMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_SMA
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_SMA_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_SMA_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_SMA MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_SMA> TechnicalIndicator
        {
            get;
        }
    }
}
