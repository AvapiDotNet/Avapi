## What TRIMA does?
This API returns the triangular moving average (TRIMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#trima)  

***
## Including the TRIMA namespace
The very first thing to do before diving into TRIMA calls is to include the right namespace.  

```

using Avapi.AvapiTRIMA

```

## How to get a TRIMA object?
The TRIMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TRIMA from it.
```

...
Int_TRIMA trima = 
	connection.GetQueryObject_TRIMA();

```

## Perform a TRIMA Request
To perform a TRIMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TRIMA Query(string symbol,
		TRIMA_interval interval,
		int time_period,
		TRIMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_TRIMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the TRIMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TRIMA_interval**
* **TRIMA_series_type**

**TRIMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum TRIMA_interval
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
**TRIMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum TRIMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## TRIMA Response
The response of a TRIMA request is an object that implements the **IAvapiResponse_TRIMA** interface.
```

public interface IAvapiResponse_TRIMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TRIMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TRIMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_TRIMA_Content**.
  

***
## Complete Example: Display the result of a TRIMA request
```

using System;
using System.IO;
using Avapi.AvapiTRIMA;

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

            // Get the TRIMA query object
            Int_TRIMA trima =
                connection.GetQueryObject_TRIMA();

            // Perform the TRIMA request and get the result
            IAvapiResponse_TRIMA trimaResponse = 
            trima.Query(
                 "MSFT",
                 Const_TRIMA.TRIMA_interval.n_1min,
                 10,
                 Const_TRIMA.TRIMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA TRIMA ********");
            Console.WriteLine(trimaResponse.RowData);

```
