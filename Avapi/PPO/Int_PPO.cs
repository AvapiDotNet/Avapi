using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiPPO
{
    public interface Int_PPO
    {
		IAvapiResponse_PPO Query(
			string symbol,
			Const_PPO.PPO_interval interval,
			Const_PPO.PPO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_PPO.PPO_matype matype = Const_PPO.PPO_matype.none);

		Task<IAvapiResponse_PPO> QueryAsync(
			string symbol,
			Const_PPO.PPO_interval interval,
			Const_PPO.PPO_series_type series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			Const_PPO.PPO_matype matype = Const_PPO.PPO_matype.none);


		IAvapiResponse_PPO QueryPrimitive(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1);

		Task<IAvapiResponse_PPO> QueryPrimitiveAsync(
			string symbol,
			string interval,
			string series_type,
			int fastperiod = -1,
			int slowperiod = -1,
			int matype = -1);

	}

    public interface IAvapiResponse_PPO
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_PPO_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_PPO_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_PPO MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_PPO> TechnicalIndicator
        {
            get;
        }
    }
}
