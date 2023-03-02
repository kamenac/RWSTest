using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using FileConvertor.Converter;
using Microsoft.Extensions.Configuration;
using Moravia.Domain;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            // todo: move configuration to a command line via:
            // https://www.nuget.org/packages/Microsoft.Extensions.Configuration.CommandLine/8.0.0-preview.1.23110.8

            var config = builder.Build();

            var sourceFileName = config["SourceFileName"];
            var targetFileName = config["TargetFileName"];

            try
            {
                var factory = new ConverterFactory();
                factory.RegisterAll();

                var converter = factory.Create(sourceFileName, targetFileName);
                converter.Convert();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}