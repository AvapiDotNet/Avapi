## What AROON does?
This API returns the Aroon (AROON) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#aroon)  

***
## Including the AROON namespace
The very first thing to do before diving into AROON calls is to include the right namespace.  

```

using Avapi.AvapiAROON

```

## How to get a AROON object?
The AROON object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the AROON from it.
```

...
Int_AROON aroon = 
	connection.GetQueryObject_AROON();

```

## Perform a AROON Request
To perform a AROON request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_AROON Query(string symbol,
		AROON_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_AROON Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the AROON request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each AROON value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **AROON_interval**

**AROON_interval**: The time interval between two consecutive data points in the time series.
```  

public enum AROON_interval
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
## AROON Response
The response of a AROON request is an object that implements the **IAvapiResponse_AROON** interface.
```

public interface IAvapiResponse_AROON
{
    string RowData
    {
        get;
    }
    IAvapiResponse_AROON_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_AROON** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_AROON_Content**.
  

***
## Complete Example: Display the result of a AROON request
```

using System;
using System.IO;
using Avapi.AvapiAROON;

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

            // Get the AROON query object
            Int_AROON aroon =
                connection.GetQueryObject_AROON();

            // Perform the AROON request and get the result
            IAvapiResponse_AROON aroonResponse = 
            aroon.Query(
                 "MSFT",
                 Const_AROON.AROON_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA AROON ********");
            Console.WriteLine(aroonResponse.RowData);

```
