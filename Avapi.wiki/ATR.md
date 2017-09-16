## What ATR does?
This API returns the average true range (ATR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#atr)  

***
## Including the ATR namespace
The very first thing to do before diving into ATR calls is to include the right namespace.  

```

using Avapi.AvapiATR

```

## How to get a ATR object?
The ATR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ATR from it.
```

...
Int_ATR atr = 
	connection.GetQueryObject_ATR();

```

## Perform a ATR Request
To perform a ATR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ATR Query(string symbol,
		ATR_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_ATR Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the ATR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each ATR value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ATR_interval**

**ATR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ATR_interval
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
## ATR Response
The response of a ATR request is an object that implements the **IAvapiResponse_ATR** interface.
```

public interface IAvapiResponse_ATR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ATR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ATR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ATR_Content**.
  

***
## Complete Example: Display the result of a ATR request
```

using System;
using System.IO;
using Avapi.AvapiATR;

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

            // Get the ATR query object
            Int_ATR atr =
                connection.GetQueryObject_ATR();

            // Perform the ATR request and get the result
            IAvapiResponse_ATR atrResponse = 
            atr.Query(
                 "MSFT",
                 Const_ATR.ATR_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA ATR ********");
            Console.WriteLine(atrResponse.RowData);

```
