## What ROCR does?
This API returns the rate of change ratio (ROCR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#rocr)  

***
## Including the ROCR namespace
The very first thing to do before diving into ROCR calls is to include the right namespace.  

```

using Avapi.AvapiROCR

```

## How to get a ROCR object?
The ROCR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ROCR from it.
```

...
Int_ROCR rocr = 
	connection.GetQueryObject_ROCR();

```

## Perform a ROCR Request
To perform a ROCR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ROCR Query(string symbol,
		ROCR_interval interval,
		int time_period,
		ROCR_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_ROCR Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the ROCR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each ROCR value. 
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ROCR_interval**
* **ROCR_series_type**

**ROCR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ROCR_interval
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
**ROCR_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum ROCR_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## ROCR Response
The response of a ROCR request is an object that implements the **IAvapiResponse_ROCR** interface.
```

public interface IAvapiResponse_ROCR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ROCR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ROCR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ROCR_Content**.
  

***
## Complete Example: Display the result of a ROCR request
```

using System;
using System.IO;
using Avapi.AvapiROCR;

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

            // Get the ROCR query object
            Int_ROCR rocr =
                connection.GetQueryObject_ROCR();

            // Perform the ROCR request and get the result
            IAvapiResponse_ROCR rocrResponse = 
            rocr.Query(
                 "MSFT",
                 Const_ROCR.ROCR_interval.n_1min,
                 10,
                 Const_ROCR.ROCR_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA ROCR ********");
            Console.WriteLine(rocrResponse.RowData);

```
