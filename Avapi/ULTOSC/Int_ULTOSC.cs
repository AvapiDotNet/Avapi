using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiULTOSC
{
    public interface Int_ULTOSC
    {
		IAvapiResponse_ULTOSC Query(
			string symbol,
			Const_ULTOSC.ULTOSC_interval interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1);

		Task<IAvapiResponse_ULTOSC> QueryAsync(
			string symbol,
			Const_ULTOSC.ULTOSC_interval interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1);


		IAvapiResponse_ULTOSC QueryPrimitive(
			string symbol,
			string interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1);

		Task<IAvapiResponse_ULTOSC> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int timeperiod1 = -1,
			int timeperiod2 = -1,
			int timeperiod3 = -1);

	}

    public interface IAvapiResponse_ULTOSC
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_ULTOSC_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_ULTOSC_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_ULTOSC MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_ULTOSC> TechnicalIndicator
        {
            get;
        }
    }
}
