## What MAMA does?
This API returns the MESA adaptive moving average (MAMA) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#mama)  

***
## Including the MAMA namespace
The very first thing to do before diving into MAMA calls is to include the right namespace.  

```

using Avapi.AvapiMAMA

```

## How to get a MAMA object?
The MAMA object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MAMA from it.
```

...
Int_MAMA mama = 
	connection.GetQueryObject_MAMA();

```

## Perform a MAMA Request
To perform a MAMA request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MAMA Query(string symbol,
		MAMA_interval interval,
		MAMA_series_type series_type,
		float fastlimit [OPTIONAL],
		float slowlimit [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_MAMA Query(string symbol,
		string interval,
		string series_type,
		string fastlimit [OPTIONAL],
		string slowlimit [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the MAMA request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **fastlimit [OPTIONAL]**: It is a optional value; positive floats are accepted. By default, fastlimit=0.01
* **slowlimit [OPTIONAL]**: It is a optional value; positive floats are accepted. By default, slowlimit=0.01

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MAMA_interval**
* **MAMA_series_type**

**MAMA_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MAMA_interval
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
**MAMA_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum MAMA_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## MAMA Response
The response of a MAMA request is an object that implements the **IAvapiResponse_MAMA** interface.
```

public interface IAvapiResponse_MAMA
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MAMA_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MAMA** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MAMA_Content**.
  

***
## Complete Example: Display the result of a MAMA request
```

using System;
using System.IO;
using Avapi.AvapiMAMA;

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

            // Get the MAMA query object
            Int_MAMA mama =
                connection.GetQueryObject_MAMA();

            // Perform the MAMA request and get the result
            IAvapiResponse_MAMA mamaResponse = 
            mama.Query(
                 "MSFT",
                 Const_MAMA.MAMA_interval.n_1min,
                 Const_MAMA.MAMA_series_type.close,
                 0.2f,
                 0.2f);

            // Printout the results
            Console.WriteLine("******** RAW DATA MAMA ********");
            Console.WriteLine(mamaResponse.RowData);

```
