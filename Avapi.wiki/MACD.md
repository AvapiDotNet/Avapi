## What MACD does?
This API returns the moving average convergence / divergence (MACD) values). The related REST API documentation is [here](https://www.alphavantage.co/documentation/#macd)  

***
## Including the MACD namespace
The very first thing to do before diving into MACD calls is to include the right namespace.  

```

using Avapi.AvapiMACD

```

## How to get a MACD object?
The MACD object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MACD from it.
```

...
Int_MACD macd = 
	connection.GetQueryObject_MACD();

```

## Perform a MACD Request
To perform a MACD request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MACD Query(string symbol,
		MACD_interval interval,
		MACD_series_type series_type,
		int fastperiod [OPTIONAL],
		int slowperiod [OPTIONAL],
		int signalperiod [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_MACD Query(string symbol,
		string interval,
		string series_type,
		string fastperiod [OPTIONAL],
		string slowperiod [OPTIONAL],
		string signalperiod [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the MACD request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **fastperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastperiod=12
* **slowperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowperiod=26
* **signalperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, signalperiod=9

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MACD_interval**
* **MACD_series_type**

**MACD_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MACD_interval
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
**MACD_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum MACD_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## MACD Response
The response of a MACD request is an object that implements the **IAvapiResponse_MACD** interface.
```

public interface IAvapiResponse_MACD
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MACD_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MACD** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MACD_Content**.
  

***
## Complete Example: Display the result of a MACD request
```

using System;
using System.IO;
using Avapi.AvapiMACD;

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

            // Get the MACD query object
            Int_MACD macd =
                connection.GetQueryObject_MACD();

            // Perform the MACD request and get the result
            IAvapiResponse_MACD macdResponse = 
            macd.Query(
                 "MSFT",
                 Const_MACD.MACD_interval.n_1min,
                 Const_MACD.MACD_series_type.close,
                 10,
                 10,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA MACD ********");
            Console.WriteLine(macdResponse.RowData);

```
