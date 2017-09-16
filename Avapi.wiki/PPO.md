## What PPO does?
This API returns the percentage price oscillator (PPO) values. The related REST API documentation is [here](https://www.alphavantage.co/documentation/#ppo)  

***
## Including the PPO namespace
The very first thing to do before diving into PPO calls is to include the right namespace.  

```

using Avapi.AvapiPPO

```

## How to get a PPO object?
The PPO object is retrieved from the Connection object.  

The snippet below shows how to get the Connection object:
```

...
IAvapiConnection connection = AvapiConnection.Instance
connection.Connect("Your Alpha Vantage API Key !!!!");
...

```
Once you got the Connection object you can extract the PPO from it.
```

...
Int_PPO ppo = 
	connection.GetQueryObject_PPO();

```

## Perform a PPO Request
To perform a PPO request you have 2 options:
1. **The request with constants**:

```

IAvapiResponse_PPO Query(string symbol,
		PPO_interval interval,
		PPO_series_type series_type,
		int fastperiod [OPTIONAL],
		int slowperiod [OPTIONAL],
		PPO_matype matype [OPTIONAL]);

```  

2. **The request without constants**:

```

IAvapiResponse_PPO Query(string symbol,
		string interval,
		string series_type,
		string fastperiod [OPTIONAL],
		string slowperiod [OPTIONAL],
		string matype [OPTIONAL]);

```  

### Parameters
The parameters below are needed to perform the PPO request.  
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
* **PPO_interval**
* **PPO_series_type**
* **PPO_matype**

**PPO_interval**: The time interval between two consecutive data points in the time series.
```  

public enum PPO_interval
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
**PPO_series_type**: The price type in the time series. The types supported are: close, open, high, low
```  

public enum PPO_series_type
{
	none,
	close,
	open,
	high,
	low
}

```  
**PPO_matype**: Moving average type, it is a optional value. By default: matype=0, check the Alphavantage documentation for the mapping. 
```  

public enum PPO_matype
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
## PPO Response
The response of a PPO request is an object that implements the **IAvapiResponse_PPO** interface.
```

public interface IAvapiResponse_PPO
{
    string RowData
    {
        get;
    }
    IAvapiResponse_PPO_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_PPO** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_PPO_Content**.
  

***
## Complete Example: Display the result of a PPO request
```

using System;
using System.IO;
using Avapi.AvapiPPO;

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

            // Get the PPO query object
            Int_PPO ppo =
                connection.GetQueryObject_PPO();

            // Perform the PPO request and get the result
            IAvapiResponse_PPO ppoResponse = 
            ppo.Query(
                 "MSFT",
                 Const_PPO.PPO_interval.n_1min,
                 Const_PPO.PPO_series_type.close,
                 10,
                 10,
                 Const_PPO.PPO_matype.n_0);

            // Printout the results
            Console.WriteLine("******** RAW DATA PPO ********");
            Console.WriteLine(ppoResponse.RowData);

```
