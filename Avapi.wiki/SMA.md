## What SMA does?
This API returns the simple moving average (SMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#sma)  

***
## Including the SMA namespace
The very first thing to do before diving into SMA calls is to include the right namespace.  

```

using Avapi.AvapiSMA

```

## How to get a SMA object?
The SMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the SMA from it.
```

...
Int_SMA sma = 
	connection.GetQueryObject_SMA();

```

## Perform a SMA Request
To perform a SMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_SMA Query(string symbol,
		SMA_interval interval,
		int time_period,
		SMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_SMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the SMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **SMA_interval**
* **SMA_series_type**

**SMA_interval**: The time interval between two consecutive data points in the time series.
```  

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

```  
**SMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum SMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## SMA Response
The response of a SMA request is an object that implements the **IAvapiResponse_SMA** interface.
```

public interface IAvapiResponse_SMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_SMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_SMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_SMA_Content**.
  

***
## Complete Example: Display the result of a SMA request
```

using System;
using System.IO;
using Avapi.AvapiSMA;

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

            // Get the SMA query object
            Int_SMA sma =
                connection.GetQueryObject_SMA();

            // Perform the SMA request and get the result
            IAvapiResponse_SMA smaResponse = 
            sma.Query(
                 "MSFT",
                 Const_SMA.SMA_interval.n_1min,
                 10,
                 Const_SMA.SMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA SMA ********");
            Console.WriteLine(smaResponse.RowData);

```
