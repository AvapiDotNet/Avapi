using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiHT_DCPHASE
{
    public interface Int_HT_DCPHASE
    {
		IAvapiResponse_HT_DCPHASE Query(
			string symbol,
			Const_HT_DCPHASE.HT_DCPHASE_interval interval,
			Const_HT_DCPHASE.HT_DCPHASE_series_type series_type);

		Task<IAvapiResponse_HT_DCPHASE> QueryAsync(
			string symbol,
			Const_HT_DCPHASE.HT_DCPHASE_interval interval,
			Const_HT_DCPHASE.HT_DCPHASE_series_type series_type);


		IAvapiResponse_HT_DCPHASE QueryPrimitive(
			string symbol,
			string interval,
			string series_type);

		Task<IAvapiResponse_HT_DCPHASE> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type);

	}

    public interface IAvapiResponse_HT_DCPHASE
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_HT_DCPHASE_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_HT_DCPHASE_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_HT_DCPHASE MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_HT_DCPHASE> TechnicalIndicator
        {
            get;
        }
    }
}
