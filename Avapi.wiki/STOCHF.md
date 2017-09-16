## What STOCHF does?
This API returns the stochastic fast (STOCHF) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#stochf)  

***
## Including the STOCHF namespace
The very first thing to do before diving into STOCHF calls is to include the right namespace.  

```

using Avapi.AvapiSTOCHF

```

## How to get a STOCHF object?
The STOCHF object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the STOCHF from it.
```

...
Int_STOCHF stochf = 
	connection.GetQueryObject_STOCHF();

```

## Perform a STOCHF Request
To perform a STOCHF request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_STOCHF Query(string symbol,
		STOCHF_interval interval,
		int fastkperiod [OPTIONAL],
		int fastdperiod [OPTIONAL],
		STOCHF_fastdmatype fastdmatype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_STOCHF Query(string symbol,
		string interval,
		string fastkperiod [OPTIONAL],
		string fastdperiod [OPTIONAL],
		string fastdmatype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the STOCHF request.  
* **symbol**: The name of the equity
* **interval**: The time interval between two consecutive data points in the time series.
* **fastkperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastkperiod=5
* **fastdperiod [OPTIONAL]**: It is a optional value; positive integers are accepted. By default, fastdperiod=3
* **fastdmatype [OPTIONAL]**: Moving average type for the fastd moving average, it is a optional value. By default: fastdmatype=0, check the Alphavantage documentation for the mapping. 

Please notice that the info above are copied from the official alphavantage documentation, that you can find [here](https://www.alphavantage.co/documentation/).  

***
## The request with constants
The request with constants implies the use of different enums:
* **STOCHF_interval**
* **STOCHF_fastdmatype**

**STOCHF_interval**: The time interval between two consecutive data points in the time series.
```  

public enum STOCHF_interval
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
**STOCHF_fastdmatype**: Moving average type for the fastd moving average, it is a optional value. By default: fastdmatype=0, check the Alphavantage documentation for the mapping. 
```  

public enum STOCHF_fastdmatype
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
## STOCHF Response
The response of a STOCHF request is an object that implements the **IAvapiResponse_STOCHF** interface.
```

public interface IAvapiResponse_STOCHF
{
    string RowData
    {
        get;
    }
    IAvapiResponse_STOCHF_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_STOCHF** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_STOCHF_Content**.
  

***
## Complete Example: Display the result of a STOCHF request
```

using System;
using System.IO;
using Avapi.AvapiSTOCHF;

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

            // Get the STOCHF query object
            Int_STOCHF stochf =
                connection.GetQueryObject_STOCHF();

            // Perform the STOCHF request and get the result
            IAvapiResponse_STOCHF stochfResponse = 
            stochf.Query(
                 "MSFT",
                 Const_STOCHF.STOCHF_interval.n_1min,
                 10,
                 10,
                 Const_STOCHF.STOCHF_fastdmatype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA STOCHF ********");
            Console.WriteLine(stochfResponse.RowData);

```
