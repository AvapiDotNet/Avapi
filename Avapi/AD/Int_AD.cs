using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiAD
{
    public interface Int_AD
    {
		IAvapiResponse_AD Query(
			string symbol,
			Const_AD.AD_interval interval);

		Task<IAvapiResponse_AD> QueryAsync(
			string symbol,
			Const_AD.AD_interval interval);


		IAvapiResponse_AD QueryPrimitive(
			string symbol,
			string interval);

		Task<IAvapiResponse_AD> QueryPrimitiveAsync(
			string symbol,
			string interval);

	}

    public interface IAvapiResponse_AD
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_AD_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_AD_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_AD MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_AD> TechnicalIndicator
        {
            get;
        }
    }
}
