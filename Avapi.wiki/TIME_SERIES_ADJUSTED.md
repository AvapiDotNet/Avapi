## What TIME_SERIES_ADJUSTED does?
This API returns daily time series (date, daily open, daily high, daily low, daily close, daily split/dividend-adjusted close, daily volume, dividend amount, split coefficient) of the equity specified, covering up to 20 years of historical data. The most recent data point is the current trading day (updated realtime).  

***
## First thing to do when using TIME_SERIES_ADJUSTED
The very first to do before diving into TIME_SERIES_ADJUSTED calls is to include the right namespace.  

```

using Avapi.AvapiTIME_SERIES_ADJUSTED

```

## How to get a TIME_SERIES_ADJUSTED object?
The TIME_SERIES_ADJUSTED object is retrieved from the Connection object.Creating and using the connection object is very easy and if you do not know how to do it please check the [[Connection Object]] section.  

```

...
IAvapiConnection connection = ... 
...
Int_TIME_SERIES_ADJUSTED timeseriesadjusted = 
	connection.GetQueryObject_TIME_SERIES_ADJUSTED();

```

## Perform a TIME_SERIES_ADJUSTED request
To perform a TIME_SERIES_ADJUSTED request you have 2 options:
1. The request with constants:

```

IAvapiResponse_TIME_SERIES_ADJUSTED Query(string symbol,
		TIME_SERIES_ADJUSTED_outputsize outputsize [OPTIONAL],
		TIME_SERIES_ADJUSTED_datatype datatype [OPTIONAL]);

```  

2. The request without constants:

```

IAvapiResponse_TIME_SERIES_ADJUSTED Query(string symbol,
		string outputsize [OPTIONAL],
		string datatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the TIME_SERIES_ADJUSTED request.  
* **symbol [OPTIONAL]**: No description available
* **outputsize**: No description available
* **datatype**: No description available

Please notice that the info above are copied from the official documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* TIME_SERIES_ADJUSTED_symbol
* TIME_SERIES_ADJUSTED_outputsize
* TIME_SERIES_ADJUSTED_datatype

TIME_SERIES_ADJUSTED_outputsize

```  

public enum TIME_SERIES_ADJUSTED_outputsize
{
	none,
	compact,
	full
}

```  
TIME_SERIES_ADJUSTED_datatype

```  

public enum TIME_SERIES_ADJUSTED_datatype
{
	none,
	json,
	csv
}

```  
  

***
## What I get in response of a TIME_SERIES_ADJUSTED request?
The response of a TIME_SERIES_ADJUSTED request is an object implement the IAvapiResponse_TIME_SERIES_ADJUSTED interface.
```

public interface IAvapiResponse_TIME_SERIES_ADJUSTED
{
    string RowData
    {
        get;
    }
    IAvapiResponse_TIME_SERIES_ADJUSTED_Content Data
    {
        get;
    }
}

```
The IAvapiResponse_TIME_SERIES_ADJUSTED shows two operations: RowData and Data.
* RowData: returns the json response in string format.
* Data: it is not implemented yet but it will return the parsed response in an object implementing the interface IAvapiResponse_TIME_SERIES_ADJUSTED_Content.
  

***
## Complete Example: Display the result of a TIME_SERIES_ADJUSTED request
```

using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_ADJUSTED;

namespace Avapi
{
    public class Example
    {
        static void Main()
        {
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("XXXXXXXXXXXXX");

            // Get the TIME_SERIES_ADJUSTED query object
            Int_TIME_SERIES_ADJUSTED timeSeriesIntraday =
                connection.GetQueryObject_TIME_SERIES_ADJUSTED();

            // Perform the TIME_SERIES_ADJUSTED request and get the result
            IAvapiResponse_TIME_SERIES_ADJUSTED time_series_adjustedResponse = 
            time_series_adjusted.Query(
                 "MSFT",
                 Const_TIME_SERIES_ADJUSTED.TIME_SERIES_ADJUSTED_outputsize.compact,
                 Const_TIME_SERIES_ADJUSTED.TIME_SERIES_ADJUSTED_datatype.json,);
            // Printout the results
            Console.WriteLine(time_series_adjusted.RowData);
        }
    }
}

```

