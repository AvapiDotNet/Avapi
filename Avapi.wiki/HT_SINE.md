## What HT_SINE does?
This API returns the Hilbert transform, sine wave (HT_SINE) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#htsine)  

***
## Including the HT_SINE namespace
The very first thing to do before diving into HT_SINE calls is to include the right namespace.  

```

using Avapi.AvapiHT_SINE

```

## How to get a HT_SINE object?
The HT_SINE object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the HT_SINE from it.
```

...
Int_HT_SINE htsine = 
	connection.GetQueryObject_HT_SINE();

```

## Perform a HT_SINE Request
To perform a HT_SINE request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_HT_SINE Query(string symbol,
		HT_SINE_interval interval,
		HT_SINE_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_HT_SINE Query(string symbol,
		string interval,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the HT_SINE request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **HT_SINE_interval**
* **HT_SINE_series_type**

**HT_SINE_interval**: The time interval between two consecutive data points in the time series.
```  

public enum HT_SINE_interval
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
**HT_SINE_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum HT_SINE_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## HT_SINE Response
The response of a HT_SINE request is an object that implements the **IAvapiResponse_HT_SINE** interface.
```

public interface IAvapiResponse_HT_SINE
{
    string RowData
    {
        get;
    }
    IAvapiResponse_HT_SINE_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_HT_SINE** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_HT_SINE_Content**.
  

***
## Complete Example: Display the result of a HT_SINE request
```

using System;
using System.IO;
using Avapi.AvapiHT_SINE;

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

            // Get the HT_SINE query object
            Int_HT_SINE ht_sine =
                connection.GetQueryObject_HT_SINE();

            // Perform the HT_SINE request and get the result
            IAvapiResponse_HT_SINE ht_sineResponse = 
            ht_sine.Query(
                 "MSFT",
                 Const_HT_SINE.HT_SINE_interval.n_1min,
                 Const_HT_SINE.HT_SINE_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA HT_SINE ********");
            Console.WriteLine(ht_sineResponse.RowData);

```
