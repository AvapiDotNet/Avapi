namespace Avapi.AvapiSMA
{
	public static class Const_SMA
	{
		public enum SMA_interval
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
		public enum SMA_series_type
		{
			none,
			close,
			open,
			high,
			low
		}
	}
}
