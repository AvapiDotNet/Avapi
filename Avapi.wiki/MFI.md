## What MFI does?
This API returns the money flow index (MFI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#mfi)  

***
## Including the MFI namespace
The very first thing to do before diving into MFI calls is to include the right namespace.  

```

using Avapi.AvapiMFI

```

## How to get a MFI object?
The MFI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the MFI from it.
```

...
Int_MFI mfi = 
	connection.GetQueryObject_MFI();

```

## Perform a MFI Request
To perform a MFI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_MFI Query(string symbol,
		MFI_interval interval,
		int time_period);

```  

2. **The request without constants**:

```

IAvapiResponse_MFI Query(string symbol,
		string interval,
		string time_period);

```  

### Parameters
The parameters below are needed to perform the MFI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each MFI value. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **MFI_interval**

**MFI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum MFI_interval
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
## MFI Response
The response of a MFI request is an object that implements the **IAvapiResponse_MFI** interface.
```

public interface IAvapiResponse_MFI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_MFI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_MFI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_MFI_Content**.
  

***
## Complete Example: Display the result of a MFI request
```

using System;
using System.IO;
using Avapi.AvapiMFI;

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

            // Get the MFI query object
            Int_MFI mfi =
                connection.GetQueryObject_MFI();

            // Perform the MFI request and get the result
            IAvapiResponse_MFI mfiResponse = 
            mfi.Query(
                 "MSFT",
                 Const_MFI.MFI_interval.n_1min,
                 10);

            // Printout the results
            Console.WriteLine("******** RAW DATA MFI ********");
            Console.WriteLine(mfiResponse.RowData);

```
