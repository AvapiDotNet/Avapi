## What MINUS_DM does?
This API returns the minus directional movement (MINUS_DM) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#minusdm)  

***
## Including the MINUS_DM namespace
The very first thing to do before diving into MINUS_DM calls is to include the right namespace.  

```

using Avapi.AvapiMINUS_DM

```

## How to get a MINUS_DM object?
The MINUS_DM object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MINUS_DM from it.
```

...
Int_MINUS_DM minusdm = 
	connection.GetQueryObject_MINUS_DM();

```

## Perform a MINUS_DM Request
To perform a MINUS_DM request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MINUS_DM Query(string symbol,
		MINUS_DM_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_MINUS_DM Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the MINUS_DM request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MINUS_DM value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MINUS_DM_interval**

**MINUS_DM_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MINUS_DM_interval
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
## MINUS_DM Response
The response of a MINUS_DM request is an object that implements the **IAvapiResponse_MINUS_DM** interface.
```

public interface IAvapiResponse_MINUS_DM
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MINUS_DM_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MINUS_DM** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MINUS_DM_Content**.
  

***
## Complete Example: Display the result of a MINUS_DM request
```

using System;
using System.IO;
using Avapi.AvapiMINUS_DM;

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

            // Get the MINUS_DM query object
            Int_MINUS_DM minus_dm =
                connection.GetQueryObject_MINUS_DM();

            // Perform the MINUS_DM request and get the result
            IAvapiResponse_MINUS_DM minus_dmResponse = 
            minus_dm.Query(
                 "MSFT",
                 Const_MINUS_DM.MINUS_DM_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA MINUS_DM ********");
            Console.WriteLine(minus_dmResponse.RowData);

```
