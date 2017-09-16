## What DEMA does?
This API returns the double exponential moving average (DEMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#dema)  

***
## Including the DEMA namespace
The very first thing to do before diving into DEMA calls is to include the right namespace.  

```

using Avapi.AvapiDEMA

```

## How to get a DEMA object?
The DEMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the DEMA from it.
```

...
Int_DEMA dema = 
	connection.GetQueryObject_DEMA();

```

## Perform a DEMA Request
To perform a DEMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_DEMA Query(string symbol,
		DEMA_interval interval,
		int time_period,
		DEMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_DEMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the DEMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **DEMA_interval**
* **DEMA_series_type**

**DEMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum DEMA_interval
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
**DEMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum DEMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## DEMA Response
The response of a DEMA request is an object that implements the **IAvapiResponse_DEMA** interface.
```

public interface IAvapiResponse_DEMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_DEMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_DEMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_DEMA_Content**.
  

***
## Complete Example: Display the result of a DEMA request
```

using System;
using System.IO;
using Avapi.AvapiDEMA;

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

            // Get the DEMA query object
            Int_DEMA dema =
                connection.GetQueryObject_DEMA();

            // Perform the DEMA request and get the result
            IAvapiResponse_DEMA demaResponse = 
            dema.Query(
                 "MSFT",
                 Const_DEMA.DEMA_interval.n_1min,
                 10,
                 Const_DEMA.DEMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA DEMA ********");
            Console.WriteLine(demaResponse.RowData);

```
