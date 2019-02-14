using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMINUS_DM
{
    public interface Int_MINUS_DM
    {
		IAvapiResponse_MINUS_DM Query(
			string symbol,
			Const_MINUS_DM.MINUS_DM_interval interval,
			int time_period);

		Task<IAvapiResponse_MINUS_DM> QueryAsync(
			string symbol,
			Const_MINUS_DM.MINUS_DM_interval interval,
			int time_period);


		IAvapiResponse_MINUS_DM QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_MINUS_DM> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_MINUS_DM
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MINUS_DM_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MINUS_DM_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MINUS_DM MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MINUS_DM> TechnicalIndicator
        {
            get;
        }
    }
}
