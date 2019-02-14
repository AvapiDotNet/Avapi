using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTRIX
{
    public interface Int_TRIX
    {
		IAvapiResponse_TRIX Query(
			string symbol,
			Const_TRIX.TRIX_interval interval,
			int time_period,
			Const_TRIX.TRIX_series_type series_type);

		Task<IAvapiResponse_TRIX> QueryAsync(
			string symbol,
			Const_TRIX.TRIX_interval interval,
			int time_period,
			Const_TRIX.TRIX_series_type series_type);


		IAvapiResponse_TRIX QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_TRIX> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_TRIX
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TRIX_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TRIX_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TRIX MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_TRIX> TechnicalIndicator
        {
            get;
        }
    }
}
