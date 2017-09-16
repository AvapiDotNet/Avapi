## What HT_TRENDMODE does?
This API returns the Hilbert transform, trend vs cycle mode (HT_TRENDMODE) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#httrendmode)  

***
## Including the HT_TRENDMODE namespace
The very first thing to do before diving into HT_TRENDMODE calls is to include the right namespace.  

```

using Avapi.AvapiHT_TRENDMODE

```

## How to get a HT_TRENDMODE object?
The HT_TRENDMODE object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the HT_TRENDMODE from it.
```

...
Int_HT_TRENDMODE httrendmode = 
	connection.GetQueryObject_HT_TRENDMODE();

```

## Perform a HT_TRENDMODE Request
To perform a HT_TRENDMODE request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_HT_TRENDMODE Query(string symbol,
		HT_TRENDMODE_interval interval,
		HT_TRENDMODE_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_HT_TRENDMODE Query(string symbol,
		string interval,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the HT_TRENDMODE request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **HT_TRENDMODE_interval**
* **HT_TRENDMODE_series_type**

**HT_TRENDMODE_interval**: The time interval between two consecutive data points in the time series.
```  

public enum HT_TRENDMODE_interval
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
**HT_TRENDMODE_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum HT_TRENDMODE_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## HT_TRENDMODE Response
The response of a HT_TRENDMODE request is an object that implements the **IAvapiResponse_HT_TRENDMODE** interface.
```

public interface IAvapiResponse_HT_TRENDMODE
{
    string RowData
    {
        get;
    }
    IAvapiResponse_HT_TRENDMODE_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_HT_TRENDMODE** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_HT_TRENDMODE_Content**.
  

***
## Complete Example: Display the result of a HT_TRENDMODE request
```

using System;
using System.IO;
using Avapi.AvapiHT_TRENDMODE;

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

            // Get the HT_TRENDMODE query object
            Int_HT_TRENDMODE ht_trendmode =
                connection.GetQueryObject_HT_TRENDMODE();

            // Perform the HT_TRENDMODE request and get the result
            IAvapiResponse_HT_TRENDMODE ht_trendmodeResponse = 
            ht_trendmode.Query(
                 "MSFT",
                 Const_HT_TRENDMODE.HT_TRENDMODE_interval.n_1min,
                 Const_HT_TRENDMODE.HT_TRENDMODE_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA HT_TRENDMODE ********");
            Console.WriteLine(ht_trendmodeResponse.RowData);

```
