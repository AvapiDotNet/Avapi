## What STOCH does?
This API returns the stochastic oscillator (STOCH) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#stoch)  

***
## Including the STOCH namespace
The very first thing to do before diving into STOCH calls is to include the right namespace.  

```

using Avapi.AvapiSTOCH

```

## How to get a STOCH object?
The STOCH object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the STOCH from it.
```

...
Int_STOCH stoch = 
	connection.GetQueryObject_STOCH();

```

## Perform a STOCH Request
To perform a STOCH request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_STOCH Query(string symbol,
		STOCH_interval interval,
		int fastkperiod [OPTIONAL],
		int slowkperiod [OPTIONAL],
		int slowdperiod [OPTIONAL],
		STOCH_slowkmatype slowkmatype [OPTIONAL],
		STOCH_slowdmatype slowdmatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_STOCH Query(string symbol,
		string interval,
		string fastkperiod [OPTIONAL],
		string slowkperiod [OPTIONAL],
		string slowdperiod [OPTIONAL],
		string slowkmatype [OPTIONAL],
		string slowdmatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the STOCH request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **fastkperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastkperiod=5
* **slowkperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowkperiod=3
* **slowdperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, slowdperiod=3
* **slowkmatype [OPTIONAL]**: Moving average type for the slowk moving average, it is a optional value. By default: slowkmatype=0, check the Alphavantage documentation for the mapping. 
* **slowdmatype [OPTIONAL]**: Moving average type for the slowd moving average, it is a optional value. By default: slowdmatype=0, check the Alphavantage documentation for the mapping. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **STOCH_interval**
* **STOCH_slowkmatype**
* **STOCH_slowdmatype**

**STOCH_interval**: The time interval between two consecutive data points in the time series.
```  

public enum STOCH_interval
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
**STOCH_slowkmatype**: Moving average type for the slowk moving average, it is a optional value. By default: slowkmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum STOCH_slowkmatype
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
**STOCH_slowdmatype**: Moving average type for the slowd moving average, it is a optional value. By default: slowdmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum STOCH_slowdmatype
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
## STOCH Response
The response of a STOCH request is an object that implements the **IAvapiResponse_STOCH** interface.
```

public interface IAvapiResponse_STOCH
{
    string RowData
    {
        get;
    }
    IAvapiResponse_STOCH_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_STOCH** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_STOCH_Content**.
  

***
## Complete Example: Display the result of a STOCH request
```

using System;
using System.IO;
using Avapi.AvapiSTOCH;

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

            // Get the STOCH query object
            Int_STOCH stoch =
                connection.GetQueryObject_STOCH();

            // Perform the STOCH request and get the result
            IAvapiResponse_STOCH stochResponse = 
            stoch.Query(
                 "MSFT",
                 Const_STOCH.STOCH_interval.n_1min,
                 10,
                 10,
                 10,
                 Const_STOCH.STOCH_slowkmatype.n_0,
                 Const_STOCH.STOCH_slowdmatype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA STOCH ********");
            Console.WriteLine(stochResponse.RowData);

```
