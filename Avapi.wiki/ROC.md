## What ROC does?
This API returns the rate of change (ROC) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#roc)  

***
## Including the ROC namespace
The very first thing to do before diving into ROC calls is to include the right namespace.  

```

using Avapi.AvapiROC

```

## How to get a ROC object?
The ROC object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the ROC from it.
```

...
Int_ROC roc = 
	connection.GetQueryObject_ROC();

```

## Perform a ROC Request
To perform a ROC request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_ROC Query(string symbol,
		ROC_interval interval,
		int time_period,
		ROC_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_ROC Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the ROC request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each ROC value. 
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **ROC_interval**
* **ROC_series_type**

**ROC_interval**: The time interval between two consecutive data points in the time series.
```  

public enum ROC_interval
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
**ROC_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum ROC_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## ROC Response
The response of a ROC request is an object that implements the **IAvapiResponse_ROC** interface.
```

public interface IAvapiResponse_ROC
{
    string RowData
    {
        get;
    }
    IAvapiResponse_ROC_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_ROC** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_ROC_Content**.
  

***
## Complete Example: Display the result of a ROC request
```

using System;
using System.IO;
using Avapi.AvapiROC;

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

            // Get the ROC query object
            Int_ROC roc =
                connection.GetQueryObject_ROC();

            // Perform the ROC request and get the result
            IAvapiResponse_ROC rocResponse = 
            roc.Query(
                 "MSFT",
                 Const_ROC.ROC_interval.n_1min,
                 10,
                 Const_ROC.ROC_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA ROC ********");
            Console.WriteLine(rocResponse.RowData);

```
