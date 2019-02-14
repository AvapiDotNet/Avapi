using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiNATR
{
    public interface Int_NATR
    {
		IAvapiResponse_NATR Query(
			string symbol,
			Const_NATR.NATR_interval interval,
			int time_period);

		Task<IAvapiResponse_NATR> QueryAsync(
			string symbol,
			Const_NATR.NATR_interval interval,
			int time_period);


		IAvapiResponse_NATR QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_NATR> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_NATR
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_NATR_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_NATR_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_NATR MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_NATR> TechnicalIndicator
        {
            get;
        }
    }
}
