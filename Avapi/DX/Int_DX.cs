using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiDX
{
    public interface Int_DX
    {
		IAvapiResponse_DX Query(
			string symbol,
			Const_DX.DX_interval interval,
			int time_period);

		Task<IAvapiResponse_DX> QueryAsync(
			string symbol,
			Const_DX.DX_interval interval,
			int time_period);


		IAvapiResponse_DX QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_DX> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_DX
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_DX_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_DX_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_DX MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_DX> TechnicalIndicator
        {
            get;
        }
    }
}
