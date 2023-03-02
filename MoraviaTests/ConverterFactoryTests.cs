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
    public class ConverterFactoryTests
    {
        [TestMethod]
        public void ConverterFactoryCreateConverterTest()
        {
            // arrange
            var factory = new ConverterFactory();

            factory.RegisterSerializer(new JsonDocumentSerializer());
            factory.RegisterSerializer(new XmlDocumentSerializer());
            factory.RegisterStorage(new FileSystemStorage());

            // act
            var converter = factory.Create(
                        @"d:\file.json",
                        @"d:\file.xml"
            );

            // assert
            converter.ShouldNotBeNull();
        }

        [TestMethod]
        public void ConverterFactoryMissingSerializerTest()
        {
            // arrange
            var factory = new ConverterFactory();

            factory.RegisterSerializer(new JsonDocumentSerializer());
            factory.RegisterStorage(new FileSystemStorage());

            // act and assert

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
            // arrange
            var factory = new ConverterFactory();

            factory.RegisterSerializer(new JsonDocumentSerializer());
            factory.RegisterSerializer(new XmlDocumentSerializer());
            factory.RegisterStorage(new FileSystemStorage());

            var inputPath = Path.GetFullPath(@"TestFiles\input.json");
            var outputPath = Path.GetFullPath(@"TestFiles\output.xml");


            // act
            var converter = factory.Create(
                        inputPath,
                        outputPath
            );

            // assert
            Should.NotThrow(() =>
            {
                converter.Convert();
            });

        }

        [TestMethod]
        public void ConverterFactoryRegistrationTest()
        {
            // arrange
            var factory = new ConverterFactory();
            factory.RegisterAll();

            var inputPath = Path.GetFullPath(@"TestFiles\input.json");
            var outputPath = Path.GetFullPath(@"TestFiles\output.xml");

            // act
            var converter = factory.Create(
                        inputPath,
                        outputPath
            );

            // assert
            Should.NotThrow(() =>
            {
                converter.Convert();
            });

        }

        [TestMethod]
        public void HttpStorageConverterFactoryTest()
        {
            // arrange
            var factory = new ConverterFactory();

            factory.RegisterSerializer(new JsonDocumentSerializer());
            factory.RegisterSerializer(new XmlDocumentSerializer());
            factory.RegisterStorage(new FileSystemStorage());
            factory.RegisterStorage(new HttpStorage());

            // random json file online
            var inputPath = "https://gist.githubusercontent.com/sunilshenoy/23a3e7132c27d62599ba741bce13056a/raw/517b07fc382c843dcc7d444046d959a318695245/sample_json.json";
            var outputPath = Path.GetFullPath(@"TestFiles\output.xml");

            // act 
            var converter = factory.Create(
                        inputPath,
                        outputPath
            );

            // assert
            converter.ShouldNotBeNull();
        }
    }
}