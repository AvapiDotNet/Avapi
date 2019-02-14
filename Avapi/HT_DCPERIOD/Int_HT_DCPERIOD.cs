using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiHT_DCPERIOD
{
    public interface Int_HT_DCPERIOD
    {
		IAvapiResponse_HT_DCPERIOD Query(
			string symbol,
			Const_HT_DCPERIOD.HT_DCPERIOD_interval interval,
			Const_HT_DCPERIOD.HT_DCPERIOD_series_type series_type);

		Task<IAvapiResponse_HT_DCPERIOD> QueryAsync(
			string symbol,
			Const_HT_DCPERIOD.HT_DCPERIOD_interval interval,
			Const_HT_DCPERIOD.HT_DCPERIOD_series_type series_type);


		IAvapiResponse_HT_DCPERIOD QueryPrimitive(
			string symbol,
			string interval,
			string series_type);

		Task<IAvapiResponse_HT_DCPERIOD> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type);

	}

    public interface IAvapiResponse_HT_DCPERIOD
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_HT_DCPERIOD_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_HT_DCPERIOD_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_HT_DCPERIOD MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_HT_DCPERIOD> TechnicalIndicator
        {
            get;
        }
    }
}
