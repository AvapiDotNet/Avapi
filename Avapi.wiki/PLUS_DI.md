## What PLUS_DI does?
This API returns the plus directional indicator (PLUS_DI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#plusdi)  

***
## Including the PLUS_DI namespace
The very first thing to do before diving into PLUS_DI calls is to include the right namespace.  

```

using Avapi.AvapiPLUS_DI

```

## How to get a PLUS_DI object?
The PLUS_DI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the PLUS_DI from it.
```

...
Int_PLUS_DI plusdi = 
	connection.GetQueryObject_PLUS_DI();

```

## Perform a PLUS_DI Request
To perform a PLUS_DI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_PLUS_DI Query(string symbol,
		PLUS_DI_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_PLUS_DI Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the PLUS_DI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each PLUS_DI value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **PLUS_DI_interval**

**PLUS_DI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum PLUS_DI_interval
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
## PLUS_DI Response
The response of a PLUS_DI request is an object that implements the **IAvapiResponse_PLUS_DI** interface.
```

public interface IAvapiResponse_PLUS_DI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_PLUS_DI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_PLUS_DI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_PLUS_DI_Content**.
  

***
## Complete Example: Display the result of a PLUS_DI request
```

using System;
using System.IO;
using Avapi.AvapiPLUS_DI;

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

            // Get the PLUS_DI query object
            Int_PLUS_DI plus_di =
                connection.GetQueryObject_PLUS_DI();

            // Perform the PLUS_DI request and get the result
            IAvapiResponse_PLUS_DI plus_diResponse = 
            plus_di.Query(
                 "MSFT",
                 Const_PLUS_DI.PLUS_DI_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA PLUS_DI ********");
            Console.WriteLine(plus_diResponse.RowData);

```
