## What AROONOSC does?
This API returns the Aroon oscillator (AROONOSC) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#aroonosc)  

***
## Including the AROONOSC namespace
The very first thing to do before diving into AROONOSC calls is to include the right namespace.  

```

using Avapi.AvapiAROONOSC

```

## How to get a AROONOSC object?
The AROONOSC object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the AROONOSC from it.
```

...
Int_AROONOSC aroonosc = 
	connection.GetQueryObject_AROONOSC();

```

## Perform a AROONOSC Request
To perform a AROONOSC request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_AROONOSC Query(string symbol,
		AROONOSC_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_AROONOSC Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the AROONOSC request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each AROONOSC value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **AROONOSC_interval**

**AROONOSC_interval**: The time interval between two consecutive data points in the time series.
```  

public enum AROONOSC_interval
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
## AROONOSC Response
The response of a AROONOSC request is an object that implements the **IAvapiResponse_AROONOSC** interface.
```

public interface IAvapiResponse_AROONOSC
{
    string RowData
    {
        get;
    }
    IAvapiResponse_AROONOSC_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_AROONOSC** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_AROONOSC_Content**.
  

***
## Complete Example: Display the result of a AROONOSC request
```

using System;
using System.IO;
using Avapi.AvapiAROONOSC;

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

            // Get the AROONOSC query object
            Int_AROONOSC aroonosc =
                connection.GetQueryObject_AROONOSC();

            // Perform the AROONOSC request and get the result
            IAvapiResponse_AROONOSC aroonoscResponse = 
            aroonosc.Query(
                 "MSFT",
                 Const_AROONOSC.AROONOSC_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA AROONOSC ********");
            Console.WriteLine(aroonoscResponse.RowData);

```
