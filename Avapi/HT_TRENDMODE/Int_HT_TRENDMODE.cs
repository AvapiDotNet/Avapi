using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiHT_TRENDMODE
{
    public interface Int_HT_TRENDMODE
    {
		IAvapiResponse_HT_TRENDMODE Query(
			string symbol,
			Const_HT_TRENDMODE.HT_TRENDMODE_interval interval,
			Const_HT_TRENDMODE.HT_TRENDMODE_series_type series_type);

		Task<IAvapiResponse_HT_TRENDMODE> QueryAsync(
			string symbol,
			Const_HT_TRENDMODE.HT_TRENDMODE_interval interval,
			Const_HT_TRENDMODE.HT_TRENDMODE_series_type series_type);


		IAvapiResponse_HT_TRENDMODE QueryPrimitive(
			string symbol,
			string interval,
			string series_type);

		Task<IAvapiResponse_HT_TRENDMODE> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type);

	}

    public interface IAvapiResponse_HT_TRENDMODE
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_HT_TRENDMODE_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_HT_TRENDMODE_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_HT_TRENDMODE MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_HT_TRENDMODE> TechnicalIndicator
        {
            get;
        }
    }
}
