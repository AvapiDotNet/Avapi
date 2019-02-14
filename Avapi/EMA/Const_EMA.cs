namespace Avapi.AvapiEMA
{
	public static class Const_EMA
	{
		public enum EMA_interval
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
		public enum EMA_series_type
		{
			none,
			close,
			open,
			high,
			low
		}
	}
}
