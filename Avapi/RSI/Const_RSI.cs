namespace Avapi.AvapiRSI
{
	public static class Const_RSI
	{
		public enum RSI_interval
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
		public enum RSI_series_type
		{
			none,
			close,
			open,
			high,
			low
		}
	}
}
