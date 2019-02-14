using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiKAMA
{
    public interface Int_KAMA
    {
		IAvapiResponse_KAMA Query(
			string symbol,
			Const_KAMA.KAMA_interval interval,
			int time_period,
			Const_KAMA.KAMA_series_type series_type);

		Task<IAvapiResponse_KAMA> QueryAsync(
			string symbol,
			Const_KAMA.KAMA_interval interval,
			int time_period,
			Const_KAMA.KAMA_series_type series_type);


		IAvapiResponse_KAMA QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_KAMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_KAMA
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_KAMA_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_KAMA_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_KAMA MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_KAMA> TechnicalIndicator
        {
            get;
        }
    }
}
