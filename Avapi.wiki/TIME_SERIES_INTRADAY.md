## What TIME_SERIES_INTRADAY does?
This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified.
The related REST API documentation is [here](https://www.alphavantage.co/documentation/#intraday)  

***
## Including the TIME_SERIES_INTRADAY namespace
The very first thing to do before diving into TIME_SERIES_INTRADAY calls is to include the right namespace.  

```

using Avapi.AvapiTIME_SERIES_INTRADAY

```

## How to get a TIME_SERIES_INTRADAY object?
The TIME_SERIES_INTRADAY object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TIME_SERIES_INTRADAY from it.
```

...
Int_TIME_SERIES_INTRADAY timeseriesintraday = 
	connection.GetQueryObject_TIME_SERIES_INTRADAY();

```

## Perform a TIME_SERIES_INTRADAY Request
To perform a TIME_SERIES_INTRADAY request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TIME_SERIES_INTRADAY Query(string symbol,
		TIME_SERIES_INTRADAY_interval interval,
		TIME_SERIES_INTRADAY_outputsize outputsize [OPTIONAL],
		TIME_SERIES_INTRADAY_datatype datatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_TIME_SERIES_INTRADAY Query(string symbol,
		string interval,
		string outputsize [OPTIONAL],
		string datatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the TIME_SERIES_INTRADAY request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **outputsize [OPTIONAL]**: It is a optional value; compact and full are accepted with the following specifications: compact returns only the latest 100 data points in the intraday time series; full returns the full-length intraday time series. The "compact" option is recommended if you would like to reduce the data size of each API call.
* **datatype [OPTIONAL]**: It is a optional value; json and csv are accepted with the following specifications: json returns the intraday time series in JSON format; csv returns the time series as a CSV (comma separated value) file.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TIME_SERIES_INTRADAY_interval**
* **TIME_SERIES_INTRADAY_outputsize**
* **TIME_SERIES_INTRADAY_datatype**

**TIME_SERIES_INTRADAY_interval**: The time interval between two consecutive data points in the time series.
```  

public enum TIME_SERIES_INTRADAY_interval
{
	none,
	n_1min,
	n_5min,
	n_15min,
	n_30min,
	n_60min
}

```  
**TIME_SERIES_INTRADAY_outputsize**: It is a optional value; compact and full are accepted with the following specifications: compact returns only the latest 100 data points in the intraday time series; full returns the full-length intraday time series. The "compact" option is recommended if you would like to reduce the data size of each API call.
```  

public enum TIME_SERIES_INTRADAY_outputsize
{
	none,
	compact,
	full
}

```  
**TIME_SERIES_INTRADAY_datatype**: It is a optional value; json and csv are accepted with the following specifications: json returns the intraday time series in JSON format; csv returns the time series as a CSV (comma separated value) file.
```  

public enum TIME_SERIES_INTRADAY_datatype
{
	none,
	json,
	csv
}

```  
  

***
## TIME_SERIES_INTRADAY Response
The response of a TIME_SERIES_INTRADAY request is an object that implements the **IAvapiResponse_TIME_SERIES_INTRADAY** interface.
```

public interface IAvapiResponse_TIME_SERIES_INTRADAY
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TIME_SERIES_INTRADAY_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TIME_SERIES_INTRADAY** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: It represents the parsed response in an object implementing the interface **IAvapiResponse_TIME_SERIES_INTRADAY_Content**.
  

***
## Complete Example: Display the result of a TIME_SERIES_INTRADAY request
```

using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_INTRADAY;

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

            // Get the TIME_SERIES_INTRADAY query object
            Int_TIME_SERIES_INTRADAY time_series_intraday =
                connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // Perform the TIME_SERIES_INTRADAY request and get the result
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse = 
            time_series_intraday.Query(
                 "MSFT",
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_1min,
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.compact,
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_datatype.json);

            // Printout the results
            Console.WriteLine("******** RAW DATA TIME_SERIES_INTRADAY ********");
            Console.WriteLine(time_series_intradayResponse.RowData);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_INTRADAY ********");
            var data = time_series_intradayResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Information: " + data.MetaData.Information);
                Console.WriteLine("Symbol: " + data.MetaData.Symbol);
                Console.WriteLine("LastRefreshed: " + data.MetaData.LastRefreshed);
                Console.WriteLine("Interval: " + data.MetaData.Interval);
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
                    Console.WriteLine("volume: " + timeseries.volume);
                    Console.WriteLine("DateTime: " + timeseries.DateTime);
                    Console.WriteLine("========================");
                }
            }
        }
    }
}

```
