# Avapi (.NET CORE)
Avapi is a .NET CORE API Wrapper allowing to retrieve data from Alpha Vantage endpoint (https://www.alphavantage.co/).  

To start using Avapi you just need to:
* Register to Alpha Vantage web site and get your personal api key (https://www.alphavantage.co/support/#api-key). It's for free!
* Install Avapi package on your project
* Consume the Avapi library

To see the complete documentation of Avapi .NET CORE click [here](https://github.com/sgiulians/AvapiDotNetCore/wiki)

## Register to Alpha Vantage
To Claim the Alpha Vantage free API key, you should register [here](https://www.alphavantage.co/support/#api-key) 


## Install AVAPI .NET CORE
You can manually download the official package [here](https://www.nuget.org/packages/Avapi/).  

... or you can get it from the .NET CLI or from the Package Manager.

### from the .NET CLI
```
dotnet add package Avapi
```
### from the Package Manager
```
Install-Package Avapi
```

## Guided Example on how to consume Avapi library
There are a number of steps you need to follow to use Avapi on .NET CORE environment:

1. Create an empty folder. Get into that folder and run the following command: 
```
dotnet new console
```

2. Add the package to your project
```
dotnet add package Avapi
```

3. Restore the project
```
dotnet restore
```

4. Replace the content of Program.cs created automatically by 1. with the following code:
```csharp

using System;
using System.IO;
using Avapi.AvapiTIME_SERIES_DAILY;

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

            // Get the TIME_SERIES_DAILY query object
            Int_TIME_SERIES_DAILY time_series_daily =
                connection.GetQueryObject_TIME_SERIES_DAILY();

            // Perform the TIME_SERIES_DAILY request and get the result
            IAvapiResponse_TIME_SERIES_DAILY time_series_dailyResponse = 
            time_series_daily.Query(
                 "MSFT",
                 Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize.compact);

            // Printout the results
            Console.WriteLine("******** RAW DATA TIME_SERIES_DAILY ********");
            Console.WriteLine(time_series_dailyResponse.RawData);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_DAILY ********");
            var data = time_series_dailyResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Information: " + data.MetaData.Information);
                Console.WriteLine("Symbol: " + data.MetaData.Symbol);
                Console.WriteLine("LastRefreshed: " + data.MetaData.LastRefreshed);
                Console.WriteLine("OutputSize: " + data.MetaData.OutputSize);
                Console.WriteLine("TimeZone: " + data.MetaData.TimeZone);
                Console.WriteLine("========================");
                Console.WriteLine("========================");
                foreach (var timeseries in data.TimeSeries)
                {
                    Console.WriteLine("open: " + timeseries.open);
                    Console.WriteLine("high: " + timeseries.high);
                    Console.WriteLine("low: " + timeseries.low);
                    Console.WriteLine("close: " + timeseries.close);
                    Console.WriteLine("volume: " + timeseries.volume);
                    Console.WriteLine("DateTime: " + timeseries.DateTime);
                    Console.WriteLine("========================");
                }
            }
        }
    }
}

```
and replace the parameter in **connection.Connect(""Your Alpha Vantage API Key !!!!")** , with your Alpha Vantage API key (to claim it see above).

5. Run the following commands: 
```
dotnet run
```

6. You can enjoy your _MSFT Daily time series_ displayed on your console :)
***

**Authors**: Simone Giuliani and Antonio Papa  

**Email**: if you have any queries or suggestions please send us an email: sgiuliani.apapa at gmail.com
