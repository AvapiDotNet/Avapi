## What WMA does?
This API returns the weighted moving average (WMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#wma)  

***
## Including the WMA namespace
The very first thing to do before diving into WMA calls is to include the right namespace.  

```

using Avapi.AvapiWMA

```

## How to get a WMA object?
The WMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the WMA from it.
```

...
Int_WMA wma = 
	connection.GetQueryObject_WMA();

```

## Perform a WMA Request
To perform a WMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_WMA Query(string symbol,
		WMA_interval interval,
		int time_period,
		WMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_WMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the WMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **WMA_interval**
* **WMA_series_type**

**WMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum WMA_interval
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

```  
**WMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum WMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## WMA Response
The response of a WMA request is an object that implements the **IAvapiResponse_WMA** interface.
```

public interface IAvapiResponse_WMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_WMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_WMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_WMA_Content**.
  

***
## Complete Example: Display the result of a WMA request
```

using System;
using System.IO;
using Avapi.AvapiWMA;

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

            // Get the WMA query object
            Int_WMA wma =
                connection.GetQueryObject_WMA();

            // Perform the WMA request and get the result
            IAvapiResponse_WMA wmaResponse = 
            wma.Query(
                 "MSFT",
                 Const_WMA.WMA_interval.n_1min,
                 10,
                 Const_WMA.WMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA WMA ********");
            Console.WriteLine(wmaResponse.RowData);

```
