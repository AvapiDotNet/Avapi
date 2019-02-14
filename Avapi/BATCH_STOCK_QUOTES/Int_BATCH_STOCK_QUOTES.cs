using System.Collections.Generic;
using System.Threading.Tasks;
namespace Avapi.AvapiBATCH_STOCK_QUOTES
{
    public interface Int_BATCH_STOCK_QUOTES
    {

		IAvapiResponse_BATCH_STOCK_QUOTES QueryPrimitive(
			string symbols);

		Task<IAvapiResponse_BATCH_STOCK_QUOTES> QueryPrimitiveAsync(
			string symbols);

	}

    public interface IAvapiResponse_BATCH_STOCK_QUOTES
    {
        string LastHttpRequest
        {
            get;
        }

        string RawData
        {
            get;
        }

        IAvapiResponse_BATCH_STOCK_QUOTES_Content Data
        {
            get;
        }
    }

    public interface IAvapiResponse_BATCH_STOCK_QUOTES_Content
    {
        bool Error
        {
            get;
        }

        string ErrorMessage
        {
            get;
        }

        MetaData_Type_BATCH_STOCK_QUOTES MetaData
        {
            get;
        }

        IList <StockQuotes_Type_BATCH_STOCK_QUOTES> StockQuotes
        {
            get;
        }
    }
}
