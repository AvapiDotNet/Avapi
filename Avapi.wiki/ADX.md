## What ADX does?
This API returns the average directional movement index (ADX) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#adx)  

***
## Including the ADX namespace
The very first thing to do before diving into ADX calls is to include the right namespace.  

```

using Avapi.AvapiADX

```

## How to get a ADX object?
The ADX object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ADX from it.
```

...
Int_ADX adx = 
	connection.GetQueryObject_ADX();

```

## Perform a ADX Request
To perform a ADX request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ADX Query(string symbol,
		ADX_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_ADX Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the ADX request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each ADX value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ADX_interval**

**ADX_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ADX_interval
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
## ADX Response
The response of a ADX request is an object that implements the **IAvapiResponse_ADX** interface.
```

public interface IAvapiResponse_ADX
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ADX_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ADX** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ADX_Content**.
  

***
## Complete Example: Display the result of a ADX request
```

using System;
using System.IO;
using Avapi.AvapiADX;

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

            // Get the ADX query object
            Int_ADX adx =
                connection.GetQueryObject_ADX();

            // Perform the ADX request and get the result
            IAvapiResponse_ADX adxResponse = 
            adx.Query(
                 "MSFT",
                 Const_ADX.ADX_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA ADX ********");
            Console.WriteLine(adxResponse.RowData);

```
