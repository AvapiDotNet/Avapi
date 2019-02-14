using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMIDPOINT
{
    public interface Int_MIDPOINT
    {
		IAvapiResponse_MIDPOINT Query(
			string symbol,
			Const_MIDPOINT.MIDPOINT_interval interval,
			int time_period,
			Const_MIDPOINT.MIDPOINT_series_type series_type);

		Task<IAvapiResponse_MIDPOINT> QueryAsync(
			string symbol,
			Const_MIDPOINT.MIDPOINT_interval interval,
			int time_period,
			Const_MIDPOINT.MIDPOINT_series_type series_type);


		IAvapiResponse_MIDPOINT QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_MIDPOINT> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_MIDPOINT
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MIDPOINT_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MIDPOINT_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MIDPOINT MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MIDPOINT> TechnicalIndicator
        {
            get;
        }
    }
}
