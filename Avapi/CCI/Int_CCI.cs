using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiCCI
{
    public interface Int_CCI
    {
		IAvapiResponse_CCI Query(
			string symbol,
			Const_CCI.CCI_interval interval,
			int time_period);

		Task<IAvapiResponse_CCI> QueryAsync(
			string symbol,
			Const_CCI.CCI_interval interval,
			int time_period);


		IAvapiResponse_CCI QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_CCI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_CCI
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_CCI_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_CCI_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_CCI MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_CCI> TechnicalIndicator
        {
            get;
        }
    }
}
