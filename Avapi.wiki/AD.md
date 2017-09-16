## What AD does?
This API returns the Chaikin A/D line (AD) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#ad)  

***
## Including the AD namespace
The very first thing to do before diving into AD calls is to include the right namespace.  

```

using Avapi.AvapiAD

```

## How to get a AD object?
The AD object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the AD from it.
```

...
Int_AD ad = 
	connection.GetQueryObject_AD();

```

## Perform a AD Request
To perform a AD request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_AD Query(string symbol,
		AD_interval interval);

```  

2. **The request without constants**:

```

IAvapiResponse_AD Query(string symbol,
		string interval);

```  

### Parameters
The parameters below are needed to perform the AD request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **AD_interval**

**AD_interval**: The time interval between two consecutive data points in the time series.
```  

public enum AD_interval
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
## AD Response
The response of a AD request is an object that implements the **IAvapiResponse_AD** interface.
```

public interface IAvapiResponse_AD
{
    string RowData
    {
        get;
    }
    IAvapiResponse_AD_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_AD** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_AD_Content**.
  

***
## Complete Example: Display the result of a AD request
```

using System;
using System.IO;
using Avapi.AvapiAD;

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

            // Get the AD query object
            Int_AD ad =
                connection.GetQueryObject_AD();

            // Perform the AD request and get the result
            IAvapiResponse_AD adResponse = 
            ad.Query(
                 "MSFT",
                 Const_AD.AD_interval.n_1min);

            // Printout the results
            Console.WriteLine("******** RAW DATA AD ********");
            Console.WriteLine(adResponse.RowData);

```
