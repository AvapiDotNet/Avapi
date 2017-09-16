## What HT_DCPERIOD does?
This API returns the Hilbert transform, dominant cycle period (HT_DCPERIOD) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#htdcperiod)  

***
## Including the HT_DCPERIOD namespace
The very first thing to do before diving into HT_DCPERIOD calls is to include the right namespace.  

```

using Avapi.AvapiHT_DCPERIOD

```

## How to get a HT_DCPERIOD object?
The HT_DCPERIOD object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the HT_DCPERIOD from it.
```

...
Int_HT_DCPERIOD htdcperiod = 
	connection.GetQueryObject_HT_DCPERIOD();

```

## Perform a HT_DCPERIOD Request
To perform a HT_DCPERIOD request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_HT_DCPERIOD Query(string symbol,
		HT_DCPERIOD_interval interval,
		HT_DCPERIOD_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_HT_DCPERIOD Query(string symbol,
		string interval,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the HT_DCPERIOD request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **HT_DCPERIOD_interval**
* **HT_DCPERIOD_series_type**

**HT_DCPERIOD_interval**: The time interval between two consecutive data points in the time series.
```  

public enum HT_DCPERIOD_interval
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
**HT_DCPERIOD_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum HT_DCPERIOD_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## HT_DCPERIOD Response
The response of a HT_DCPERIOD request is an object that implements the **IAvapiResponse_HT_DCPERIOD** interface.
```

public interface IAvapiResponse_HT_DCPERIOD
{
    string RowData
    {
        get;
    }
    IAvapiResponse_HT_DCPERIOD_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_HT_DCPERIOD** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_HT_DCPERIOD_Content**.
  

***
## Complete Example: Display the result of a HT_DCPERIOD request
```

using System;
using System.IO;
using Avapi.AvapiHT_DCPERIOD;

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

            // Get the HT_DCPERIOD query object
            Int_HT_DCPERIOD ht_dcperiod =
                connection.GetQueryObject_HT_DCPERIOD();

            // Perform the HT_DCPERIOD request and get the result
            IAvapiResponse_HT_DCPERIOD ht_dcperiodResponse = 
            ht_dcperiod.Query(
                 "MSFT",
                 Const_HT_DCPERIOD.HT_DCPERIOD_interval.n_1min,
                 Const_HT_DCPERIOD.HT_DCPERIOD_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA HT_DCPERIOD ********");
            Console.WriteLine(ht_dcperiodResponse.RowData);

```
