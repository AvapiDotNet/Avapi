## What STOCHRSI does?
This API returns the stochastic relative strength index (STOCHRSI) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#stochrsi)  

***
## Including the STOCHRSI namespace
The very first thing to do before diving into STOCHRSI calls is to include the right namespace.  

```

using Avapi.AvapiSTOCHRSI

```

## How to get a STOCHRSI object?
The STOCHRSI object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the STOCHRSI from it.
```

...
Int_STOCHRSI stochrsi = 
	connection.GetQueryObject_STOCHRSI();

```

## Perform a STOCHRSI Request
To perform a STOCHRSI request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_STOCHRSI Query(string symbol,
		STOCHRSI_interval interval,
		int time_period,
		STOCHRSI_series_type series_type,
		int fastkperiod [OPTIONAL],
		int fastdperiod [OPTIONAL],
		STOCHRSI_fastdmatype fastdmatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_STOCHRSI Query(string symbol,
		string interval,
		string time_period,
		string series_type,
		string fastkperiod [OPTIONAL],
		string fastdperiod [OPTIONAL],
		string fastdmatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the STOCHRSI request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **time_period**: Number of data points used to calculate each STOCHRSI value
* **series_type**: The price type in the time series. The types supported are: close, open, high, low
* **fastkperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastkperiod=5
* **fastdperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastkperiod=5
* **fastdmatype [OPTIONAL]**: Moving average type for the fastd moving average, it is a optional value. By default: fastdmatype=0, check the Alphavantage documentation for the mapping. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **STOCHRSI_interval**
* **STOCHRSI_series_type**
* **STOCHRSI_fastdmatype**

**STOCHRSI_interval**: The time interval between two consecutive data points in the time series.
```  

public enum STOCHRSI_interval
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
**STOCHRSI_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum STOCHRSI_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
**STOCHRSI_fastdmatype**: Moving average type for the fastd moving average, it is a optional value. By default: fastdmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum STOCHRSI_fastdmatype
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
## STOCHRSI Response
The response of a STOCHRSI request is an object that implements the **IAvapiResponse_STOCHRSI** interface.
```

public interface IAvapiResponse_STOCHRSI
{
    string RowData
    {
        get;
    }
    IAvapiResponse_STOCHRSI_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_STOCHRSI** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_STOCHRSI_Content**.
  

***
## Complete Example: Display the result of a STOCHRSI request
```

using System;
using System.IO;
using Avapi.AvapiSTOCHRSI;

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

            // Get the STOCHRSI query object
            Int_STOCHRSI stochrsi =
                connection.GetQueryObject_STOCHRSI();

            // Perform the STOCHRSI request and get the result
            IAvapiResponse_STOCHRSI stochrsiResponse = 
            stochrsi.Query(
                 "MSFT",
                 Const_STOCHRSI.STOCHRSI_interval.n_1min,
                 10,
                 Const_STOCHRSI.STOCHRSI_series_type.close,
                 10,
                 10,
                 Const_STOCHRSI.STOCHRSI_fastdmatype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA STOCHRSI ********");
            Console.WriteLine(stochrsiResponse.RowData);

```
