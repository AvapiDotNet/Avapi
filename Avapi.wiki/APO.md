## What APO does?
This API returns the absolute price oscillator (APO) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#apo)  

***
## Including the APO namespace
The very first thing to do before diving into APO calls is to include the right namespace.  

```

using Avapi.AvapiAPO

```

## How to get a APO object?
The APO object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the APO from it.
```

...
Int_APO apo = 
	connection.GetQueryObject_APO();

```

## Perform a APO Request
To perform a APO request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_APO Query(string symbol,
		APO_interval interval,
		APO_series_type series_type,
		int fastperiod [OPTIONAL],
		int slowperiod [OPTIONAL],
		APO_matype matype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_APO Query(string symbol,
		string interval,
		string series_type,
		string fastperiod [OPTIONAL],
		string slowperiod [OPTIONAL],
		string matype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the APO request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **fastperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastperiod=12
* **slowperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowperiod=26
* **matype [OPTIONAL]**: Moving average type, it is a optional value. By default: matype=0, check the Alphavantage documentation for the mapping. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **APO_interval**
* **APO_series_type**
* **APO_matype**

**APO_interval**: The time interval between two consecutive data points in the time series.
```  

public enum APO_interval
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
**APO_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum APO_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
**APO_matype**: Moving average type, it is a optional value. By default: matype=0, check the Alphavantage documentation for the mapping. 
```  

public enum APO_matype
{
	none,
	n_0,
	n_1,
	n_2,
	n_3,
	n_4,
	n_5,
	n_6,
	n_7,
	n_8
}

```  
  

***
## APO Response
The response of a APO request is an object that implements the **IAvapiResponse_APO** interface.
```

public interface IAvapiResponse_APO
{
    string RowData
    {
        get;
    }
    IAvapiResponse_APO_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_APO** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_APO_Content**.
  

***
## Complete Example: Display the result of a APO request
```

using System;
using System.IO;
using Avapi.AvapiAPO;

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

            // Get the APO query object
            Int_APO apo =
                connection.GetQueryObject_APO();

            // Perform the APO request and get the result
            IAvapiResponse_APO apoResponse = 
            apo.Query(
                 "MSFT",
                 Const_APO.APO_interval.n_1min,
                 Const_APO.APO_series_type.close,
                 10,
                 10,
                 Const_APO.APO_matype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA APO ********");
            Console.WriteLine(apoResponse.RowData);

```
