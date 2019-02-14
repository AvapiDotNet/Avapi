using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiHT_PHASOR
{
    public interface Int_HT_PHASOR
    {
		IAvapiResponse_HT_PHASOR Query(
			string symbol,
			Const_HT_PHASOR.HT_PHASOR_interval interval,
			Const_HT_PHASOR.HT_PHASOR_series_type series_type);

		Task<IAvapiResponse_HT_PHASOR> QueryAsync(
			string symbol,
			Const_HT_PHASOR.HT_PHASOR_interval interval,
			Const_HT_PHASOR.HT_PHASOR_series_type series_type);


		IAvapiResponse_HT_PHASOR QueryPrimitive(
			string symbol,
			string interval,
			string series_type);

		Task<IAvapiResponse_HT_PHASOR> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type);

	}

    public interface IAvapiResponse_HT_PHASOR
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_HT_PHASOR_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_HT_PHASOR_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_HT_PHASOR MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_HT_PHASOR> TechnicalIndicator
        {
            get;
        }
    }
}
