using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiPLUS_DI
{
    public interface Int_PLUS_DI
    {
		IAvapiResponse_PLUS_DI Query(
			string symbol,
			Const_PLUS_DI.PLUS_DI_interval interval,
			int time_period);

		Task<IAvapiResponse_PLUS_DI> QueryAsync(
			string symbol,
			Const_PLUS_DI.PLUS_DI_interval interval,
			int time_period);


		IAvapiResponse_PLUS_DI QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_PLUS_DI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_PLUS_DI
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_PLUS_DI_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_PLUS_DI_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_PLUS_DI MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_PLUS_DI> TechnicalIndicator
        {
            get;
        }
    }
}
