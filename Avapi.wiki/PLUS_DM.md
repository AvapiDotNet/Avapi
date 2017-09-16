## What PLUS_DM does?
This API returns the plus directional movement (PLUS_DM) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#plusdm)  

***
## Including the PLUS_DM namespace
The very first thing to do before diving into PLUS_DM calls is to include the right namespace.  

```

using Avapi.AvapiPLUS_DM

```

## How to get a PLUS_DM object?
The PLUS_DM object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the PLUS_DM from it.
```

...
Int_PLUS_DM plusdm = 
	connection.GetQueryObject_PLUS_DM();

```

## Perform a PLUS_DM Request
To perform a PLUS_DM request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_PLUS_DM Query(string symbol,
		PLUS_DM_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_PLUS_DM Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the PLUS_DM request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each PLUS_DM value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **PLUS_DM_interval**

**PLUS_DM_interval**: The time interval between two consecutive data points in the time series.
```  

public enum PLUS_DM_interval
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
## PLUS_DM Response
The response of a PLUS_DM request is an object that implements the **IAvapiResponse_PLUS_DM** interface.
```

public interface IAvapiResponse_PLUS_DM
{
    string RowData
    {
        get;
    }
    IAvapiResponse_PLUS_DM_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_PLUS_DM** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_PLUS_DM_Content**.
  

***
## Complete Example: Display the result of a PLUS_DM request
```

using System;
using System.IO;
using Avapi.AvapiPLUS_DM;

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

            // Get the PLUS_DM query object
            Int_PLUS_DM plus_dm =
                connection.GetQueryObject_PLUS_DM();

            // Perform the PLUS_DM request and get the result
            IAvapiResponse_PLUS_DM plus_dmResponse = 
            plus_dm.Query(
                 "MSFT",
                 Const_PLUS_DM.PLUS_DM_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA PLUS_DM ********");
            Console.WriteLine(plus_dmResponse.RowData);

```
