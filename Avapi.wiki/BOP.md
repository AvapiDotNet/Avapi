## What BOP does?
This API returns the balance of power (BOP) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#bop)  

***
## Including the BOP namespace
The very first thing to do before diving into BOP calls is to include the right namespace.  

```

using Avapi.AvapiBOP

```

## How to get a BOP object?
The BOP object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the BOP from it.
```

...
Int_BOP bop = 
	connection.GetQueryObject_BOP();

```

## Perform a BOP Request
To perform a BOP request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_BOP Query(string symbol,
		BOP_interval interval);

```  

2. **The request without constants**:

```

IAvapiResponse_BOP Query(string symbol,
		string interval);

```  

### Parameters
The parameters below are needed to perform the BOP request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **BOP_interval**

**BOP_interval**: The time interval between two consecutive data points in the time series.
```  

public enum BOP_interval
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
## BOP Response
The response of a BOP request is an object that implements the **IAvapiResponse_BOP** interface.
```

public interface IAvapiResponse_BOP
{
    string RowData
    {
        get;
    }
    IAvapiResponse_BOP_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_BOP** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_BOP_Content**.
  

***
## Complete Example: Display the result of a BOP request
```

using System;
using System.IO;
using Avapi.AvapiBOP;

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

            // Get the BOP query object
            Int_BOP bop =
                connection.GetQueryObject_BOP();

            // Perform the BOP request and get the result
            IAvapiResponse_BOP bopResponse = 
            bop.Query(
                 "MSFT",
                 Const_BOP.BOP_interval.n_1min);

            // Printout the results
            Console.WriteLine("******** RAW DATA BOP ********");
            Console.WriteLine(bopResponse.RowData);

```
