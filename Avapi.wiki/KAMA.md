## What KAMA does?
This API returns the Kaufman adaptive moving average (KAMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#kama)  

***
## Including the KAMA namespace
The very first thing to do before diving into KAMA calls is to include the right namespace.  

```

using Avapi.AvapiKAMA

```

## How to get a KAMA object?
The KAMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the KAMA from it.
```

...
Int_KAMA kama = 
	connection.GetQueryObject_KAMA();

```

## Perform a KAMA Request
To perform a KAMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_KAMA Query(string symbol,
		KAMA_interval interval,
		int time_period,
		KAMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_KAMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the KAMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **KAMA_interval**
* **KAMA_series_type**

**KAMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum KAMA_interval
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
**KAMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum KAMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## KAMA Response
The response of a KAMA request is an object that implements the **IAvapiResponse_KAMA** interface.
```

public interface IAvapiResponse_KAMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_KAMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_KAMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_KAMA_Content**.
  

***
## Complete Example: Display the result of a KAMA request
```

using System;
using System.IO;
using Avapi.AvapiKAMA;

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

            // Get the KAMA query object
            Int_KAMA kama =
                connection.GetQueryObject_KAMA();

            // Perform the KAMA request and get the result
            IAvapiResponse_KAMA kamaResponse = 
            kama.Query(
                 "MSFT",
                 Const_KAMA.KAMA_interval.n_1min,
                 10,
                 Const_KAMA.KAMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA KAMA ********");
            Console.WriteLine(kamaResponse.RowData);

```
