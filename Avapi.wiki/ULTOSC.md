## What ULTOSC does?
This API returns the ultimate oscillator (ULTOSC) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#ultosc)  

***
## Including the ULTOSC namespace
The very first thing to do before diving into ULTOSC calls is to include the right namespace.  

```

using Avapi.AvapiULTOSC

```

## How to get a ULTOSC object?
The ULTOSC object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ULTOSC from it.
```

...
Int_ULTOSC ultosc = 
	connection.GetQueryObject_ULTOSC();

```

## Perform a ULTOSC Request
To perform a ULTOSC request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ULTOSC Query(string symbol,
		ULTOSC_interval interval,
		int timeperiod1 [OPTIONAL],
		int timeperiod2 [OPTIONAL],
		int timeperiod3 [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_ULTOSC Query(string symbol,
		string interval,
		string timeperiod1 [OPTIONAL],
		string timeperiod2 [OPTIONAL],
		string timeperiod3 [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the ULTOSC request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **timeperiod1 [OPTIONAL]**: The first time period for the indicator. It is a optional value; positive integers are accepted. By default, timeperiod1=7
* **timeperiod2 [OPTIONAL]**: The second time period for the indicator. It is a optional value; positive integers are accepted. By default, timeperiod2=14
* **timeperiod3 [OPTIONAL]**: The third time period for the indicator. It is a optional value; positive integers are accepted. By default, timeperiod3=28

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ULTOSC_interval**

**ULTOSC_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ULTOSC_interval
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
## ULTOSC Response
The response of a ULTOSC request is an object that implements the **IAvapiResponse_ULTOSC** interface.
```

public interface IAvapiResponse_ULTOSC
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ULTOSC_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ULTOSC** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ULTOSC_Content**.
  

***
## Complete Example: Display the result of a ULTOSC request
```

using System;
using System.IO;
using Avapi.AvapiULTOSC;

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

            // Get the ULTOSC query object
            Int_ULTOSC ultosc =
                connection.GetQueryObject_ULTOSC();

            // Perform the ULTOSC request and get the result
            IAvapiResponse_ULTOSC ultoscResponse = 
            ultosc.Query(
                 "MSFT",
                 Const_ULTOSC.ULTOSC_interval.n_1min,
                 10,
                 10,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA ULTOSC ********");
            Console.WriteLine(ultoscResponse.RowData);

```
