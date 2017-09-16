This library acts as a .NET CORE Wrapper of Alpha Vantage REST API. You can find the official documentation of the REST API [here](https://www.alphavantage.co/documentation/).

Alpha Vantage APIs are grouped into three categories:
1. Time Series Data
2. Technical Indicators
3. Sector Performances   

### Time Series Data
Time Series Data provides realtime and historical equity data in four different temporal resolutions:
1. Intraday
2. Daily
3. Weekly
4. Monthly  

Daily, weekly, and monthly time series contain up to 20 years of historical data. Intraday time series typically span the past 10 to 15 active trading days.  

### Technical indicator
Technical indicator values are updated realtime: the latest data point is derived from the current trading day of a given equity.  

### Sector
This API returns the realtime and historical sector performances calculated from S&P500 incumbents.  
