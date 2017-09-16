## What WILLR does?
This API returns the Williams' %R (WILLR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#willr)  

***
## Including the WILLR namespace
The very first thing to do before diving into WILLR calls is to include the right namespace.  

```

using Avapi.AvapiWILLR

```

## How to get a WILLR object?
The WILLR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the WILLR from it.
```

...
Int_WILLR willr = 
	connection.GetQueryObject_WILLR();

```

## Perform a WILLR Request
To perform a WILLR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_WILLR Query(string symbol,
		WILLR_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_WILLR Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the WILLR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each WILLR value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **WILLR_interval**

**WILLR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum WILLR_interval
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
## WILLR Response
The response of a WILLR request is an object that implements the **IAvapiResponse_WILLR** interface.
```

public interface IAvapiResponse_WILLR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_WILLR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_WILLR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_WILLR_Content**.
  

***
## Complete Example: Display the result of a WILLR request
```

using System;
using System.IO;
using Avapi.AvapiWILLR;

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

            // Get the WILLR query object
            Int_WILLR willr =
                connection.GetQueryObject_WILLR();

            // Perform the WILLR request and get the result
            IAvapiResponse_WILLR willrResponse = 
            willr.Query(
                 "MSFT",
                 Const_WILLR.WILLR_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA WILLR ********");
            Console.WriteLine(willrResponse.RowData);

```
