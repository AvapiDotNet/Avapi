using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiCMO
{
    public interface Int_CMO
    {
		IAvapiResponse_CMO Query(
			string symbol,
			Const_CMO.CMO_interval interval,
			int time_period,
			Const_CMO.CMO_series_type series_type);

		Task<IAvapiResponse_CMO> QueryAsync(
			string symbol,
			Const_CMO.CMO_interval interval,
			int time_period,
			Const_CMO.CMO_series_type series_type);


		IAvapiResponse_CMO QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_CMO> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_CMO
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_CMO_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_CMO_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_CMO MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_CMO> TechnicalIndicator
        {
            get;
        }
    }
}
