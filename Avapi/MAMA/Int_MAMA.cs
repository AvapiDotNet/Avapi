using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMAMA
{
    public interface Int_MAMA
    {
		IAvapiResponse_MAMA Query(
			string symbol,
			Const_MAMA.MAMA_interval interval,
			Const_MAMA.MAMA_series_type series_type,
			float fastlimit = -1,
			float slowlimit = -1);

		Task<IAvapiResponse_MAMA> QueryAsync(
			string symbol,
			Const_MAMA.MAMA_interval interval,
			Const_MAMA.MAMA_series_type series_type,
			float fastlimit = -1,
			float slowlimit = -1);


		IAvapiResponse_MAMA QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			float fastlimit = -1,
			float slowlimit = -1);

		Task<IAvapiResponse_MAMA> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			float fastlimit = -1,
			float slowlimit = -1);

	}

    public interface IAvapiResponse_MAMA
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MAMA_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MAMA_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MAMA MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MAMA> TechnicalIndicator
        {
            get;
        }
    }
}
