using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiPLUS_DM
{
    public interface Int_PLUS_DM
    {
		IAvapiResponse_PLUS_DM Query(
			string symbol,
			Const_PLUS_DM.PLUS_DM_interval interval,
			int time_period);

		Task<IAvapiResponse_PLUS_DM> QueryAsync(
			string symbol,
			Const_PLUS_DM.PLUS_DM_interval interval,
			int time_period);


		IAvapiResponse_PLUS_DM QueryPrimitive(
			string symbol,
			string interval,
			int time_period);

		Task<IAvapiResponse_PLUS_DM> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period);

	}

    public interface IAvapiResponse_PLUS_DM
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_PLUS_DM_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_PLUS_DM_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_PLUS_DM MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_PLUS_DM> TechnicalIndicator
        {
            get;
        }
    }
}
