## What HT_DCPHASE does?
This API returns the Hilbert transform, dominant cycle phase (HT_DCPHASE) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#htdcphase)  

***
## Including the HT_DCPHASE namespace
The very first thing to do before diving into HT_DCPHASE calls is to include the right namespace.  

```

using Avapi.AvapiHT_DCPHASE

```

## How to get a HT_DCPHASE object?
The HT_DCPHASE object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the HT_DCPHASE from it.
```

...
Int_HT_DCPHASE htdcphase = 
	connection.GetQueryObject_HT_DCPHASE();

```

## Perform a HT_DCPHASE Request
To perform a HT_DCPHASE request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_HT_DCPHASE Query(string symbol,
		HT_DCPHASE_interval interval,
		HT_DCPHASE_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_HT_DCPHASE Query(string symbol,
		string interval,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the HT_DCPHASE request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **HT_DCPHASE_interval**
* **HT_DCPHASE_series_type**

**HT_DCPHASE_interval**: The time interval between two consecutive data points in the time series.
```  

public enum HT_DCPHASE_interval
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
**HT_DCPHASE_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum HT_DCPHASE_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## HT_DCPHASE Response
The response of a HT_DCPHASE request is an object that implements the **IAvapiResponse_HT_DCPHASE** interface.
```

public interface IAvapiResponse_HT_DCPHASE
{
    string RowData
    {
        get;
    }
    IAvapiResponse_HT_DCPHASE_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_HT_DCPHASE** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_HT_DCPHASE_Content**.
  

***
## Complete Example: Display the result of a HT_DCPHASE request
```

using System;
using System.IO;
using Avapi.AvapiHT_DCPHASE;

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

            // Get the HT_DCPHASE query object
            Int_HT_DCPHASE ht_dcphase =
                connection.GetQueryObject_HT_DCPHASE();

            // Perform the HT_DCPHASE request and get the result
            IAvapiResponse_HT_DCPHASE ht_dcphaseResponse = 
            ht_dcphase.Query(
                 "MSFT",
                 Const_HT_DCPHASE.HT_DCPHASE_interval.n_1min,
                 Const_HT_DCPHASE.HT_DCPHASE_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA HT_DCPHASE ********");
            Console.WriteLine(ht_dcphaseResponse.RowData);

```
