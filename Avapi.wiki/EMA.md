## What EMA does?
This API returns the exponential moving average (EMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#ema)  

***
## Including the EMA namespace
The very first thing to do before diving into EMA calls is to include the right namespace.  

```

using Avapi.AvapiEMA

```

## How to get a EMA object?
The EMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the EMA from it.
```

...
Int_EMA ema = 
	connection.GetQueryObject_EMA();

```

## Perform a EMA Request
To perform a EMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_EMA Query(string symbol,
		EMA_interval interval,
		int time_period,
		EMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_EMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the EMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **EMA_interval**
* **EMA_series_type**

**EMA_interval**: The time interval between two consecutive data points in the time series.
```  

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

```  
**EMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum EMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## EMA Response
The response of a EMA request is an object that implements the **IAvapiResponse_EMA** interface.
```

public interface IAvapiResponse_EMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_EMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_EMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_EMA_Content**.
  

***
## Complete Example: Display the result of a EMA request
```

using System;
using System.IO;
using Avapi.AvapiEMA;

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

            // Get the EMA query object
            Int_EMA ema =
                connection.GetQueryObject_EMA();

            // Perform the EMA request and get the result
            IAvapiResponse_EMA emaResponse = 
            ema.Query(
                 "MSFT",
                 Const_EMA.EMA_interval.n_1min,
                 10,
                 Const_EMA.EMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA EMA ********");
            Console.WriteLine(emaResponse.RowData);

```
