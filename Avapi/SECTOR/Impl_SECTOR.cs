using System; 
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Avapi.AvapiSECTOR
{
    internal class AvapiResponse_SECTOR : IAvapiResponse_SECTOR
    {
        public string LastHttpRequest
        {
            get;
            internal set;

        }
        public string RawData
        {
            get;
            internal set;
        }

        public IAvapiResponse_SECTOR_Content Data
        {
            get;
            internal set;
        }
    }

    public class MetaData_Type_SECTOR
    {
        public string Information
        {
            internal set;
            get;
        }

        public string LastRefreshed
        {
            internal set;
            get;
        }

    }

    public class RankA_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankB_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankC_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankD_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankE_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankF_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankG_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string RealEstate
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankH_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankI_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }
    public class RankJ_Type_SECTOR
        {
        public string RankName
        {
            internal set;
            get;
        }

        public string Financials
        {
            internal set;
            get;
        }
        public string Utilities
        {
            internal set;
            get;
        }
        public string HealthCare
        {
            internal set;
            get;
        }
        public string Industrials
        {
            internal set;
            get;
        }
        public string Materials
        {
            internal set;
            get;
        }
        public string TelecommunicationServices
        {
            internal set;
            get;
        }
        public string ConsumerDiscretionary
        {
            internal set;
            get;
        }
        public string ConsumerStaples
        {
            internal set;
            get;
        }
        public string InformationTechnology
        {
            internal set;
            get;
        }
        public string Energy
        {
            internal set;
            get;
        }
        }

    internal class AvapiResponse_SECTOR_Content : IAvapiResponse_SECTOR_Content
    {
        internal AvapiResponse_SECTOR_Content()
        {
           MetaData = new MetaData_Type_SECTOR();
           RankA = new RankA_Type_SECTOR();
           RankB = new RankB_Type_SECTOR();
           RankC = new RankC_Type_SECTOR();
           RankD = new RankD_Type_SECTOR();
           RankE = new RankE_Type_SECTOR();
           RankF = new RankF_Type_SECTOR();
           RankG = new RankG_Type_SECTOR();
           RankH = new RankH_Type_SECTOR();
           RankI = new RankI_Type_SECTOR();
           RankJ = new RankJ_Type_SECTOR();
        }

       public MetaData_Type_SECTOR MetaData
        {
            internal set;
            get;
        }

        public RankA_Type_SECTOR RankA
        {
            internal set;
            get;
        }

        public RankB_Type_SECTOR RankB
        {
            internal set;
            get;
        }

        public RankC_Type_SECTOR RankC
        {
            internal set;
            get;
        }

        public RankD_Type_SECTOR RankD
        {
            internal set;
            get;
        }

        public RankE_Type_SECTOR RankE
        {
            internal set;
            get;
        }

        public RankF_Type_SECTOR RankF
        {
            internal set;
            get;
        }

        public RankG_Type_SECTOR RankG
        {
            internal set;
            get;
        }

        public RankH_Type_SECTOR RankH
        {
            internal set;
            get;
        }

        public RankI_Type_SECTOR RankI
        {
            internal set;
            get;
        }

        public RankJ_Type_SECTOR RankJ
        {
            internal set;
            get;
        }

        public bool Error
        {
            internal set;
            get;
        }

        public string ErrorMessage
        {
            internal set;
            get;
        }
    }

	public class Impl_SECTOR : Int_SECTOR
	{
		const string s_function = "SECTOR";

		internal static string ApiKey
		{
			get;
			set;
		}

		internal static HttpClient RestClient
		{
			get;
			set;
		}

		internal static string AvapiUrl
		{
			get;
			set;
		}

		private static readonly Lazy<Impl_SECTOR> s_Impl_SECTOR =
			new Lazy<Impl_SECTOR>(() => new Impl_SECTOR());
		public static Impl_SECTOR Instance
		{
			get
			{
				return s_Impl_SECTOR.Value;
			}
		}
		private Impl_SECTOR()
		{
		}

		public IAvapiResponse_SECTOR QueryPrimitive()
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			queryString += UrlUtility.AsQueryString(getParameters);

			// Sent the Request and get the raw data from the Response
			string response = RestClient?.
				GetAsync(queryString)?.
				Result?.
				Content?.
				ReadAsStringAsync()?.
				Result; 

			IAvapiResponse_SECTOR ret = new AvapiResponse_SECTOR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}
		public async Task<IAvapiResponse_SECTOR> QueryPrimitiveAsync()
		{
			// Build Base Uri
			string queryString = AvapiUrl + "/query";

			// Build query parameters
			IDictionary<string, string> getParameters = new Dictionary<string, string>();
			getParameters.Add(new KeyValuePair<string, string>("function", s_function));
			getParameters.Add(new KeyValuePair<string, string>("apikey", ApiKey));
			queryString += UrlUtility.AsQueryString(getParameters);

			string response;
			using (var result = await RestClient.GetAsync(queryString))
			{
				response = await result.Content.ReadAsStringAsync();
			}
			IAvapiResponse_SECTOR ret = new AvapiResponse_SECTOR
			{
				RawData = response,
				Data = ParseInternal(response),
				LastHttpRequest = queryString
			};

			return ret;
		}

        static internal IAvapiResponse_SECTOR_Content ParseInternal(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                return null;
            }
            if(jsonInput == "{}")
            {
                return null;
            }

            AvapiResponse_SECTOR_Content ret = new AvapiResponse_SECTOR_Content();
            JObject jsonInputParsed = JObject.Parse(jsonInput);
            string errorMessage = (string)jsonInputParsed["Error Message"];
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ret.Error = true;
                ret.ErrorMessage = errorMessage;
            }
            else
            {
                JToken metaData = jsonInputParsed["Meta Data"];
                ret.MetaData.Information = (string)metaData["Information"];
                ret.MetaData.LastRefreshed = (string)metaData["Last Refreshed"];
                JToken result;

                //RankA
                result  = jsonInputParsed["Rank A: Real-Time Performance"];
                ret.RankA.RankName = "Rank A: Real-Time Performance";
                ret.RankA.Financials = (string)result["Financials"];
                ret.RankA.Utilities = (string)result["Utilities"];
                ret.RankA.HealthCare = (string)result["Health Care"];
                ret.RankA.Industrials = (string)result["Industrials"];
                ret.RankA.RealEstate = (string)result["Real Estate"];
                ret.RankA.Materials = (string)result["Materials"];
                ret.RankA.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankA.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankA.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankA.InformationTechnology = (string)result["Information Technology"];
                ret.RankA.Energy = (string)result["Energy"];
                //RankB
                result  = jsonInputParsed["Rank B: 1 Day Performance"];
                ret.RankB.RankName = "Rank B: 1 Day Performance";
                ret.RankB.Financials = (string)result["Financials"];
                ret.RankB.Utilities = (string)result["Utilities"];
                ret.RankB.HealthCare = (string)result["Health Care"];
                ret.RankB.Industrials = (string)result["Industrials"];
                ret.RankB.RealEstate = (string)result["Real Estate"];
                ret.RankB.Materials = (string)result["Materials"];
                ret.RankB.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankB.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankB.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankB.InformationTechnology = (string)result["Information Technology"];
                ret.RankB.Energy = (string)result["Energy"];
                //RankC
                result  = jsonInputParsed["Rank C: 5 Day Performance"];
                ret.RankC.RankName = "Rank C: 5 Day Performance";
                ret.RankC.Financials = (string)result["Financials"];
                ret.RankC.Utilities = (string)result["Utilities"];
                ret.RankC.HealthCare = (string)result["Health Care"];
                ret.RankC.Industrials = (string)result["Industrials"];
                ret.RankC.RealEstate = (string)result["Real Estate"];
                ret.RankC.Materials = (string)result["Materials"];
                ret.RankC.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankC.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankC.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankC.InformationTechnology = (string)result["Information Technology"];
                ret.RankC.Energy = (string)result["Energy"];
                //RankD
                result  = jsonInputParsed["Rank D: 1 Month Performance"];
                ret.RankD.RankName = "Rank D: 1 Month Performance";
                ret.RankD.Financials = (string)result["Financials"];
                ret.RankD.Utilities = (string)result["Utilities"];
                ret.RankD.HealthCare = (string)result["Health Care"];
                ret.RankD.Industrials = (string)result["Industrials"];
                ret.RankD.RealEstate = (string)result["Real Estate"];
                ret.RankD.Materials = (string)result["Materials"];
                ret.RankD.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankD.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankD.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankD.InformationTechnology = (string)result["Information Technology"];
                ret.RankD.Energy = (string)result["Energy"];
                //RankE
                result  = jsonInputParsed["Rank E: 3 Month Performance"];
                ret.RankE.RankName = "Rank E: 3 Month Performance";
                ret.RankE.Financials = (string)result["Financials"];
                ret.RankE.Utilities = (string)result["Utilities"];
                ret.RankE.HealthCare = (string)result["Health Care"];
                ret.RankE.Industrials = (string)result["Industrials"];
                ret.RankE.RealEstate = (string)result["Real Estate"];
                ret.RankE.Materials = (string)result["Materials"];
                ret.RankE.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankE.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankE.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankE.InformationTechnology = (string)result["Information Technology"];
                ret.RankE.Energy = (string)result["Energy"];
                //RankF
                result  = jsonInputParsed["Rank F: Year-to-Date (YTD) Performance"];
                ret.RankF.RankName = "Rank F: Year-to-Date (YTD) Performance";
                ret.RankF.Financials = (string)result["Financials"];
                ret.RankF.Utilities = (string)result["Utilities"];
                ret.RankF.HealthCare = (string)result["Health Care"];
                ret.RankF.Industrials = (string)result["Industrials"];
                ret.RankF.RealEstate = (string)result["Real Estate"];
                ret.RankF.Materials = (string)result["Materials"];
                ret.RankF.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankF.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankF.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankF.InformationTechnology = (string)result["Information Technology"];
                ret.RankF.Energy = (string)result["Energy"];
                //RankG
                result  = jsonInputParsed["Rank G: 1 Year Performance"];
                ret.RankG.RankName = "Rank G: 1 Year Performance";
                ret.RankG.Financials = (string)result["Financials"];
                ret.RankG.Utilities = (string)result["Utilities"];
                ret.RankG.HealthCare = (string)result["Health Care"];
                ret.RankG.Industrials = (string)result["Industrials"];
                ret.RankG.RealEstate = (string)result["Real Estate"];
                ret.RankG.Materials = (string)result["Materials"];
                ret.RankG.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankG.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankG.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankG.InformationTechnology = (string)result["Information Technology"];
                ret.RankG.Energy = (string)result["Energy"];
                //RankH
                result  = jsonInputParsed["Rank H: 3 Year Performance"];
                ret.RankH.RankName = "Rank H: 3 Year Performance";
                ret.RankH.Financials = (string)result["Financials"];
                ret.RankH.Utilities = (string)result["Utilities"];
                ret.RankH.HealthCare = (string)result["Health Care"];
                ret.RankH.Industrials = (string)result["Industrials"];
                ret.RankH.Materials = (string)result["Materials"];
                ret.RankH.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankH.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankH.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankH.InformationTechnology = (string)result["Information Technology"];
                ret.RankH.Energy = (string)result["Energy"];
                //RankI
                result  = jsonInputParsed["Rank I: 5 Year Performance"];
                ret.RankI.RankName = "Rank I: 5 Year Performance";
                ret.RankI.Financials = (string)result["Financials"];
                ret.RankI.Utilities = (string)result["Utilities"];
                ret.RankI.HealthCare = (string)result["Health Care"];
                ret.RankI.Industrials = (string)result["Industrials"];
                ret.RankI.Materials = (string)result["Materials"];
                ret.RankI.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankI.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankI.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankI.InformationTechnology = (string)result["Information Technology"];
                ret.RankI.Energy = (string)result["Energy"];
                //RankJ
                result  = jsonInputParsed["Rank J: 10 Year Performance"];
                ret.RankJ.RankName = "Rank J: 10 Year Performance";
                ret.RankJ.Financials = (string)result["Financials"];
                ret.RankJ.Utilities = (string)result["Utilities"];
                ret.RankJ.HealthCare = (string)result["Health Care"];
                ret.RankJ.Industrials = (string)result["Industrials"];
                ret.RankJ.Materials = (string)result["Materials"];
                ret.RankJ.TelecommunicationServices = (string)result["Telecommunication Services"];
                ret.RankJ.ConsumerDiscretionary = (string)result["Consumer Discretionary"];
                ret.RankJ.ConsumerStaples = (string)result["Consumer Staples"];
                ret.RankJ.InformationTechnology = (string)result["Information Technology"];
                ret.RankJ.Energy = (string)result["Energy"];
            }
            return ret;
        }
	}
}