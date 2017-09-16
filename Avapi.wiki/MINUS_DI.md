## What MINUS_DI does?
This API returns the minus directional indicator (MINUS_DI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#minusdi)  

***
## Including the MINUS_DI namespace
The very first thing to do before diving into MINUS_DI calls is to include the right namespace.  

```

using Avapi.AvapiMINUS_DI

```

## How to get a MINUS_DI object?
The MINUS_DI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MINUS_DI from it.
```

...
Int_MINUS_DI minusdi = 
	connection.GetQueryObject_MINUS_DI();

```

## Perform a MINUS_DI Request
To perform a MINUS_DI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MINUS_DI Query(string symbol,
		MINUS_DI_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_MINUS_DI Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the MINUS_DI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MINUS_DI value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MINUS_DI_interval**

**MINUS_DI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MINUS_DI_interval
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
## MINUS_DI Response
The response of a MINUS_DI request is an object that implements the **IAvapiResponse_MINUS_DI** interface.
```

public interface IAvapiResponse_MINUS_DI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MINUS_DI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MINUS_DI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MINUS_DI_Content**.
  

***
## Complete Example: Display the result of a MINUS_DI request
```

using System;
using System.IO;
using Avapi.AvapiMINUS_DI;

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

            // Get the MINUS_DI query object
            Int_MINUS_DI minus_di =
                connection.GetQueryObject_MINUS_DI();

            // Perform the MINUS_DI request and get the result
            IAvapiResponse_MINUS_DI minus_diResponse = 
            minus_di.Query(
                 "MSFT",
                 Const_MINUS_DI.MINUS_DI_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA MINUS_DI ********");
            Console.WriteLine(minus_diResponse.RowData);

```
