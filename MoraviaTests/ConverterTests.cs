using FileConvertor.Converter;
using FileConvertor.Serializers;
using FileConvertor.Storage;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoraviaTests
{
    [TestClass]

    public class ConverterTests
    {

        [TestMethod]
        public void ConverterFactoryCreateConverterTest()
        {
            var factory = new ConverterFactory();

            factory.Register("json", new JsonDocumentSerializer());
            factory.Register("xml", new XmlDocumentSerializer());
            factory.Register(FileSystemStorage.MatchExpression, new FileSystemStorage());

            var converter = factory.Create(
                        @"d:\file.json",
                        @"d:\file.xml"
            );

            converter.ShouldNotBeNull();
        }

        [TestMethod]
        public void ConverterFactoryMissingSerializerTest()
        {
            var factory = new ConverterFactory();

            factory.Register("json", new JsonDocumentSerializer());
            factory.Register(FileSystemStorage.MatchExpression, new FileSystemStorage());

            Should.Throw<ArgumentException>(() =>
            {
                var converter = factory.Create(
                            @"d:\file.json",
                            @"d:\file.xml" // there is no xml serializer registered
                );

            });
        }

        [TestMethod]
        public void ConverterTest()
        {
            var factory = new ConverterFactory();

            factory.Register("json", new JsonDocumentSerializer());
            factory.Register("xml", new XmlDocumentSerializer());
            factory.Register(FileSystemStorage.MatchExpression, new FileSystemStorage());

            var inputPath = Path.GetFullPath(@"TestFiles\input.json");
            var outputPath = Path.GetFullPath(@"TestFiles\output.xml");

            var converter = factory.Create(
                        inputPath,
                        outputPath
            );

            converter.Convert(inputPath, outputPath);
        }

        [TestMethod]
        public void HttpStorageConverterFactoryTest()
        {
            var factory = new ConverterFactory();

            factory.Register("json", new JsonDocumentSerializer());
            factory.Register("xml", new XmlDocumentSerializer());
            factory.Register(FileSystemStorage.MatchExpression, new FileSystemStorage());
            factory.Register(HttpStorage.MatchExpression, new HttpStorage());

            var inputPath = "https://gist.githubusercontent.com/sunilshenoy/23a3e7132c27d62599ba741bce13056a/raw/517b07fc382c843dcc7d444046d959a318695245/sample_json.json";
            var outputPath = Path.GetFullPath(@"TestFiles\output.xml");

            var converter = factory.Create(
                        inputPath,
                        outputPath
            );

            converter.ShouldNotBeNull();
        }

    }
}
