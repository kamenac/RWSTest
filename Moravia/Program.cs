using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Moravia.Domain;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\Document1.json");

            string input;
            try
            {
                using (FileStream sourceStream = File.Open(sourceFileName, FileMode.Open))
                {
                    var reader = new StreamReader(sourceStream);
                    input = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            using (var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(targetStream))
                {
                    sw.Write(serializedDoc);
                }
            }

        }
    }
}