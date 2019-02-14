using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiOBV
{
    public interface Int_OBV
    {
		IAvapiResponse_OBV Query(
			string symbol,
			Const_OBV.OBV_interval interval);

		Task<IAvapiResponse_OBV> QueryAsync(
			string symbol,
			Const_OBV.OBV_interval interval);


		IAvapiResponse_OBV QueryPrimitive(
			string symbol,
			string interval);

		Task<IAvapiResponse_OBV> QueryPrimitiveAsync(
			string symbol,
			string interval);

	}

    public interface IAvapiResponse_OBV
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_OBV_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_OBV_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_OBV MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_OBV> TechnicalIndicator
        {
            get;
        }
    }
}
