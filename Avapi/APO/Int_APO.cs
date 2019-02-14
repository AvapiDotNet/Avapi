using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiAPO
{
    public interface Int_APO
    {
		IAvapiResponse_APO Query(
			string symbol,
			Const_APO.APO_interval interval,
			Const_APO.APO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_APO.APO_matype matype = Const_APO.APO_matype.none);

		Task<IAvapiResponse_APO> QueryAsync(
			string symbol,
			Const_APO.APO_interval interval,
			Const_APO.APO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_APO.APO_matype matype = Const_APO.APO_matype.none);


		IAvapiResponse_APO QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1);

		Task<IAvapiResponse_APO> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1);

	}

    public interface IAvapiResponse_APO
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_APO_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_APO_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_APO MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_APO> TechnicalIndicator
        {
            get;
        }
    }
}
