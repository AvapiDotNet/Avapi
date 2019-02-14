using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiHT_SINE
{
    public interface Int_HT_SINE
    {
		IAvapiResponse_HT_SINE Query(
			string symbol,
			Const_HT_SINE.HT_SINE_interval interval,
			Const_HT_SINE.HT_SINE_series_type series_type);

		Task<IAvapiResponse_HT_SINE> QueryAsync(
			string symbol,
			Const_HT_SINE.HT_SINE_interval interval,
			Const_HT_SINE.HT_SINE_series_type series_type);


		IAvapiResponse_HT_SINE QueryPrimitive(
			string symbol,
			string interval,
			string series_type);

		Task<IAvapiResponse_HT_SINE> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type);

	}

    public interface IAvapiResponse_HT_SINE
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_HT_SINE_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_HT_SINE_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_HT_SINE MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_HT_SINE> TechnicalIndicator
        {
            get;
        }
    }
}
