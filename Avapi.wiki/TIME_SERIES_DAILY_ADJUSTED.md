## What TIME_SERIES_DAILY_ADJUSTED does?
This API returns daily time series (date, daily open, daily high, daily low, daily close, daily split/dividend-adjusted close, daily volume, dividend amount, split coefficient) of the equity specified, covering up to 20 years of historical data. The most recent data point is the current trading day (updated realtime). The related REST API documentation is [here](https://www.alphavantage.co/documentation/#dailyadj)  

***
## Including the TIME_SERIES_DAILY_ADJUSTED namespace
The very first thing to do before diving into TIME_SERIES_DAILY_ADJUSTED calls is to include the right namespace.  

```

using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED

```

## How to get a TIME_SERIES_DAILY_ADJUSTED object?
The TIME_SERIES_DAILY_ADJUSTED object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TIME_SERIES_DAILY_ADJUSTED from it.
```

...
Int_TIME_SERIES_DAILY_ADJUSTED timeseriesdailyadjusted = 
	connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

```

## Perform a TIME_SERIES_DAILY_ADJUSTED Request
To perform a TIME_SERIES_DAILY_ADJUSTED request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED Query(string symbol,
		TIME_SERIES_DAILY_ADJUSTED_outputsize outputsize [OPTIONAL],
		TIME_SERIES_DAILY_ADJUSTED_datatype datatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED Query(string symbol,
		string outputsize [OPTIONAL],
		string datatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the TIME_SERIES_DAILY_ADJUSTED request.  
* **symbol**: The name of the equity
* **outputsize [OPTIONAL]**: It is a optional value; compact and full are accepted with the following specifications: ccompact returns only the latest 100 data points; full returns the full-length time series of up to 20 years of historical data. The "compact" option is recommended if you would like to reduce the data size of each API call.
* **datatype [OPTIONAL]**: It is a optional value; json and csv are accepted with the following specifications: json returns the daily time series in JSON format; csv returns the time series as a CSV (comma separated value) file.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TIME_SERIES_DAILY_ADJUSTED_outputsize**
* **TIME_SERIES_DAILY_ADJUSTED_datatype**

**TIME_SERIES_DAILY_ADJUSTED_outputsize**: It is a optional value; compact and full are accepted with the following specifications: ccompact returns only the latest 100 data points; full returns the full-length time series of up to 20 years of historical data. The "compact" option is recommended if you would like to reduce the data size of each API call.
```  

public enum TIME_SERIES_DAILY_ADJUSTED_outputsize
{
	none,
	compact,
	full
}

```  
**TIME_SERIES_DAILY_ADJUSTED_datatype**: It is a optional value; json and csv are accepted with the following specifications: json returns the daily time series in JSON format; csv returns the time series as a CSV (comma separated value) file.
```  

public enum TIME_SERIES_DAILY_ADJUSTED_datatype
{
	none,
	json,
	csv
}

```  
  

***
## TIME_SERIES_DAILY_ADJUSTED Response
The response of a TIME_SERIES_DAILY_ADJUSTED request is an object that implements the **IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED** interface.
```

public interface IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: It represents the parsed response in an object implementing the interface **IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED_Content**.
  

***
## Complete Example: Display the result of a TIME_SERIES_DAILY_ADJUSTED request
```

using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED;

namespace Avapi
{
    public class Example
    {
        static void Main()
        {
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("Your Alpha Vantage API Key !!!!");

            // Get the TIME_SERIES_DAILY_ADJUSTED query object
            Int_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjusted =
                connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

            // Perform the TIME_SERIES_DAILY_ADJUSTED request and get the result
            IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjustedResponse = 
            time_series_daily_adjusted.Query(
                 "MSFT",
                 Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.compact,
                 Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_datatype.json);

            // Printout the results
            Console.WriteLine("******** RAW DATA TIME_SERIES_DAILY_ADJUSTED ********");
            Console.WriteLine(time_series_daily_adjustedResponse.RowData);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_DAILY_ADJUSTED ********");
            var data = time_series_daily_adjustedResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Information: " + data.MetaData.Information);
                Console.WriteLine("Symbol: " + data.MetaData.Symbol);
                Console.WriteLine("LastRefreshed: " + data.MetaData.LastRefreshed);
                Console.WriteLine("OutputSize: " + data.MetaData.OutputSize);
                Console.WriteLine("TimeZone: " + data.MetaData.TimeZone);
                Console.WriteLine("========================");
                Console.WriteLine("========================");
                foreach (var timeseries in data.TimeSeries)
                {
                    Console.WriteLine("open: " + timeseries.open);
                    Console.WriteLine("high: " + timeseries.high);
                    Console.WriteLine("low: " + timeseries.low);
                    Console.WriteLine("close: " + timeseries.close);
                    Console.WriteLine("adjustedclose: " + timeseries.adjustedclose);
                    Console.WriteLine("volume: " + timeseries.volume);
                    Console.WriteLine("dividendamount: " + timeseries.dividendamount);
                    Console.WriteLine("splitcoefficient: " + timeseries.splitcoefficient);
                    Console.WriteLine("DateTime: " + timeseries.DateTime);
                    Console.WriteLine("========================");
                }
            }
        }
    }
}

```
