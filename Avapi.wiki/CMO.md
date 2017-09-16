## What CMO does?
This API returns the Chande momentum oscillator (CMO) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#cmo)  

***
## Including the CMO namespace
The very first thing to do before diving into CMO calls is to include the right namespace.  

```

using Avapi.AvapiCMO

```

## How to get a CMO object?
The CMO object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the CMO from it.
```

...
Int_CMO cmo = 
	connection.GetQueryObject_CMO();

```

## Perform a CMO Request
To perform a CMO request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_CMO Query(string symbol,
		CMO_interval interval,
		int time_period,
		CMO_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_CMO Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the CMO request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each CMO value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **CMO_interval**
* **CMO_series_type**

**CMO_interval**: The time interval between two consecutive data points in the time series.
```  

public enum CMO_interval
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
**CMO_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum CMO_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## CMO Response
The response of a CMO request is an object that implements the **IAvapiResponse_CMO** interface.
```

public interface IAvapiResponse_CMO
{
    string RowData
    {
        get;
    }
    IAvapiResponse_CMO_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_CMO** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_CMO_Content**.
  

***
## Complete Example: Display the result of a CMO request
```

using System;
using System.IO;
using Avapi.AvapiCMO;

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

            // Get the CMO query object
            Int_CMO cmo =
                connection.GetQueryObject_CMO();

            // Perform the CMO request and get the result
            IAvapiResponse_CMO cmoResponse = 
            cmo.Query(
                 "MSFT",
                 Const_CMO.CMO_interval.n_1min,
                 10,
                 Const_CMO.CMO_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA CMO ********");
            Console.WriteLine(cmoResponse.RowData);

```
