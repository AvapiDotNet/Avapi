## What MIDPRICE does?
This API returns the midpoint price (MIDPRICE) values. MIDPRICE = (highest high + lowest low)/2. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#midprice)  

***
## Including the MIDPRICE namespace
The very first thing to do before diving into MIDPRICE calls is to include the right namespace.  

```

using Avapi.AvapiMIDPRICE

```

## How to get a MIDPRICE object?
The MIDPRICE object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MIDPRICE from it.
```

...
Int_MIDPRICE midprice = 
	connection.GetQueryObject_MIDPRICE();

```

## Perform a MIDPRICE Request
To perform a MIDPRICE request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MIDPRICE Query(string symbol,
		MIDPRICE_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_MIDPRICE Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the MIDPRICE request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MIDPRICE value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MIDPRICE_interval**

**MIDPRICE_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MIDPRICE_interval
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
## MIDPRICE Response
The response of a MIDPRICE request is an object that implements the **IAvapiResponse_MIDPRICE** interface.
```

public interface IAvapiResponse_MIDPRICE
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MIDPRICE_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MIDPRICE** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MIDPRICE_Content**.
  

***
## Complete Example: Display the result of a MIDPRICE request
```

using System;
using System.IO;
using Avapi.AvapiMIDPRICE;

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

            // Get the MIDPRICE query object
            Int_MIDPRICE midprice =
                connection.GetQueryObject_MIDPRICE();

            // Perform the MIDPRICE request and get the result
            IAvapiResponse_MIDPRICE midpriceResponse = 
            midprice.Query(
                 "MSFT",
                 Const_MIDPRICE.MIDPRICE_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA MIDPRICE ********");
            Console.WriteLine(midpriceResponse.RowData);

```
