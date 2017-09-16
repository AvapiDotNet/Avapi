## What MOM does?
his API returns the momentum (MOM) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#mom)  

***
## Including the MOM namespace
The very first thing to do before diving into MOM calls is to include the right namespace.  

```

using Avapi.AvapiMOM

```

## How to get a MOM object?
The MOM object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MOM from it.
```

...
Int_MOM mom = 
	connection.GetQueryObject_MOM();

```

## Perform a MOM Request
To perform a MOM request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MOM Query(string symbol,
		MOM_interval interval,
		int time_period,
		MOM_series_type series_type);

```  

2. **The request without constants**:

```

IAvapiResponse_MOM Query(string symbol,
		string interval,
		string time_period,
		string series_type);

```  

### Parameters
The parameters below are needed to perform the MOM request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MOM value.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MOM_interval**
* **MOM_series_type**

**MOM_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MOM_interval
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
**MOM_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum MOM_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
  

***
## MOM Response
The response of a MOM request is an object that implements the **IAvapiResponse_MOM** interface.
```

public interface IAvapiResponse_MOM
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MOM_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MOM** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MOM_Content**.
  

***
## Complete Example: Display the result of a MOM request
```

using System;
using System.IO;
using Avapi.AvapiMOM;

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

            // Get the MOM query object
            Int_MOM mom =
                connection.GetQueryObject_MOM();

            // Perform the MOM request and get the result
            IAvapiResponse_MOM momResponse = 
            mom.Query(
                 "MSFT",
                 Const_MOM.MOM_interval.n_1min,
                 10,
                 Const_MOM.MOM_series_type.close);

            // Printout the results
            Console.WriteLine("******** RAW DATA MOM ********");
            Console.WriteLine(momResponse.RowData);

```
