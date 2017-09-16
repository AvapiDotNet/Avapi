## What TEMA does?
This API returns the triple exponential moving average (TEMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#tema)  

***
## Including the TEMA namespace
The very first thing to do before diving into TEMA calls is to include the right namespace.  

```

using Avapi.AvapiTEMA

```

## How to get a TEMA object?
The TEMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the TEMA from it.
```

...
Int_TEMA tema = 
	connection.GetQueryObject_TEMA();

```

## Perform a TEMA Request
To perform a TEMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_TEMA Query(string symbol,
		TEMA_interval interval,
		int time_period,
		TEMA_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_TEMA Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the TEMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each moving average value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **TEMA_interval**
* **TEMA_series_type**

**TEMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum TEMA_interval
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
**TEMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum TEMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## TEMA Response
The response of a TEMA request is an object that implements the **IAvapiResponse_TEMA** interface.
```

public interface IAvapiResponse_TEMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TEMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_TEMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_TEMA_Content**.
  

***
## Complete Example: Display the result of a TEMA request
```

using System;
using System.IO;
using Avapi.AvapiTEMA;

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

            // Get the TEMA query object
            Int_TEMA tema =
                connection.GetQueryObject_TEMA();

            // Perform the TEMA request and get the result
            IAvapiResponse_TEMA temaResponse = 
            tema.Query(
                 "MSFT",
                 Const_TEMA.TEMA_interval.n_1min,
                 10,
                 Const_TEMA.TEMA_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA TEMA ********");
            Console.WriteLine(temaResponse.RowData);

```
