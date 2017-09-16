## What RSI does?
This API returns the relative strength index (RSI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#rsi)  

***
## Including the RSI namespace
The very first thing to do before diving into RSI calls is to include the right namespace.  

```

using Avapi.AvapiRSI

```

## How to get a RSI object?
The RSI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the RSI from it.
```

...
Int_RSI rsi = 
	connection.GetQueryObject_RSI();

```

## Perform a RSI Request
To perform a RSI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_RSI Query(string symbol,
		RSI_interval interval,
		int time_period,
		RSI_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_RSI Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the RSI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**:  Number of data points used to calculate each RSI value
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **RSI_interval**
* **RSI_series_type**

**RSI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum RSI_interval
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
**RSI_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum RSI_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## RSI Response
The response of a RSI request is an object that implements the **IAvapiResponse_RSI** interface.
```

public interface IAvapiResponse_RSI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_RSI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_RSI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_RSI_Content**.
  

***
## Complete Example: Display the result of a RSI request
```

using System;
using System.IO;
using Avapi.AvapiRSI;

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

            // Get the RSI query object
            Int_RSI rsi =
                connection.GetQueryObject_RSI();

            // Perform the RSI request and get the result
            IAvapiResponse_RSI rsiResponse = 
            rsi.Query(
                 "MSFT",
                 Const_RSI.RSI_interval.n_1min,
                 10,
                 Const_RSI.RSI_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA RSI ********");
            Console.WriteLine(rsiResponse.RowData);

```
