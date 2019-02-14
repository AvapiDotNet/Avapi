using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiAROON
{
    public interface Int_AROON
    {
		IAvapiResponse_AROON Query(
			string symbol,
			Const_AROON.AROON_interval interval,
			int time_period);

		Task<IAvapiResponse_AROON> QueryAsync(
			string symbol,
			Const_AROON.AROON_interval interval,
			int time_period);


		IAvapiResponse_AROON QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_AROON> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_AROON
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_AROON_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_AROON_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_AROON MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_AROON> TechnicalIndicator
        {
            get;
        }
    }
}
