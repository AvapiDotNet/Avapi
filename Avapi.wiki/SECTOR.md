For the SECTOR request no parameters are needed.  
  

***
## The request with constants
The request with constants implies the use of different enums:

  

***
## SECTOR Response
The response of a SECTOR request is an object that implements the **IAvapiResponse_SECTOR** interface.
```

public interface IAvapiResponse_SECTOR
{
    string RowData
    {
        get;
    }
    IAvapiResponse_SECTOR_Content Data
    {
        get;
    }
}

```
The **IAvapiResponse_SECTOR** interface has two members: RowData and Data.
* **RowData**: represents the json response in string format.
* **Data**: it is not implemented yet, but it represents the parsed response in an object implementing the interface **IAvapiResponse_SECTOR_Content**.
  

***
## Complete Example: Display the result of a SECTOR request
```

using System;
using System.IO;
using Avapi.AvapiSECTOR;

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

            // Get the SECTOR query object
            Int_SECTOR sector =
                connection.GetQueryObject_SECTOR();

            // Perform the SECTOR request and get the result
            IAvapiResponse_SECTOR sectorResponse = 
            sector.Query();

            // Printout the results
            Console.WriteLine("******** RAW DATA SECTOR ********");
            Console.WriteLine(sectorResponse.RowData);

```
