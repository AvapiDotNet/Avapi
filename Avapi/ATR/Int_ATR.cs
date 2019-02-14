using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiATR
{
    public interface Int_ATR
    {
		IAvapiResponse_ATR Query(
			string symbol,
			Const_ATR.ATR_interval interval,
			int time_period);

		Task<IAvapiResponse_ATR> QueryAsync(
			string symbol,
			Const_ATR.ATR_interval interval,
			int time_period);


		IAvapiResponse_ATR QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_ATR> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_ATR
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_ATR_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_ATR_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_ATR MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_ATR> TechnicalIndicator
        {
            get;
        }
    }
}
