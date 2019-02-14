using System;
using System.IO;

namespace AvapiGenerator
{
    public class Csproj
    {
        static string str_prefix;
        static string str_postfix;
        static string str_content;
        static string basePath;
        static string version;
        static string releaseNotes;

        public static void init(string path , string str_version, string str_releaseNotes)
        {
            str_content = "";
            version = str_version;
            releaseNotes = str_releaseNotes;
            basePath = path;
            init_prefix();
        }

        internal static void init_prefix()
        {
            str_content = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                "<Project Sdk=\"Microsoft.NET.Sdk\">\n" +
                "\t<PropertyGroup>\n" +
                "\t\t<TargetFrameworks>netcoreapp1.1;netstandard2.0</TargetFrameworks>\n" +
                "\t\t<Version>" + version + "</Version>\n"+
                "\t\t<PackageId>Avapi</PackageId>\n" +
                "\t\t<PackageVersion>" + version + "</PackageVersion>\n" +
                "\t\t<Authors>Simone Giuliani, Antonio Papa</Authors>\n" +
                "\t\t<Title>Alpha Vantage .NET API Wrapper</Title>\n" +
                "\t\t<Description>\n" +
                "This library allows to retrieve financial data using  Alpha Vantage API.\n\n" +
                "The official page of Avapi.NET CORE is available at this link: https://github.com/AvapiDotNet/Avapi/ \n\n" +
                "The complete documentation of Avapi.NET CORE is available at this link: https://github.com/AvapiDotNet/Avapi/wiki \n\n" +
                "To start using Avapi you just need to:\n\n" +
                "1. Register to Alpha Vantage web site and get your personal api key(https://www.alphavantage.co/support/#api-key). It's for free!\n\n" +
                "2. Install Avapi package on your project\n\n" +
                "3. Consume the Avapi library\n\n"+
                "\t\t</Description>\n" +
                "\t\t<PackageProjectUrl>https://github.com/AvapiDotNet/Avapi</PackageProjectUrl>\n" +
                "\t\t<PackageRequireLicenseAcceptance>false</PackageRequireLicenseAcceptance>\n" +
                "\t\t<PackageReleaseNotes>" + releaseNotes + "</PackageReleaseNotes>\n" +
                "\t\t<PackageTags>AlphaVantage Alpha Vantage API Wrapper Financial Data Finance .NET Core Avapi</PackageTags>\n" +
                "\t\t<PackageLicenseUrl></PackageLicenseUrl>\n" +
                "\t\t<PackageIconUrl></PackageIconUrl>\n" +
                "\t\t<RepositoryUrl></RepositoryUrl>\n" +
                "\t\t<RepositoryType></RepositoryType>\n" +
                "\t\t<PackageType/>\n" +
                "\t\t<Copyright>Copyright (c) 2018 Simone Giuliani and Antonio Papa\n" +
                "\t\tPermission is hereby granted, free of charge, to any person obtaining a copy\n" +
                "\t\tof this software and associated documentation files(the \"Software\"), to deal\n" +
                "\t\tin the Software without restriction, including without limitation the rights\n" +
                "\t\tto use, copy, modify, merge, publish, distribute, sublicense, and/ or sell\n" +
                "\t\tcopies of the Software, and to permit persons to whom the Software is\n" +
                "\t\tfurnished to do so, subject to the following conditions:\n" +
                "\t\tThe above copyright notice and this permission notice shall be included in all\n" +
                "\t\tcopies or substantial portions of the Software.\n" +
                "\t\tTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\n" +
                "\t\tIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\n" +
                "\t\tFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE\n" +
                "\t\tAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\n" +
                "\t\tLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\n" +
                "\t\tOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\n" +
                "\t\tSOFTWARE.</Copyright>\n" +
                "\t\t<AssemblyVersion>" + version + "</AssemblyVersion>\n" +
                "\t\t<FileVersion>" + version + "</FileVersion>\n" +
                "\t\t<NeutralLanguage>en-GB</NeutralLanguage>\n" +
                "\t\t<GeneratePackageOnBuild>false</GeneratePackageOnBuild>\n" +
                "\t\t<ApplicationIcon />\n" +
                "\t\t<OutputType>Library</OutputType>\n" +
                "\t\t<StartupObject />\n" +
                "\t\t<NoWin32Manifest>true</NoWin32Manifest>\n" +
                "\t</PropertyGroup>\n" +
                "\t<ItemGroup>\n" +
                "\t<PackageReference Include = \"Newtonsoft.Json\" Version = \"11.0.2\" />\n" +
                "\t</ItemGroup>\n" +
                "</Project>";
        }

        // Add a string
        public static void add_str(string str)
        {
            str_content += str;
        }

        // create the project file
        public static int create()
        {
            string projectPath = Path.Combine(basePath, "Avapi.csproj");
            using (var fileStream = new FileStream(string.Format(projectPath), FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(str_content);
            }
            return 0;
        } 
    }
}
