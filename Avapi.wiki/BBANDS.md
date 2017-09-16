## What BBANDS does?
This API returns the Bollinger bands (BBANDS) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#bbands)  

***
## Including the BBANDS namespace
The very first thing to do before diving into BBANDS calls is to include the right namespace.  

```

using Avapi.AvapiBBANDS

```

## How to get a BBANDS object?
The BBANDS object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the BBANDS from it.
```

...
Int_BBANDS bbands = 
	connection.GetQueryObject_BBANDS();

```

## Perform a BBANDS Request
To perform a BBANDS request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_BBANDS Query(string symbol,
		BBANDS_interval interval,
		int time_period,
		BBANDS_series_type series_type,
		int nbdevup [OPTIONAL],
		int nbdevdn [OPTIONAL],
		BBANDS_matype matype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_BBANDS Query(string symbol,
		string interval,
		string time_period,
		string series_type,
		string nbdevup [OPTIONAL],
		string nbdevdn [OPTIONAL],
		string matype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the BBANDS request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each BBANDS value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **nbdevup [OPTIONAL]**: The standard deviation multiplier of the upper band. It is a optional value; positive integers are accepted. By default, nbdevup=2
* **nbdevdn [OPTIONAL]**: The standard deviation multiplier of the lower band. It is a optional value; positive integers are accepted. By default, nbdevdn=2
* **matype [OPTIONAL]**: Moving average type of the time series. It is a optional value; positive integers are accepted. By default, matype=0, check the Alphavantage documentation for the mapping.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **BBANDS_interval**
* **BBANDS_series_type**
* **BBANDS_matype**

**BBANDS_interval**: The time interval between two consecutive data points in the time series.
```  

public enum BBANDS_interval
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
**BBANDS_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum BBANDS_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
**BBANDS_matype**: Moving average type of the time series. It is a optional value; positive integers are accepted. By default, matype=0, check the Alphavantage documentation for the mapping.
```  

public enum BBANDS_matype
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
## BBANDS Response
The response of a BBANDS request is an object that implements the **IAvapiResponse_BBANDS** interface.
```

public interface IAvapiResponse_BBANDS
{
    string RowData
    {
        get;
    }
    IAvapiResponse_BBANDS_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_BBANDS** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_BBANDS_Content**.
  

***
## Complete Example: Display the result of a BBANDS request
```

using System;
using System.IO;
using Avapi.AvapiBBANDS;

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

            // Get the BBANDS query object
            Int_BBANDS bbands =
                connection.GetQueryObject_BBANDS();

            // Perform the BBANDS request and get the result
            IAvapiResponse_BBANDS bbandsResponse = 
            bbands.Query(
                 "MSFT",
                 Const_BBANDS.BBANDS_interval.n_1min,
                 10,
                 Const_BBANDS.BBANDS_series_type.close,
                 10,
                 10,
                 Const_BBANDS.BBANDS_matype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA BBANDS ********");
            Console.WriteLine(bbandsResponse.RowData);

```
