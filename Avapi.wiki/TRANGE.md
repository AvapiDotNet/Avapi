## What TRANGE does?
This API returns the true range (TRANGE) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#trange)  

***
## Including the TRANGE namespace
The very first thing to do before diving into TRANGE calls is to include the right namespace.  

```

using Avapi.AvapiTRANGE

```

## How to get a TRANGE object?
The TRANGE object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TRANGE from it.
```

...
Int_TRANGE trange = 
	connection.GetQueryObject_TRANGE();

```

## Perform a TRANGE Request
To perform a TRANGE request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TRANGE Query(string symbol,
		TRANGE_interval interval);

```  

2. **The request without constants**:

```

IAvapiResponse_TRANGE Query(string symbol,
		string interval);

```  

### Parameters
The parameters below are needed to perform the TRANGE request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TRANGE_interval**

**TRANGE_interval**: The time interval between two consecutive data points in the time series.
```  

public enum TRANGE_interval
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
  

***
## TRANGE Response
The response of a TRANGE request is an object that implements the **IAvapiResponse_TRANGE** interface.
```

public interface IAvapiResponse_TRANGE
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TRANGE_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TRANGE** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_TRANGE_Content**.
  

***
## Complete Example: Display the result of a TRANGE request
```

using System;
using System.IO;
using Avapi.AvapiTRANGE;

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

            // Get the TRANGE query object
            Int_TRANGE trange =
                connection.GetQueryObject_TRANGE();

            // Perform the TRANGE request and get the result
            IAvapiResponse_TRANGE trangeResponse = 
            trange.Query(
                 "MSFT",
                 Const_TRANGE.TRANGE_interval.n_1min);

            // Printout the results
            Console.WriteLine("******** RAW DATA TRANGE ********");
            Console.WriteLine(trangeResponse.RowData);

```
