## What MACDEXT does?
This API returns the moving average convergence / divergence values with controllable moving average type. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#macdext)  

***
## Including the MACDEXT namespace
The very first thing to do before diving into MACDEXT calls is to include the right namespace.  

```

using Avapi.AvapiMACDEXT

```

## How to get a MACDEXT object?
The MACDEXT object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MACDEXT from it.
```

...
Int_MACDEXT macdext = 
	connection.GetQueryObject_MACDEXT();

```

## Perform a MACDEXT Request
To perform a MACDEXT request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MACDEXT Query(string symbol,
		MACDEXT_interval interval,
		MACDEXT_series_type series_type,
		int fastperiod [OPTIONAL],
		int slowperiod [OPTIONAL],
		int signalperiod [OPTIONAL],
		MACDEXT_fastmatype fastmatype [OPTIONAL],
		MACDEXT_slowmatype slowmatype [OPTIONAL],
		MACDEXT_signalmatype signalmatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_MACDEXT Query(string symbol,
		string interval,
		string series_type,
		string fastperiod [OPTIONAL],
		string slowperiod [OPTIONAL],
		string signalperiod [OPTIONAL],
		string fastmatype [OPTIONAL],
		string slowmatype [OPTIONAL],
		string signalmatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the MACDEXT request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **fastperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastperiod=12
* **slowperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowperiod=26
* **signalperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, signalperiod=9
* **fastmatype [OPTIONAL]**: Moving average type for the faster moving average, it is a optional value. By default: fastmatype=0, check the Alphavantage documentation for the mapping. 
* **slowmatype [OPTIONAL]**: Moving average type for the slower moving average, it is a optional value. By default: slowmatype=0, check the Alphavantage documentation for the mapping. 
* **signalmatype [OPTIONAL]**: Moving average type for the signal moving average, it is a optional value. By default: signalmatype=0, check the Alphavantage documentation for the mapping. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MACDEXT_interval**
* **MACDEXT_series_type**
* **MACDEXT_fastmatype**
* **MACDEXT_slowmatype**
* **MACDEXT_signalmatype**

**MACDEXT_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MACDEXT_interval
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
**MACDEXT_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum MACDEXT_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
**MACDEXT_fastmatype**: Moving average type for the faster moving average, it is a optional value. By default: fastmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum MACDEXT_fastmatype
{
	none,
	n_0,
	n_1,
	n_2,
	n_3,
	n_4,
	n_5,
	n_6,
	n_7,
	n_8
}

```  
**MACDEXT_slowmatype**: Moving average type for the slower moving average, it is a optional value. By default: slowmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum MACDEXT_slowmatype
{
	none,
	n_0,
	n_1,
	n_2,
	n_3,
	n_4,
	n_5,
	n_6,
	n_7,
	n_8
}

```  
**MACDEXT_signalmatype**: Moving average type for the signal moving average, it is a optional value. By default: signalmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum MACDEXT_signalmatype
{
	none,
	n_0,
	n_1,
	n_2,
	n_3,
	n_4,
	n_5,
	n_6,
	n_7,
	n_8
}

```  
  

***
## MACDEXT Response
The response of a MACDEXT request is an object that implements the **IAvapiResponse_MACDEXT** interface.
```

public interface IAvapiResponse_MACDEXT
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MACDEXT_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MACDEXT** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MACDEXT_Content**.
  

***
## Complete Example: Display the result of a MACDEXT request
```

using System;
using System.IO;
using Avapi.AvapiMACDEXT;

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

            // Get the MACDEXT query object
            Int_MACDEXT macdext =
                connection.GetQueryObject_MACDEXT();

            // Perform the MACDEXT request and get the result
            IAvapiResponse_MACDEXT macdextResponse = 
            macdext.Query(
                 "MSFT",
                 Const_MACDEXT.MACDEXT_interval.n_1min,
                 Const_MACDEXT.MACDEXT_series_type.close,
                 10,
                 10,
                 10,
                 Const_MACDEXT.MACDEXT_fastmatype.n_0,
                 Const_MACDEXT.MACDEXT_slowmatype.n_0,
                 Const_MACDEXT.MACDEXT_signalmatype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA MACDEXT ********");
            Console.WriteLine(macdextResponse.RowData);

```
