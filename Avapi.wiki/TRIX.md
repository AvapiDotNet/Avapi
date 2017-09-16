## What TRIX does?
This API returns the 1-day rate of change of a triple smooth exponential moving average (TRIX) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#trix)  

***
## Including the TRIX namespace
The very first thing to do before diving into TRIX calls is to include the right namespace.  

```

using Avapi.AvapiTRIX

```

## How to get a TRIX object?
The TRIX object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TRIX from it.
```

...
Int_TRIX trix = 
	connection.GetQueryObject_TRIX();

```

## Perform a TRIX Request
To perform a TRIX request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TRIX Query(string symbol,
		TRIX_interval interval,
		int time_period,
		TRIX_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_TRIX Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the TRIX request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each TRIX value. 
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TRIX_interval**
* **TRIX_series_type**

**TRIX_interval**: The time interval between two consecutive data points in the time series.
```  

public enum TRIX_interval
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
**TRIX_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum TRIX_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## TRIX Response
The response of a TRIX request is an object that implements the **IAvapiResponse_TRIX** interface.
```

public interface IAvapiResponse_TRIX
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TRIX_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TRIX** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_TRIX_Content**.
  

***
## Complete Example: Display the result of a TRIX request
```

using System;
using System.IO;
using Avapi.AvapiTRIX;

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

            // Get the TRIX query object
            Int_TRIX trix =
                connection.GetQueryObject_TRIX();

            // Perform the TRIX request and get the result
            IAvapiResponse_TRIX trixResponse = 
            trix.Query(
                 "MSFT",
                 Const_TRIX.TRIX_interval.n_1min,
                 10,
                 Const_TRIX.TRIX_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA TRIX ********");
            Console.WriteLine(trixResponse.RowData);

```
