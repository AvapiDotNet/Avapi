using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvapiGenerator
{
    public class CodeGenerator
    {
        private static string avapi_path;
        private static IList<Operation> operations;
        private static string version;
        private static string releaseNotes;

        public static void initCodeGenerator(string destinationPath , IList<Operation> list_operations 
            ,string path_version)
        {
            avapi_path = destinationPath;
            Directory.CreateDirectory(destinationPath);
            operations = list_operations;

            try
            {
                version = File.ReadLines(path_version).First();
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error: File.ReadLines({path_version}):{ex.Message}");
                version = "0.0.0";
            }

            try
            {
                releaseNotes = File.ReadLines(path_version).Skip(1).First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: File.ReadLines({path_version}):{ex.Message}");
                releaseNotes = "No Release Notes";
            }

            Csproj.init(avapi_path, version, releaseNotes);
            Utility.init(avapi_path);
            IAvapiConnection.init(avapi_path);
            AvapiConnection.init(avapi_path);
        }    

        internal static string GenerateFolder(string basePath, string relativePath)
        {
            string pathFolder = Path.Combine(basePath, relativePath );
            Directory.CreateDirectory(pathFolder);
            return pathFolder;
        }

        public static bool GenerateCode()
        {
            foreach (Operation operation in operations)
            {
                // Create a folder for the operation
                string pathFolder = GenerateFolder(avapi_path , operation.Name);
                if(string.IsNullOrEmpty(pathFolder))
                {
                    Console.WriteLine("Error: GenerateFolder("+ avapi_path + " , " +
                        operation.Name + ")" );
                    return false;
                }
                
                // Create an Interface for the operation
                var file_interface = new InterfaceAvApi( pathFolder , operation);
                if(!file_interface.generate_interface(operation.Response))
                {
                     Console.WriteLine("Error: "+operation.Name+"generate_interface() ");
                     return false;
                }

                // Create the constants for the operation
                var file_const = new Constant( pathFolder , operation);
                if(!file_const.generate_constant())
                {
                    Console.WriteLine("Error: "+operation.Name+"generate_constant() ");
                    return false;
                }

                // Create the implementation for the operation
                var file_implement = new Implementation( pathFolder , operation);
                if(!file_implement.generate_implementation())
                {
                    Console.WriteLine("Error: "+operation.Name+"generate_implementation() ");
                    return false;
                }

                AvapiConnection.add_entry(operation);
                IAvapiConnection.add_entry(operation);
            }

            // Create AvapiConnection.cs
            AvapiConnection.create();

            // Create IAvapiConnection.cs
            IAvapiConnection.create();

            // Create Utility.cs
            Utility.create();

            // Create main project
            Csproj.create(); 

            return true;
        }
    }
}
