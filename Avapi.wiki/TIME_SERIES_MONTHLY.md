## What TIME_SERIES_MONTHLY does?
This API returns monthly time series (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly volume) of the equity specified, covering up to 20 years of historical data. The latest data point is the month that contains the current trading day (updated realtime). The related REST API documentation is [here](https://www.alphavantage.co/documentation/#monthly)  

***
## Including the TIME_SERIES_MONTHLY namespace
The very first thing to do before diving into TIME_SERIES_MONTHLY calls is to include the right namespace.  

```

using Avapi.AvapiTIME_SERIES_MONTHLY

```

## How to get a TIME_SERIES_MONTHLY object?
The TIME_SERIES_MONTHLY object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TIME_SERIES_MONTHLY from it.
```

...
Int_TIME_SERIES_MONTHLY timeseriesmonthly = 
	connection.GetQueryObject_TIME_SERIES_MONTHLY();

```

## Perform a TIME_SERIES_MONTHLY Request
To perform a TIME_SERIES_MONTHLY request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TIME_SERIES_MONTHLY Query(string symbol,
		TIME_SERIES_MONTHLY_datatype datatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_TIME_SERIES_MONTHLY Query(string symbol,
		string datatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the TIME_SERIES_MONTHLY request.  
* **symbol**: The name of the equity
* **datatype [OPTIONAL]**: It is a optional value; json and csv are accepted with the following specifications: json returns the monthly time series in JSON format; csv returns the time series as a CSV (comma separated value) file.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TIME_SERIES_MONTHLY_datatype**

**TIME_SERIES_MONTHLY_datatype**: It is a optional value; json and csv are accepted with the following specifications: json returns the monthly time series in JSON format; csv returns the time series as a CSV (comma separated value) file.
```  

public enum TIME_SERIES_MONTHLY_datatype
{
	none,
	json,
	csv
}

```  
  

***
## TIME_SERIES_MONTHLY Response
The response of a TIME_SERIES_MONTHLY request is an object that implements the **IAvapiResponse_TIME_SERIES_MONTHLY** interface.
```

public interface IAvapiResponse_TIME_SERIES_MONTHLY
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TIME_SERIES_MONTHLY_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TIME_SERIES_MONTHLY** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: It represents the parsed response in an object implementing the interface **IAvapiResponse_TIME_SERIES_MONTHLY_Content**.
  

***
## Complete Example: Display the result of a TIME_SERIES_MONTHLY request
```

using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_MONTHLY;

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

            // Get the TIME_SERIES_MONTHLY query object
            Int_TIME_SERIES_MONTHLY time_series_monthly =
                connection.GetQueryObject_TIME_SERIES_MONTHLY();

            // Perform the TIME_SERIES_MONTHLY request and get the result
            IAvapiResponse_TIME_SERIES_MONTHLY time_series_monthlyResponse = 
            time_series_monthly.Query(
                 "MSFT",
                 Const_TIME_SERIES_MONTHLY.TIME_SERIES_MONTHLY_datatype.json);

            // Printout the results
            Console.WriteLine("******** RAW DATA TIME_SERIES_MONTHLY ********");
            Console.WriteLine(time_series_monthlyResponse.RowData);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_MONTHLY ********");
            var data = time_series_monthlyResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Information: " + data.MetaData.Information);
                Console.WriteLine("Symbol: " + data.MetaData.Symbol);
                Console.WriteLine("LastRefreshed: " + data.MetaData.LastRefreshed);
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
