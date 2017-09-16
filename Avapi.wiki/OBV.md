## What OBV does?
This API returns the on balance volume (OBV) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#obv)  

***
## Including the OBV namespace
The very first thing to do before diving into OBV calls is to include the right namespace.  

```

using Avapi.AvapiOBV

```

## How to get a OBV object?
The OBV object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the OBV from it.
```

...
Int_OBV obv = 
	connection.GetQueryObject_OBV();

```

## Perform a OBV Request
To perform a OBV request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_OBV Query(string symbol,
		OBV_interval interval);

```  

2. **The request without constants**:

```

IAvapiResponse_OBV Query(string symbol,
		string interval);

```  

### Parameters
The parameters below are needed to perform the OBV request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **OBV_interval**

**OBV_interval**: The time interval between two consecutive data points in the time series.
```  

public enum OBV_interval
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
## OBV Response
The response of a OBV request is an object that implements the **IAvapiResponse_OBV** interface.
```

public interface IAvapiResponse_OBV
{
    string RowData
    {
        get;
    }
    IAvapiResponse_OBV_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_OBV** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_OBV_Content**.
  

***
## Complete Example: Display the result of a OBV request
```

using System;
using System.IO;
using Avapi.AvapiOBV;

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

            // Get the OBV query object
            Int_OBV obv =
                connection.GetQueryObject_OBV();

            // Perform the OBV request and get the result
            IAvapiResponse_OBV obvResponse = 
            obv.Query(
                 "MSFT",
                 Const_OBV.OBV_interval.n_1min);

            // Printout the results
            Console.WriteLine("******** RAW DATA OBV ********");
            Console.WriteLine(obvResponse.RowData);

```
