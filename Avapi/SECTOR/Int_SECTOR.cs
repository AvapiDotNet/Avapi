using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiSECTOR
{
    public interface Int_SECTOR
    {
		IAvapiResponse_SECTOR QueryPrimitive();
		Task<IAvapiResponse_SECTOR> QueryPrimitiveAsync();
	}

    public interface IAvapiResponse_SECTOR
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_SECTOR_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_SECTOR_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_SECTOR MetaData
        {
            get;
        }

        RankA_Type_SECTOR RankA
        {
            get;
        }
        RankB_Type_SECTOR RankB
        {
            get;
        }
        RankC_Type_SECTOR RankC
        {
            get;
        }
        RankD_Type_SECTOR RankD
        {
            get;
        }
        RankE_Type_SECTOR RankE
        {
            get;
        }
        RankF_Type_SECTOR RankF
        {
            get;
        }
        RankG_Type_SECTOR RankG
        {
            get;
        }
        RankH_Type_SECTOR RankH
        {
            get;
        }
        RankI_Type_SECTOR RankI
        {
            get;
        }
        RankJ_Type_SECTOR RankJ
        {
            get;
        }
    }
}
