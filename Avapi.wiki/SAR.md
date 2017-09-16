## What SAR does?
This API returns the parabolic SAR (SAR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#sar)  

***
## Including the SAR namespace
The very first thing to do before diving into SAR calls is to include the right namespace.  

```

using Avapi.AvapiSAR

```

## How to get a SAR object?
The SAR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the SAR from it.
```

...
Int_SAR sar = 
	connection.GetQueryObject_SAR();

```

## Perform a SAR Request
To perform a SAR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_SAR Query(string symbol,
		SAR_interval interval,
		float acceleration [OPTIONAL],
		float maximum [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_SAR Query(string symbol,
		string interval,
		string acceleration [OPTIONAL],
		string maximum [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the SAR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **acceleration [OPTIONAL]**: The acceleration factor. It is a optional value; positive float are accepted. By default, acceleration=0.01
* **maximum [OPTIONAL]**: The maximum value for the accelleration. It is a optional value; positive float are accepted. By default, maximum=0.20

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **SAR_interval**

**SAR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum SAR_interval
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
## SAR Response
The response of a SAR request is an object that implements the **IAvapiResponse_SAR** interface.
```

public interface IAvapiResponse_SAR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_SAR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_SAR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_SAR_Content**.
  

***
## Complete Example: Display the result of a SAR request
```

using System;
using System.IO;
using Avapi.AvapiSAR;

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

            // Get the SAR query object
            Int_SAR sar =
                connection.GetQueryObject_SAR();

            // Perform the SAR request and get the result
            IAvapiResponse_SAR sarResponse = 
            sar.Query(
                 "MSFT",
                 Const_SAR.SAR_interval.n_1min,
                 0.2f,
                 0.2f);

            // Printout the results
            Console.WriteLine("******** RAW DATA SAR ********");
            Console.WriteLine(sarResponse.RowData);

```
