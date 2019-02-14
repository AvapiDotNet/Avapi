using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiADX
{
    public interface Int_ADX
    {
		IAvapiResponse_ADX Query(
			string symbol,
			Const_ADX.ADX_interval interval,
			int time_period);

		Task<IAvapiResponse_ADX> QueryAsync(
			string symbol,
			Const_ADX.ADX_interval interval,
			int time_period);


		IAvapiResponse_ADX QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_ADX> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_ADX
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_ADX_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_ADX_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_ADX MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_ADX> TechnicalIndicator
        {
            get;
        }
    }
}
