Avapi is a .NET CORE API Wrapper allowing to retrieve data from Alpha Vantage endpoint (https://www.alphavantage.co/).  

To start using Avapi you just need to:
* Register to Alpha Vantage web site and get your personal api key (https://www.alphavantage.co/support/#api-key). It's for free!
* Install Avapi package on your project
* Consume the Avapi library

To see the complete documentation of Avapi .NET CORE click [here](https://github.com/sgiulians/AvapiDotNetCore/wiki)

## Register to Alpha Vantage
To claim the Alpha Vantage free API key, you should register [here](https://www.alphavantage.co/support/#api-key) 


## Install AVAPI .NET CORE
You can manually download the official package [here](https://www.nuget.org/packages/Avapi/).  

... or you can get it from the .NET CLI or from the Package Manager.

### from the .NET CLI
```
dotnet add package Avapi --version 1.0.1
```
### from the Package Manager
```
Install-Package Avapi -Version 1.0.1
```

## Guided Example on how to consume Avapi library
There are a number of steps you need to follow to use Avapi on .NET CORE environment:

1. Create an empty folder. Get into that folder and run the following command: 
```
$ dotnet new console
```

2. Add the package to your project
```
dotnet add package Avapi --version 1.0.1
```

3. Restore the project
```
$ dotnet restore
```

4. Replace the content of Program.cs created automatically by 1. with the following code:
```
using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_INTRADAY;

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

            // Get the TIME_SERIES_INTRADAY query object
            Int_TIME_SERIES_INTRADAY time_series_intraday =
                connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // Perform the TIME_SERIES_INTRADAY request and get the result
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse = 
            time_series_intraday.Query(
                 "MSFT",
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_1min,
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.compact,
                 Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_datatype.json);
                 
            // Printout the results
            Console.WriteLine(time_series_intradayResponse.RowData);
        }
    }
}
```
and replace the parameter in **connection.Connect(""Your Alpha Vantage API Key !!!!")** , with your Alpha Vantage API key (to claim it see above).

5. Run the following commands: 
```
$ dotnet run
```

6. You can enjoy your _MSFT intraday time series_ displayed on your console :)
***

**Authors**: Simone Giuliani and Antonio Papa  

**Email**: if you have any queries or suggestions please send us an email: sgiuliani.apapa at gmail.com