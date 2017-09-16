## What ADXR does?
This API returns the average directional movement index rating (ADXR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#adxr)  

***
## Including the ADXR namespace
The very first thing to do before diving into ADXR calls is to include the right namespace.  

```

using Avapi.AvapiADXR

```

## How to get a ADXR object?
The ADXR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ADXR from it.
```

...
Int_ADXR adxr = 
	connection.GetQueryObject_ADXR();

```

## Perform a ADXR Request
To perform a ADXR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ADXR Query(string symbol,
		ADXR_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_ADXR Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the ADXR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each ADXR value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ADXR_interval**

**ADXR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ADXR_interval
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
## ADXR Response
The response of a ADXR request is an object that implements the **IAvapiResponse_ADXR** interface.
```

public interface IAvapiResponse_ADXR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ADXR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ADXR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ADXR_Content**.
  

***
## Complete Example: Display the result of a ADXR request
```

using System;
using System.IO;
using Avapi.AvapiADXR;

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

            // Get the ADXR query object
            Int_ADXR adxr =
                connection.GetQueryObject_ADXR();

            // Perform the ADXR request and get the result
            IAvapiResponse_ADXR adxrResponse = 
            adxr.Query(
                 "MSFT",
                 Const_ADXR.ADXR_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA ADXR ********");
            Console.WriteLine(adxrResponse.RowData);

```
