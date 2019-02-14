using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiRSI
{
    public interface Int_RSI
    {
		IAvapiResponse_RSI Query(
			string symbol,
			Const_RSI.RSI_interval interval,
			int time_period,
			Const_RSI.RSI_series_type series_type);

		Task<IAvapiResponse_RSI> QueryAsync(
			string symbol,
			Const_RSI.RSI_interval interval,
			int time_period,
			Const_RSI.RSI_series_type series_type);


		IAvapiResponse_RSI QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_RSI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_RSI
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_RSI_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_RSI_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_RSI MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_RSI> TechnicalIndicator
        {
            get;
        }
    }
}
