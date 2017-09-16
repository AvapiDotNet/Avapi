## What DX does?
This API returns the directional movement index (DX) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#dx)  

***
## Including the DX namespace
The very first thing to do before diving into DX calls is to include the right namespace.  

```

using Avapi.AvapiDX

```

## How to get a DX object?
The DX object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the DX from it.
```

...
Int_DX dx = 
	connection.GetQueryObject_DX();

```

## Perform a DX Request
To perform a DX request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_DX Query(string symbol,
		DX_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_DX Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the DX request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each DX value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **DX_interval**

**DX_interval**: The time interval between two consecutive data points in the time series.
```  

public enum DX_interval
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
## DX Response
The response of a DX request is an object that implements the **IAvapiResponse_DX** interface.
```

public interface IAvapiResponse_DX
{
    string RowData
    {
        get;
    }
    IAvapiResponse_DX_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_DX** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_DX_Content**.
  

***
## Complete Example: Display the result of a DX request
```

using System;
using System.IO;
using Avapi.AvapiDX;

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

            // Get the DX query object
            Int_DX dx =
                connection.GetQueryObject_DX();

            // Perform the DX request and get the result
            IAvapiResponse_DX dxResponse = 
            dx.Query(
                 "MSFT",
                 Const_DX.DX_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA DX ********");
            Console.WriteLine(dxResponse.RowData);

```
