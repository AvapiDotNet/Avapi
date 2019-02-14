using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiSTOCHF
{
    public interface Int_STOCHF
    {
		IAvapiResponse_STOCHF Query(
			string symbol,
			Const_STOCHF.STOCHF_interval interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHF.STOCHF_fastdmatype fastdmatype = Const_STOCHF.STOCHF_fastdmatype.none);

		Task<IAvapiResponse_STOCHF> QueryAsync(
			string symbol,
			Const_STOCHF.STOCHF_interval interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			Const_STOCHF.STOCHF_fastdmatype fastdmatype = Const_STOCHF.STOCHF_fastdmatype.none);


		IAvapiResponse_STOCHF QueryPrimitive(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1);

		Task<IAvapiResponse_STOCHF> QueryPrimitiveAsync(
			string symbol,
			string interval,
			int fastkperiod = -1,
			int fastdperiod = -1,
			int fastdmatype = -1);

	}

    public interface IAvapiResponse_STOCHF
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_STOCHF_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_STOCHF_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_STOCHF MetaData
        {
            get;
        }

        IList <TechnicalIndicator_Type_STOCHF> TechnicalIndicator
        {
            get;
        }
    }
}
