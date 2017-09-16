## What NATR does?
This API returns the normalized average true range (NATR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#natr)  

***
## Including the NATR namespace
The very first thing to do before diving into NATR calls is to include the right namespace.  

```

using Avapi.AvapiNATR

```

## How to get a NATR object?
The NATR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the NATR from it.
```

...
Int_NATR natr = 
	connection.GetQueryObject_NATR();

```

## Perform a NATR Request
To perform a NATR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_NATR Query(string symbol,
		NATR_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_NATR Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the NATR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each NATR value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **NATR_interval**

**NATR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum NATR_interval
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
## NATR Response
The response of a NATR request is an object that implements the **IAvapiResponse_NATR** interface.
```

public interface IAvapiResponse_NATR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_NATR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_NATR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_NATR_Content**.
  

***
## Complete Example: Display the result of a NATR request
```

using System;
using System.IO;
using Avapi.AvapiNATR;

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

            // Get the NATR query object
            Int_NATR natr =
                connection.GetQueryObject_NATR();

            // Perform the NATR request and get the result
            IAvapiResponse_NATR natrResponse = 
            natr.Query(
                 "MSFT",
                 Const_NATR.NATR_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA NATR ********");
            Console.WriteLine(natrResponse.RowData);

```
