using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMINUS_DI
{
    public interface Int_MINUS_DI
    {
		IAvapiResponse_MINUS_DI Query(
			string symbol,
			Const_MINUS_DI.MINUS_DI_interval interval,
			int time_period);

		Task<IAvapiResponse_MINUS_DI> QueryAsync(
			string symbol,
			Const_MINUS_DI.MINUS_DI_interval interval,
			int time_period);


		IAvapiResponse_MINUS_DI QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_MINUS_DI> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_MINUS_DI
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MINUS_DI_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MINUS_DI_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MINUS_DI MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MINUS_DI> TechnicalIndicator
        {
            get;
        }
    }
}
