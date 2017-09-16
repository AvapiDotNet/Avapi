## What MIDPOINT does?
This API returns the midpoint (MIDPOINT) values. MIDPOINT = (highest value + lowest value)/2. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#midpoint)  

***
## Including the MIDPOINT namespace
The very first thing to do before diving into MIDPOINT calls is to include the right namespace.  

```

using Avapi.AvapiMIDPOINT

```

## How to get a MIDPOINT object?
The MIDPOINT object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MIDPOINT from it.
```

...
Int_MIDPOINT midpoint = 
	connection.GetQueryObject_MIDPOINT();

```

## Perform a MIDPOINT Request
To perform a MIDPOINT request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MIDPOINT Query(string symbol,
		MIDPOINT_interval interval,
		int time_period,
		MIDPOINT_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_MIDPOINT Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the MIDPOINT request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MIDPOINT value. 
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MIDPOINT_interval**
* **MIDPOINT_series_type**

**MIDPOINT_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MIDPOINT_interval
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
**MIDPOINT_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum MIDPOINT_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## MIDPOINT Response
The response of a MIDPOINT request is an object that implements the **IAvapiResponse_MIDPOINT** interface.
```

public interface IAvapiResponse_MIDPOINT
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MIDPOINT_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MIDPOINT** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MIDPOINT_Content**.
  

***
## Complete Example: Display the result of a MIDPOINT request
```

using System;
using System.IO;
using Avapi.AvapiMIDPOINT;

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

            // Get the MIDPOINT query object
            Int_MIDPOINT midpoint =
                connection.GetQueryObject_MIDPOINT();

            // Perform the MIDPOINT request and get the result
            IAvapiResponse_MIDPOINT midpointResponse = 
            midpoint.Query(
                 "MSFT",
                 Const_MIDPOINT.MIDPOINT_interval.n_1min,
                 10,
                 Const_MIDPOINT.MIDPOINT_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA MIDPOINT ********");
            Console.WriteLine(midpointResponse.RowData);

```
