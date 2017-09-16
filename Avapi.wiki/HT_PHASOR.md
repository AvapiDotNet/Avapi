## What HT_PHASOR does?
This API returns the Hilbert transform, phasor components (HT_PHASOR) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#htphasor)  

***
## Including the HT_PHASOR namespace
The very first thing to do before diving into HT_PHASOR calls is to include the right namespace.  

```

using Avapi.AvapiHT_PHASOR

```

## How to get a HT_PHASOR object?
The HT_PHASOR object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the HT_PHASOR from it.
```

...
Int_HT_PHASOR htphasor = 
	connection.GetQueryObject_HT_PHASOR();

```

## Perform a HT_PHASOR Request
To perform a HT_PHASOR request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_HT_PHASOR Query(string symbol,
		HT_PHASOR_interval interval,
		HT_PHASOR_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_HT_PHASOR Query(string symbol,
		string interval,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the HT_PHASOR request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **HT_PHASOR_interval**
* **HT_PHASOR_series_type**

**HT_PHASOR_interval**: The time interval between two consecutive data points in the time series.
```  

public enum HT_PHASOR_interval
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
**HT_PHASOR_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum HT_PHASOR_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## HT_PHASOR Response
The response of a HT_PHASOR request is an object that implements the **IAvapiResponse_HT_PHASOR** interface.
```

public interface IAvapiResponse_HT_PHASOR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_HT_PHASOR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_HT_PHASOR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_HT_PHASOR_Content**.
  

***
## Complete Example: Display the result of a HT_PHASOR request
```

using System;
using System.IO;
using Avapi.AvapiHT_PHASOR;

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

            // Get the HT_PHASOR query object
            Int_HT_PHASOR ht_phasor =
                connection.GetQueryObject_HT_PHASOR();

            // Perform the HT_PHASOR request and get the result
            IAvapiResponse_HT_PHASOR ht_phasorResponse = 
            ht_phasor.Query(
                 "MSFT",
                 Const_HT_PHASOR.HT_PHASOR_interval.n_1min,
                 Const_HT_PHASOR.HT_PHASOR_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA HT_PHASOR ********");
            Console.WriteLine(ht_phasorResponse.RowData);

```
