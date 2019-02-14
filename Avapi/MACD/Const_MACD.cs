namespace Avapi.AvapiMACD
{
	public static class Const_MACD
	{
		public enum MACD_interval
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
		public enum MACD_series_type
		{
			none,
			close,
			open,
			high,
			low
		}
	}
}
