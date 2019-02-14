using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiTEMA
{
    public interface Int_TEMA
    {
		IAvapiResponse_TEMA Query(
			string symbol,
			Const_TEMA.TEMA_interval interval,
			int time_period,
			Const_TEMA.TEMA_series_type series_type);

		Task<IAvapiResponse_TEMA> QueryAsync(
			string symbol,
			Const_TEMA.TEMA_interval interval,
			int time_period,
			Const_TEMA.TEMA_series_type series_type);


		IAvapiResponse_TEMA QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_TEMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_TEMA
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_TEMA_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_TEMA_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_TEMA MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_TEMA> TechnicalIndicator
        {
            get;
        }
    }
}
