using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiMOM
{
    public interface Int_MOM
    {
		IAvapiResponse_MOM Query(
			string symbol,
			Const_MOM.MOM_interval interval,
			int time_period,
			Const_MOM.MOM_series_type series_type);

		Task<IAvapiResponse_MOM> QueryAsync(
			string symbol,
			Const_MOM.MOM_interval interval,
			int time_period,
			Const_MOM.MOM_series_type series_type);


		IAvapiResponse_MOM QueryPrimitive(
			string symbol,
			string interval,
			int time_period,
			string series_type);

		Task<IAvapiResponse_MOM> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int time_period,
			string series_type);

	}

    public interface IAvapiResponse_MOM
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_MOM_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_MOM_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_MOM MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_MOM> TechnicalIndicator
        {
            get;
        }
    }
}
