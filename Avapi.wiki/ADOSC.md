## What ADOSC does?
This API returns the Chaikin A/D oscillator (ADOSC) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#adosc)  

***
## Including the ADOSC namespace
The very first thing to do before diving into ADOSC calls is to include the right namespace.  

```

using Avapi.AvapiADOSC

```

## How to get a ADOSC object?
The ADOSC object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ADOSC from it.
```

...
Int_ADOSC adosc = 
	connection.GetQueryObject_ADOSC();

```

## Perform a ADOSC Request
To perform a ADOSC request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ADOSC Query(string symbol,
		ADOSC_interval interval,
		int fastperiod [OPTIONAL],
		int slowperiod [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_ADOSC Query(string symbol,
		string interval,
		string fastperiod [OPTIONAL],
		string slowperiod [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the ADOSC request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **fastperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastperiod=3
* **slowperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowperiod=10

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ADOSC_interval**

**ADOSC_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ADOSC_interval
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
## ADOSC Response
The response of a ADOSC request is an object that implements the **IAvapiResponse_ADOSC** interface.
```

public interface IAvapiResponse_ADOSC
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ADOSC_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ADOSC** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ADOSC_Content**.
  

***
## Complete Example: Display the result of a ADOSC request
```

using System;
using System.IO;
using Avapi.AvapiADOSC;

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

            // Get the ADOSC query object
            Int_ADOSC adosc =
                connection.GetQueryObject_ADOSC();

            // Perform the ADOSC request and get the result
            IAvapiResponse_ADOSC adoscResponse = 
            adosc.Query(
                 "MSFT",
                 Const_ADOSC.ADOSC_interval.n_1min,
                 10,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA ADOSC ********");
            Console.WriteLine(adoscResponse.RowData);

```
