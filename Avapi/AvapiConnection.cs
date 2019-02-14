using System; 
using System.Net.Http;
namespace Avapi
{
	public class AvapiConnection : IAvapiConnection
	{
		private const string m_avapiUrlDefault = "https://www.alphavantage.co";
		private string m_avapiUrl;
		private HttpClient m_restClient;
		private static readonly Lazy<AvapiConnection> s_avapiConnection =
			new Lazy<AvapiConnection>(() => new AvapiConnection());
		public static AvapiConnection Instance
		{
			get
			{
				return s_avapiConnection.Value;
			}
		}
		private AvapiConnection()
		{
		}
		public string AvapiUrl
		{
			get
			{
				if (!string.IsNullOrEmpty(m_avapiUrl))
				{
					return m_avapiUrl;
				}
				return m_avapiUrlDefault;
			}
			set
			{
				m_avapiUrl = value;
			}
		}
		public string AvapiUrlDefault
		{
			get
			{
				return m_avapiUrlDefault;
			}
		}
		public string ApiKey
		{
			get;
			set;
		}
		public void Connect(string apiKey)
		{
			m_restClient = new HttpClient();
			ApiKey = apiKey;
		}
		public AvapiTIME_SERIES_INTRADAY.Int_TIME_SERIES_INTRADAY GetQueryObject_TIME_SERIES_INTRADAY()
		{
			AvapiTIME_SERIES_INTRADAY.Impl_TIME_SERIES_INTRADAY.ApiKey = ApiKey;
			AvapiTIME_SERIES_INTRADAY.Impl_TIME_SERIES_INTRADAY.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_INTRADAY.Impl_TIME_SERIES_INTRADAY.RestClient = m_restClient;
			return AvapiTIME_SERIES_INTRADAY.Impl_TIME_SERIES_INTRADAY.Instance;
		}
		public AvapiTIME_SERIES_DAILY.Int_TIME_SERIES_DAILY GetQueryObject_TIME_SERIES_DAILY()
		{
			AvapiTIME_SERIES_DAILY.Impl_TIME_SERIES_DAILY.ApiKey = ApiKey;
			AvapiTIME_SERIES_DAILY.Impl_TIME_SERIES_DAILY.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_DAILY.Impl_TIME_SERIES_DAILY.RestClient = m_restClient;
			return AvapiTIME_SERIES_DAILY.Impl_TIME_SERIES_DAILY.Instance;
		}
		public AvapiTIME_SERIES_DAILY_ADJUSTED.Int_TIME_SERIES_DAILY_ADJUSTED GetQueryObject_TIME_SERIES_DAILY_ADJUSTED()
		{
			AvapiTIME_SERIES_DAILY_ADJUSTED.Impl_TIME_SERIES_DAILY_ADJUSTED.ApiKey = ApiKey;
			AvapiTIME_SERIES_DAILY_ADJUSTED.Impl_TIME_SERIES_DAILY_ADJUSTED.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_DAILY_ADJUSTED.Impl_TIME_SERIES_DAILY_ADJUSTED.RestClient = m_restClient;
			return AvapiTIME_SERIES_DAILY_ADJUSTED.Impl_TIME_SERIES_DAILY_ADJUSTED.Instance;
		}
		public AvapiTIME_SERIES_WEEKLY.Int_TIME_SERIES_WEEKLY GetQueryObject_TIME_SERIES_WEEKLY()
		{
			AvapiTIME_SERIES_WEEKLY.Impl_TIME_SERIES_WEEKLY.ApiKey = ApiKey;
			AvapiTIME_SERIES_WEEKLY.Impl_TIME_SERIES_WEEKLY.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_WEEKLY.Impl_TIME_SERIES_WEEKLY.RestClient = m_restClient;
			return AvapiTIME_SERIES_WEEKLY.Impl_TIME_SERIES_WEEKLY.Instance;
		}
		public AvapiTIME_SERIES_WEEKLY_ADJUSTED.Int_TIME_SERIES_WEEKLY_ADJUSTED GetQueryObject_TIME_SERIES_WEEKLY_ADJUSTED()
		{
			AvapiTIME_SERIES_WEEKLY_ADJUSTED.Impl_TIME_SERIES_WEEKLY_ADJUSTED.ApiKey = ApiKey;
			AvapiTIME_SERIES_WEEKLY_ADJUSTED.Impl_TIME_SERIES_WEEKLY_ADJUSTED.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_WEEKLY_ADJUSTED.Impl_TIME_SERIES_WEEKLY_ADJUSTED.RestClient = m_restClient;
			return AvapiTIME_SERIES_WEEKLY_ADJUSTED.Impl_TIME_SERIES_WEEKLY_ADJUSTED.Instance;
		}
		public AvapiTIME_SERIES_MONTHLY.Int_TIME_SERIES_MONTHLY GetQueryObject_TIME_SERIES_MONTHLY()
		{
			AvapiTIME_SERIES_MONTHLY.Impl_TIME_SERIES_MONTHLY.ApiKey = ApiKey;
			AvapiTIME_SERIES_MONTHLY.Impl_TIME_SERIES_MONTHLY.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_MONTHLY.Impl_TIME_SERIES_MONTHLY.RestClient = m_restClient;
			return AvapiTIME_SERIES_MONTHLY.Impl_TIME_SERIES_MONTHLY.Instance;
		}
		public AvapiBATCH_STOCK_QUOTES.Int_BATCH_STOCK_QUOTES GetQueryObject_BATCH_STOCK_QUOTES()
		{
			AvapiBATCH_STOCK_QUOTES.Impl_BATCH_STOCK_QUOTES.ApiKey = ApiKey;
			AvapiBATCH_STOCK_QUOTES.Impl_BATCH_STOCK_QUOTES.AvapiUrl = AvapiUrl;
			AvapiBATCH_STOCK_QUOTES.Impl_BATCH_STOCK_QUOTES.RestClient = m_restClient;
			return AvapiBATCH_STOCK_QUOTES.Impl_BATCH_STOCK_QUOTES.Instance;
		}
		public AvapiTIME_SERIES_MONTHLY_ADJUSTED.Int_TIME_SERIES_MONTHLY_ADJUSTED GetQueryObject_TIME_SERIES_MONTHLY_ADJUSTED()
		{
			AvapiTIME_SERIES_MONTHLY_ADJUSTED.Impl_TIME_SERIES_MONTHLY_ADJUSTED.ApiKey = ApiKey;
			AvapiTIME_SERIES_MONTHLY_ADJUSTED.Impl_TIME_SERIES_MONTHLY_ADJUSTED.AvapiUrl = AvapiUrl;
			AvapiTIME_SERIES_MONTHLY_ADJUSTED.Impl_TIME_SERIES_MONTHLY_ADJUSTED.RestClient = m_restClient;
			return AvapiTIME_SERIES_MONTHLY_ADJUSTED.Impl_TIME_SERIES_MONTHLY_ADJUSTED.Instance;
		}
		public AvapiSMA.Int_SMA GetQueryObject_SMA()
		{
			AvapiSMA.Impl_SMA.ApiKey = ApiKey;
			AvapiSMA.Impl_SMA.AvapiUrl = AvapiUrl;
			AvapiSMA.Impl_SMA.RestClient = m_restClient;
			return AvapiSMA.Impl_SMA.Instance;
		}
		public AvapiEMA.Int_EMA GetQueryObject_EMA()
		{
			AvapiEMA.Impl_EMA.ApiKey = ApiKey;
			AvapiEMA.Impl_EMA.AvapiUrl = AvapiUrl;
			AvapiEMA.Impl_EMA.RestClient = m_restClient;
			return AvapiEMA.Impl_EMA.Instance;
		}
		public AvapiWMA.Int_WMA GetQueryObject_WMA()
		{
			AvapiWMA.Impl_WMA.ApiKey = ApiKey;
			AvapiWMA.Impl_WMA.AvapiUrl = AvapiUrl;
			AvapiWMA.Impl_WMA.RestClient = m_restClient;
			return AvapiWMA.Impl_WMA.Instance;
		}
		public AvapiDEMA.Int_DEMA GetQueryObject_DEMA()
		{
			AvapiDEMA.Impl_DEMA.ApiKey = ApiKey;
			AvapiDEMA.Impl_DEMA.AvapiUrl = AvapiUrl;
			AvapiDEMA.Impl_DEMA.RestClient = m_restClient;
			return AvapiDEMA.Impl_DEMA.Instance;
		}
		public AvapiTEMA.Int_TEMA GetQueryObject_TEMA()
		{
			AvapiTEMA.Impl_TEMA.ApiKey = ApiKey;
			AvapiTEMA.Impl_TEMA.AvapiUrl = AvapiUrl;
			AvapiTEMA.Impl_TEMA.RestClient = m_restClient;
			return AvapiTEMA.Impl_TEMA.Instance;
		}
		public AvapiTRIMA.Int_TRIMA GetQueryObject_TRIMA()
		{
			AvapiTRIMA.Impl_TRIMA.ApiKey = ApiKey;
			AvapiTRIMA.Impl_TRIMA.AvapiUrl = AvapiUrl;
			AvapiTRIMA.Impl_TRIMA.RestClient = m_restClient;
			return AvapiTRIMA.Impl_TRIMA.Instance;
		}
		public AvapiKAMA.Int_KAMA GetQueryObject_KAMA()
		{
			AvapiKAMA.Impl_KAMA.ApiKey = ApiKey;
			AvapiKAMA.Impl_KAMA.AvapiUrl = AvapiUrl;
			AvapiKAMA.Impl_KAMA.RestClient = m_restClient;
			return AvapiKAMA.Impl_KAMA.Instance;
		}
		public AvapiMAMA.Int_MAMA GetQueryObject_MAMA()
		{
			AvapiMAMA.Impl_MAMA.ApiKey = ApiKey;
			AvapiMAMA.Impl_MAMA.AvapiUrl = AvapiUrl;
			AvapiMAMA.Impl_MAMA.RestClient = m_restClient;
			return AvapiMAMA.Impl_MAMA.Instance;
		}
		public AvapiT3.Int_T3 GetQueryObject_T3()
		{
			AvapiT3.Impl_T3.ApiKey = ApiKey;
			AvapiT3.Impl_T3.AvapiUrl = AvapiUrl;
			AvapiT3.Impl_T3.RestClient = m_restClient;
			return AvapiT3.Impl_T3.Instance;
		}
		public AvapiMACD.Int_MACD GetQueryObject_MACD()
		{
			AvapiMACD.Impl_MACD.ApiKey = ApiKey;
			AvapiMACD.Impl_MACD.AvapiUrl = AvapiUrl;
			AvapiMACD.Impl_MACD.RestClient = m_restClient;
			return AvapiMACD.Impl_MACD.Instance;
		}
		public AvapiMACDEXT.Int_MACDEXT GetQueryObject_MACDEXT()
		{
			AvapiMACDEXT.Impl_MACDEXT.ApiKey = ApiKey;
			AvapiMACDEXT.Impl_MACDEXT.AvapiUrl = AvapiUrl;
			AvapiMACDEXT.Impl_MACDEXT.RestClient = m_restClient;
			return AvapiMACDEXT.Impl_MACDEXT.Instance;
		}
		public AvapiSTOCH.Int_STOCH GetQueryObject_STOCH()
		{
			AvapiSTOCH.Impl_STOCH.ApiKey = ApiKey;
			AvapiSTOCH.Impl_STOCH.AvapiUrl = AvapiUrl;
			AvapiSTOCH.Impl_STOCH.RestClient = m_restClient;
			return AvapiSTOCH.Impl_STOCH.Instance;
		}
		public AvapiSTOCHF.Int_STOCHF GetQueryObject_STOCHF()
		{
			AvapiSTOCHF.Impl_STOCHF.ApiKey = ApiKey;
			AvapiSTOCHF.Impl_STOCHF.AvapiUrl = AvapiUrl;
			AvapiSTOCHF.Impl_STOCHF.RestClient = m_restClient;
			return AvapiSTOCHF.Impl_STOCHF.Instance;
		}
		public AvapiRSI.Int_RSI GetQueryObject_RSI()
		{
			AvapiRSI.Impl_RSI.ApiKey = ApiKey;
			AvapiRSI.Impl_RSI.AvapiUrl = AvapiUrl;
			AvapiRSI.Impl_RSI.RestClient = m_restClient;
			return AvapiRSI.Impl_RSI.Instance;
		}
		public AvapiSTOCHRSI.Int_STOCHRSI GetQueryObject_STOCHRSI()
		{
			AvapiSTOCHRSI.Impl_STOCHRSI.ApiKey = ApiKey;
			AvapiSTOCHRSI.Impl_STOCHRSI.AvapiUrl = AvapiUrl;
			AvapiSTOCHRSI.Impl_STOCHRSI.RestClient = m_restClient;
			return AvapiSTOCHRSI.Impl_STOCHRSI.Instance;
		}
		public AvapiWILLR.Int_WILLR GetQueryObject_WILLR()
		{
			AvapiWILLR.Impl_WILLR.ApiKey = ApiKey;
			AvapiWILLR.Impl_WILLR.AvapiUrl = AvapiUrl;
			AvapiWILLR.Impl_WILLR.RestClient = m_restClient;
			return AvapiWILLR.Impl_WILLR.Instance;
		}
		public AvapiADX.Int_ADX GetQueryObject_ADX()
		{
			AvapiADX.Impl_ADX.ApiKey = ApiKey;
			AvapiADX.Impl_ADX.AvapiUrl = AvapiUrl;
			AvapiADX.Impl_ADX.RestClient = m_restClient;
			return AvapiADX.Impl_ADX.Instance;
		}
		public AvapiADXR.Int_ADXR GetQueryObject_ADXR()
		{
			AvapiADXR.Impl_ADXR.ApiKey = ApiKey;
			AvapiADXR.Impl_ADXR.AvapiUrl = AvapiUrl;
			AvapiADXR.Impl_ADXR.RestClient = m_restClient;
			return AvapiADXR.Impl_ADXR.Instance;
		}
		public AvapiAPO.Int_APO GetQueryObject_APO()
		{
			AvapiAPO.Impl_APO.ApiKey = ApiKey;
			AvapiAPO.Impl_APO.AvapiUrl = AvapiUrl;
			AvapiAPO.Impl_APO.RestClient = m_restClient;
			return AvapiAPO.Impl_APO.Instance;
		}
		public AvapiPPO.Int_PPO GetQueryObject_PPO()
		{
			AvapiPPO.Impl_PPO.ApiKey = ApiKey;
			AvapiPPO.Impl_PPO.AvapiUrl = AvapiUrl;
			AvapiPPO.Impl_PPO.RestClient = m_restClient;
			return AvapiPPO.Impl_PPO.Instance;
		}
		public AvapiMOM.Int_MOM GetQueryObject_MOM()
		{
			AvapiMOM.Impl_MOM.ApiKey = ApiKey;
			AvapiMOM.Impl_MOM.AvapiUrl = AvapiUrl;
			AvapiMOM.Impl_MOM.RestClient = m_restClient;
			return AvapiMOM.Impl_MOM.Instance;
		}
		public AvapiBOP.Int_BOP GetQueryObject_BOP()
		{
			AvapiBOP.Impl_BOP.ApiKey = ApiKey;
			AvapiBOP.Impl_BOP.AvapiUrl = AvapiUrl;
			AvapiBOP.Impl_BOP.RestClient = m_restClient;
			return AvapiBOP.Impl_BOP.Instance;
		}
		public AvapiCCI.Int_CCI GetQueryObject_CCI()
		{
			AvapiCCI.Impl_CCI.ApiKey = ApiKey;
			AvapiCCI.Impl_CCI.AvapiUrl = AvapiUrl;
			AvapiCCI.Impl_CCI.RestClient = m_restClient;
			return AvapiCCI.Impl_CCI.Instance;
		}
		public AvapiCMO.Int_CMO GetQueryObject_CMO()
		{
			AvapiCMO.Impl_CMO.ApiKey = ApiKey;
			AvapiCMO.Impl_CMO.AvapiUrl = AvapiUrl;
			AvapiCMO.Impl_CMO.RestClient = m_restClient;
			return AvapiCMO.Impl_CMO.Instance;
		}
		public AvapiROC.Int_ROC GetQueryObject_ROC()
		{
			AvapiROC.Impl_ROC.ApiKey = ApiKey;
			AvapiROC.Impl_ROC.AvapiUrl = AvapiUrl;
			AvapiROC.Impl_ROC.RestClient = m_restClient;
			return AvapiROC.Impl_ROC.Instance;
		}
		public AvapiROCR.Int_ROCR GetQueryObject_ROCR()
		{
			AvapiROCR.Impl_ROCR.ApiKey = ApiKey;
			AvapiROCR.Impl_ROCR.AvapiUrl = AvapiUrl;
			AvapiROCR.Impl_ROCR.RestClient = m_restClient;
			return AvapiROCR.Impl_ROCR.Instance;
		}
		public AvapiAROON.Int_AROON GetQueryObject_AROON()
		{
			AvapiAROON.Impl_AROON.ApiKey = ApiKey;
			AvapiAROON.Impl_AROON.AvapiUrl = AvapiUrl;
			AvapiAROON.Impl_AROON.RestClient = m_restClient;
			return AvapiAROON.Impl_AROON.Instance;
		}
		public AvapiAROONOSC.Int_AROONOSC GetQueryObject_AROONOSC()
		{
			AvapiAROONOSC.Impl_AROONOSC.ApiKey = ApiKey;
			AvapiAROONOSC.Impl_AROONOSC.AvapiUrl = AvapiUrl;
			AvapiAROONOSC.Impl_AROONOSC.RestClient = m_restClient;
			return AvapiAROONOSC.Impl_AROONOSC.Instance;
		}
		public AvapiMFI.Int_MFI GetQueryObject_MFI()
		{
			AvapiMFI.Impl_MFI.ApiKey = ApiKey;
			AvapiMFI.Impl_MFI.AvapiUrl = AvapiUrl;
			AvapiMFI.Impl_MFI.RestClient = m_restClient;
			return AvapiMFI.Impl_MFI.Instance;
		}
		public AvapiTRIX.Int_TRIX GetQueryObject_TRIX()
		{
			AvapiTRIX.Impl_TRIX.ApiKey = ApiKey;
			AvapiTRIX.Impl_TRIX.AvapiUrl = AvapiUrl;
			AvapiTRIX.Impl_TRIX.RestClient = m_restClient;
			return AvapiTRIX.Impl_TRIX.Instance;
		}
		public AvapiULTOSC.Int_ULTOSC GetQueryObject_ULTOSC()
		{
			AvapiULTOSC.Impl_ULTOSC.ApiKey = ApiKey;
			AvapiULTOSC.Impl_ULTOSC.AvapiUrl = AvapiUrl;
			AvapiULTOSC.Impl_ULTOSC.RestClient = m_restClient;
			return AvapiULTOSC.Impl_ULTOSC.Instance;
		}
		public AvapiDX.Int_DX GetQueryObject_DX()
		{
			AvapiDX.Impl_DX.ApiKey = ApiKey;
			AvapiDX.Impl_DX.AvapiUrl = AvapiUrl;
			AvapiDX.Impl_DX.RestClient = m_restClient;
			return AvapiDX.Impl_DX.Instance;
		}
		public AvapiMINUS_DI.Int_MINUS_DI GetQueryObject_MINUS_DI()
		{
			AvapiMINUS_DI.Impl_MINUS_DI.ApiKey = ApiKey;
			AvapiMINUS_DI.Impl_MINUS_DI.AvapiUrl = AvapiUrl;
			AvapiMINUS_DI.Impl_MINUS_DI.RestClient = m_restClient;
			return AvapiMINUS_DI.Impl_MINUS_DI.Instance;
		}
		public AvapiPLUS_DI.Int_PLUS_DI GetQueryObject_PLUS_DI()
		{
			AvapiPLUS_DI.Impl_PLUS_DI.ApiKey = ApiKey;
			AvapiPLUS_DI.Impl_PLUS_DI.AvapiUrl = AvapiUrl;
			AvapiPLUS_DI.Impl_PLUS_DI.RestClient = m_restClient;
			return AvapiPLUS_DI.Impl_PLUS_DI.Instance;
		}
		public AvapiMINUS_DM.Int_MINUS_DM GetQueryObject_MINUS_DM()
		{
			AvapiMINUS_DM.Impl_MINUS_DM.ApiKey = ApiKey;
			AvapiMINUS_DM.Impl_MINUS_DM.AvapiUrl = AvapiUrl;
			AvapiMINUS_DM.Impl_MINUS_DM.RestClient = m_restClient;
			return AvapiMINUS_DM.Impl_MINUS_DM.Instance;
		}
		public AvapiPLUS_DM.Int_PLUS_DM GetQueryObject_PLUS_DM()
		{
			AvapiPLUS_DM.Impl_PLUS_DM.ApiKey = ApiKey;
			AvapiPLUS_DM.Impl_PLUS_DM.AvapiUrl = AvapiUrl;
			AvapiPLUS_DM.Impl_PLUS_DM.RestClient = m_restClient;
			return AvapiPLUS_DM.Impl_PLUS_DM.Instance;
		}
		public AvapiBBANDS.Int_BBANDS GetQueryObject_BBANDS()
		{
			AvapiBBANDS.Impl_BBANDS.ApiKey = ApiKey;
			AvapiBBANDS.Impl_BBANDS.AvapiUrl = AvapiUrl;
			AvapiBBANDS.Impl_BBANDS.RestClient = m_restClient;
			return AvapiBBANDS.Impl_BBANDS.Instance;
		}
		public AvapiMIDPOINT.Int_MIDPOINT GetQueryObject_MIDPOINT()
		{
			AvapiMIDPOINT.Impl_MIDPOINT.ApiKey = ApiKey;
			AvapiMIDPOINT.Impl_MIDPOINT.AvapiUrl = AvapiUrl;
			AvapiMIDPOINT.Impl_MIDPOINT.RestClient = m_restClient;
			return AvapiMIDPOINT.Impl_MIDPOINT.Instance;
		}
		public AvapiMIDPRICE.Int_MIDPRICE GetQueryObject_MIDPRICE()
		{
			AvapiMIDPRICE.Impl_MIDPRICE.ApiKey = ApiKey;
			AvapiMIDPRICE.Impl_MIDPRICE.AvapiUrl = AvapiUrl;
			AvapiMIDPRICE.Impl_MIDPRICE.RestClient = m_restClient;
			return AvapiMIDPRICE.Impl_MIDPRICE.Instance;
		}
		public AvapiSAR.Int_SAR GetQueryObject_SAR()
		{
			AvapiSAR.Impl_SAR.ApiKey = ApiKey;
			AvapiSAR.Impl_SAR.AvapiUrl = AvapiUrl;
			AvapiSAR.Impl_SAR.RestClient = m_restClient;
			return AvapiSAR.Impl_SAR.Instance;
		}
		public AvapiTRANGE.Int_TRANGE GetQueryObject_TRANGE()
		{
			AvapiTRANGE.Impl_TRANGE.ApiKey = ApiKey;
			AvapiTRANGE.Impl_TRANGE.AvapiUrl = AvapiUrl;
			AvapiTRANGE.Impl_TRANGE.RestClient = m_restClient;
			return AvapiTRANGE.Impl_TRANGE.Instance;
		}
		public AvapiATR.Int_ATR GetQueryObject_ATR()
		{
			AvapiATR.Impl_ATR.ApiKey = ApiKey;
			AvapiATR.Impl_ATR.AvapiUrl = AvapiUrl;
			AvapiATR.Impl_ATR.RestClient = m_restClient;
			return AvapiATR.Impl_ATR.Instance;
		}
		public AvapiNATR.Int_NATR GetQueryObject_NATR()
		{
			AvapiNATR.Impl_NATR.ApiKey = ApiKey;
			AvapiNATR.Impl_NATR.AvapiUrl = AvapiUrl;
			AvapiNATR.Impl_NATR.RestClient = m_restClient;
			return AvapiNATR.Impl_NATR.Instance;
		}
		public AvapiAD.Int_AD GetQueryObject_AD()
		{
			AvapiAD.Impl_AD.ApiKey = ApiKey;
			AvapiAD.Impl_AD.AvapiUrl = AvapiUrl;
			AvapiAD.Impl_AD.RestClient = m_restClient;
			return AvapiAD.Impl_AD.Instance;
		}
		public AvapiADOSC.Int_ADOSC GetQueryObject_ADOSC()
		{
			AvapiADOSC.Impl_ADOSC.ApiKey = ApiKey;
			AvapiADOSC.Impl_ADOSC.AvapiUrl = AvapiUrl;
			AvapiADOSC.Impl_ADOSC.RestClient = m_restClient;
			return AvapiADOSC.Impl_ADOSC.Instance;
		}
		public AvapiOBV.Int_OBV GetQueryObject_OBV()
		{
			AvapiOBV.Impl_OBV.ApiKey = ApiKey;
			AvapiOBV.Impl_OBV.AvapiUrl = AvapiUrl;
			AvapiOBV.Impl_OBV.RestClient = m_restClient;
			return AvapiOBV.Impl_OBV.Instance;
		}
		public AvapiHT_TRENDLINE.Int_HT_TRENDLINE GetQueryObject_HT_TRENDLINE()
		{
			AvapiHT_TRENDLINE.Impl_HT_TRENDLINE.ApiKey = ApiKey;
			AvapiHT_TRENDLINE.Impl_HT_TRENDLINE.AvapiUrl = AvapiUrl;
			AvapiHT_TRENDLINE.Impl_HT_TRENDLINE.RestClient = m_restClient;
			return AvapiHT_TRENDLINE.Impl_HT_TRENDLINE.Instance;
		}
		public AvapiHT_SINE.Int_HT_SINE GetQueryObject_HT_SINE()
		{
			AvapiHT_SINE.Impl_HT_SINE.ApiKey = ApiKey;
			AvapiHT_SINE.Impl_HT_SINE.AvapiUrl = AvapiUrl;
			AvapiHT_SINE.Impl_HT_SINE.RestClient = m_restClient;
			return AvapiHT_SINE.Impl_HT_SINE.Instance;
		}
		public AvapiHT_TRENDMODE.Int_HT_TRENDMODE GetQueryObject_HT_TRENDMODE()
		{
			AvapiHT_TRENDMODE.Impl_HT_TRENDMODE.ApiKey = ApiKey;
			AvapiHT_TRENDMODE.Impl_HT_TRENDMODE.AvapiUrl = AvapiUrl;
			AvapiHT_TRENDMODE.Impl_HT_TRENDMODE.RestClient = m_restClient;
			return AvapiHT_TRENDMODE.Impl_HT_TRENDMODE.Instance;
		}
		public AvapiHT_DCPERIOD.Int_HT_DCPERIOD GetQueryObject_HT_DCPERIOD()
		{
			AvapiHT_DCPERIOD.Impl_HT_DCPERIOD.ApiKey = ApiKey;
			AvapiHT_DCPERIOD.Impl_HT_DCPERIOD.AvapiUrl = AvapiUrl;
			AvapiHT_DCPERIOD.Impl_HT_DCPERIOD.RestClient = m_restClient;
			return AvapiHT_DCPERIOD.Impl_HT_DCPERIOD.Instance;
		}
		public AvapiHT_DCPHASE.Int_HT_DCPHASE GetQueryObject_HT_DCPHASE()
		{
			AvapiHT_DCPHASE.Impl_HT_DCPHASE.ApiKey = ApiKey;
			AvapiHT_DCPHASE.Impl_HT_DCPHASE.AvapiUrl = AvapiUrl;
			AvapiHT_DCPHASE.Impl_HT_DCPHASE.RestClient = m_restClient;
			return AvapiHT_DCPHASE.Impl_HT_DCPHASE.Instance;
		}
		public AvapiHT_PHASOR.Int_HT_PHASOR GetQueryObject_HT_PHASOR()
		{
			AvapiHT_PHASOR.Impl_HT_PHASOR.ApiKey = ApiKey;
			AvapiHT_PHASOR.Impl_HT_PHASOR.AvapiUrl = AvapiUrl;
			AvapiHT_PHASOR.Impl_HT_PHASOR.RestClient = m_restClient;
			return AvapiHT_PHASOR.Impl_HT_PHASOR.Instance;
		}
		public AvapiSECTOR.Int_SECTOR GetQueryObject_SECTOR()
		{
			AvapiSECTOR.Impl_SECTOR.ApiKey = ApiKey;
			AvapiSECTOR.Impl_SECTOR.AvapiUrl = AvapiUrl;
			AvapiSECTOR.Impl_SECTOR.RestClient = m_restClient;
			return AvapiSECTOR.Impl_SECTOR.Instance;
		}
		public AvapiCURRENCY_EXCHANGE_RATE.Int_CURRENCY_EXCHANGE_RATE GetQueryObject_CURRENCY_EXCHANGE_RATE()
		{
			AvapiCURRENCY_EXCHANGE_RATE.Impl_CURRENCY_EXCHANGE_RATE.ApiKey = ApiKey;
			AvapiCURRENCY_EXCHANGE_RATE.Impl_CURRENCY_EXCHANGE_RATE.AvapiUrl = AvapiUrl;
			AvapiCURRENCY_EXCHANGE_RATE.Impl_CURRENCY_EXCHANGE_RATE.RestClient = m_restClient;
			return AvapiCURRENCY_EXCHANGE_RATE.Impl_CURRENCY_EXCHANGE_RATE.Instance;
		}
		public AvapiDIGITAL_CURRENCY_INTRADAY.Int_DIGITAL_CURRENCY_INTRADAY GetQueryObject_DIGITAL_CURRENCY_INTRADAY()
		{
			AvapiDIGITAL_CURRENCY_INTRADAY.Impl_DIGITAL_CURRENCY_INTRADAY.ApiKey = ApiKey;
			AvapiDIGITAL_CURRENCY_INTRADAY.Impl_DIGITAL_CURRENCY_INTRADAY.AvapiUrl = AvapiUrl;
			AvapiDIGITAL_CURRENCY_INTRADAY.Impl_DIGITAL_CURRENCY_INTRADAY.RestClient = m_restClient;
			return AvapiDIGITAL_CURRENCY_INTRADAY.Impl_DIGITAL_CURRENCY_INTRADAY.Instance;
		}
		public AvapiDIGITAL_CURRENCY_DAILY.Int_DIGITAL_CURRENCY_DAILY GetQueryObject_DIGITAL_CURRENCY_DAILY()
		{
			AvapiDIGITAL_CURRENCY_DAILY.Impl_DIGITAL_CURRENCY_DAILY.ApiKey = ApiKey;
			AvapiDIGITAL_CURRENCY_DAILY.Impl_DIGITAL_CURRENCY_DAILY.AvapiUrl = AvapiUrl;
			AvapiDIGITAL_CURRENCY_DAILY.Impl_DIGITAL_CURRENCY_DAILY.RestClient = m_restClient;
			return AvapiDIGITAL_CURRENCY_DAILY.Impl_DIGITAL_CURRENCY_DAILY.Instance;
		}
		public AvapiDIGITAL_CURRENCY_WEEKLY.Int_DIGITAL_CURRENCY_WEEKLY GetQueryObject_DIGITAL_CURRENCY_WEEKLY()
		{
			AvapiDIGITAL_CURRENCY_WEEKLY.Impl_DIGITAL_CURRENCY_WEEKLY.ApiKey = ApiKey;
			AvapiDIGITAL_CURRENCY_WEEKLY.Impl_DIGITAL_CURRENCY_WEEKLY.AvapiUrl = AvapiUrl;
			AvapiDIGITAL_CURRENCY_WEEKLY.Impl_DIGITAL_CURRENCY_WEEKLY.RestClient = m_restClient;
			return AvapiDIGITAL_CURRENCY_WEEKLY.Impl_DIGITAL_CURRENCY_WEEKLY.Instance;
		}
		public AvapiDIGITAL_CURRENCY_MONTHLY.Int_DIGITAL_CURRENCY_MONTHLY GetQueryObject_DIGITAL_CURRENCY_MONTHLY()
		{
			AvapiDIGITAL_CURRENCY_MONTHLY.Impl_DIGITAL_CURRENCY_MONTHLY.ApiKey = ApiKey;
			AvapiDIGITAL_CURRENCY_MONTHLY.Impl_DIGITAL_CURRENCY_MONTHLY.AvapiUrl = AvapiUrl;
			AvapiDIGITAL_CURRENCY_MONTHLY.Impl_DIGITAL_CURRENCY_MONTHLY.RestClient = m_restClient;
			return AvapiDIGITAL_CURRENCY_MONTHLY.Impl_DIGITAL_CURRENCY_MONTHLY.Instance;
		}
	}
}
