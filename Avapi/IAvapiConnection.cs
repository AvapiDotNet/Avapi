namespace Avapi
{
	public interface IAvapiConnection
	{
		void Connect(string apiKey);
		string AvapiUrl { get; set; }
		string AvapiUrlDefault { get; }
		string ApiKey { get; set; }
		AvapiTIME_SERIES_INTRADAY.Int_TIME_SERIES_INTRADAY GetQueryObject_TIME_SERIES_INTRADAY();
		AvapiTIME_SERIES_DAILY.Int_TIME_SERIES_DAILY GetQueryObject_TIME_SERIES_DAILY();
		AvapiTIME_SERIES_DAILY_ADJUSTED.Int_TIME_SERIES_DAILY_ADJUSTED GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();
		AvapiTIME_SERIES_WEEKLY.Int_TIME_SERIES_WEEKLY GetQueryObject_TIME_SERIES_WEEKLY();
		AvapiTIME_SERIES_WEEKLY_ADJUSTED.Int_TIME_SERIES_WEEKLY_ADJUSTED GetQueryObject_TIME_SERIES_WEEKLY_ADJUSTED();
		AvapiTIME_SERIES_MONTHLY.Int_TIME_SERIES_MONTHLY GetQueryObject_TIME_SERIES_MONTHLY();
		AvapiBATCH_STOCK_QUOTES.Int_BATCH_STOCK_QUOTES GetQueryObject_BATCH_STOCK_QUOTES();
		AvapiTIME_SERIES_MONTHLY_ADJUSTED.Int_TIME_SERIES_MONTHLY_ADJUSTED GetQueryObject_TIME_SERIES_MONTHLY_ADJUSTED();
		AvapiSMA.Int_SMA GetQueryObject_SMA();
		AvapiEMA.Int_EMA GetQueryObject_EMA();
		AvapiWMA.Int_WMA GetQueryObject_WMA();
		AvapiDEMA.Int_DEMA GetQueryObject_DEMA();
		AvapiTEMA.Int_TEMA GetQueryObject_TEMA();
		AvapiTRIMA.Int_TRIMA GetQueryObject_TRIMA();
		AvapiKAMA.Int_KAMA GetQueryObject_KAMA();
		AvapiMAMA.Int_MAMA GetQueryObject_MAMA();
		AvapiT3.Int_T3 GetQueryObject_T3();
		AvapiMACD.Int_MACD GetQueryObject_MACD();
		AvapiMACDEXT.Int_MACDEXT GetQueryObject_MACDEXT();
		AvapiSTOCH.Int_STOCH GetQueryObject_STOCH();
		AvapiSTOCHF.Int_STOCHF GetQueryObject_STOCHF();
		AvapiRSI.Int_RSI GetQueryObject_RSI();
		AvapiSTOCHRSI.Int_STOCHRSI GetQueryObject_STOCHRSI();
		AvapiWILLR.Int_WILLR GetQueryObject_WILLR();
		AvapiADX.Int_ADX GetQueryObject_ADX();
		AvapiADXR.Int_ADXR GetQueryObject_ADXR();
		AvapiAPO.Int_APO GetQueryObject_APO();
		AvapiPPO.Int_PPO GetQueryObject_PPO();
		AvapiMOM.Int_MOM GetQueryObject_MOM();
		AvapiBOP.Int_BOP GetQueryObject_BOP();
		AvapiCCI.Int_CCI GetQueryObject_CCI();
		AvapiCMO.Int_CMO GetQueryObject_CMO();
		AvapiROC.Int_ROC GetQueryObject_ROC();
		AvapiROCR.Int_ROCR GetQueryObject_ROCR();
		AvapiAROON.Int_AROON GetQueryObject_AROON();
		AvapiAROONOSC.Int_AROONOSC GetQueryObject_AROONOSC();
		AvapiMFI.Int_MFI GetQueryObject_MFI();
		AvapiTRIX.Int_TRIX GetQueryObject_TRIX();
		AvapiULTOSC.Int_ULTOSC GetQueryObject_ULTOSC();
		AvapiDX.Int_DX GetQueryObject_DX();
		AvapiMINUS_DI.Int_MINUS_DI GetQueryObject_MINUS_DI();
		AvapiPLUS_DI.Int_PLUS_DI GetQueryObject_PLUS_DI();
		AvapiMINUS_DM.Int_MINUS_DM GetQueryObject_MINUS_DM();
		AvapiPLUS_DM.Int_PLUS_DM GetQueryObject_PLUS_DM();
		AvapiBBANDS.Int_BBANDS GetQueryObject_BBANDS();
		AvapiMIDPOINT.Int_MIDPOINT GetQueryObject_MIDPOINT();
		AvapiMIDPRICE.Int_MIDPRICE GetQueryObject_MIDPRICE();
		AvapiSAR.Int_SAR GetQueryObject_SAR();
		AvapiTRANGE.Int_TRANGE GetQueryObject_TRANGE();
		AvapiATR.Int_ATR GetQueryObject_ATR();
		AvapiNATR.Int_NATR GetQueryObject_NATR();
		AvapiAD.Int_AD GetQueryObject_AD();
		AvapiADOSC.Int_ADOSC GetQueryObject_ADOSC();
		AvapiOBV.Int_OBV GetQueryObject_OBV();
		AvapiHT_TRENDLINE.Int_HT_TRENDLINE GetQueryObject_HT_TRENDLINE();
		AvapiHT_SINE.Int_HT_SINE GetQueryObject_HT_SINE();
		AvapiHT_TRENDMODE.Int_HT_TRENDMODE GetQueryObject_HT_TRENDMODE();
		AvapiHT_DCPERIOD.Int_HT_DCPERIOD GetQueryObject_HT_DCPERIOD();
		AvapiHT_DCPHASE.Int_HT_DCPHASE GetQueryObject_HT_DCPHASE();
		AvapiHT_PHASOR.Int_HT_PHASOR GetQueryObject_HT_PHASOR();
		AvapiSECTOR.Int_SECTOR GetQueryObject_SECTOR();
		AvapiCURRENCY_EXCHANGE_RATE.Int_CURRENCY_EXCHANGE_RATE GetQueryObject_CURRENCY_EXCHANGE_RATE();
		AvapiDIGITAL_CURRENCY_INTRADAY.Int_DIGITAL_CURRENCY_INTRADAY GetQueryObject_DIGITAL_CURRENCY_INTRADAY();
		AvapiDIGITAL_CURRENCY_DAILY.Int_DIGITAL_CURRENCY_DAILY GetQueryObject_DIGITAL_CURRENCY_DAILY();
		AvapiDIGITAL_CURRENCY_WEEKLY.Int_DIGITAL_CURRENCY_WEEKLY GetQueryObject_DIGITAL_CURRENCY_WEEKLY();
		AvapiDIGITAL_CURRENCY_MONTHLY.Int_DIGITAL_CURRENCY_MONTHLY GetQueryObject_DIGITAL_CURRENCY_MONTHLY();
	}
}
