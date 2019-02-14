using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiBOP
{
    public interface Int_BOP
    {
		IAvapiResponse_BOP Query(
			string symbol,
			Const_BOP.BOP_interval interval);

		Task<IAvapiResponse_BOP> QueryAsync(
			string symbol,
			Const_BOP.BOP_interval interval);


		IAvapiResponse_BOP QueryPrimitive(
			string symbol,
			string interval);

		Task<IAvapiResponse_BOP> QueryPrimitiveAsync(
			string symbol,
			string interval);

	}

    public interface IAvapiResponse_BOP
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_BOP_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_BOP_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_BOP MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_BOP> TechnicalIndicator
        {
            get;
        }
    }
}
