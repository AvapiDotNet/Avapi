namespace Avapi.AvapiSTOCHRSI
{
	public static class Const_STOCHRSI
	{
		public enum STOCHRSI_interval
		{
			none,
			n_1min,
			n_5min,
			n_15min,
			n_30min,
			n_60min,
			daily,
			weekly,
			monthly
		}
		public enum STOCHRSI_series_type
		{
			none,
			close,
			open,
			high,
			low
		}
		public enum STOCHRSI_fastdmatype
		{none,
			n_0,
			n_1,
			n_2,
			n_3,
			n_4,
			n_5,
			n_6,
			n_7,
			n_8
		}
	}
}
