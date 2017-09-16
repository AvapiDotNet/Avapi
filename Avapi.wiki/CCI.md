## What CCI does?
This API returns the commodity channel index (CCI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#cci)  

***
## Including the CCI namespace
The very first thing to do before diving into CCI calls is to include the right namespace.  

```

using Avapi.AvapiCCI

```

## How to get a CCI object?
The CCI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the CCI from it.
```

...
Int_CCI cci = 
	connection.GetQueryObject_CCI();

```

## Perform a CCI Request
To perform a CCI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_CCI Query(string symbol,
		CCI_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_CCI Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the CCI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each CCI value.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **CCI_interval**

**CCI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum CCI_interval
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
## CCI Response
The response of a CCI request is an object that implements the **IAvapiResponse_CCI** interface.
```

public interface IAvapiResponse_CCI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_CCI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_CCI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_CCI_Content**.
  

***
## Complete Example: Display the result of a CCI request
```

using System;
using System.IO;
using Avapi.AvapiCCI;

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

            // Get the CCI query object
            Int_CCI cci =
                connection.GetQueryObject_CCI();

            // Perform the CCI request and get the result
            IAvapiResponse_CCI cciResponse = 
            cci.Query(
                 "MSFT",
                 Const_CCI.CCI_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA CCI ********");
            Console.WriteLine(cciResponse.RowData);

```
