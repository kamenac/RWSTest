using FileConvertor.Serializers;
using Moravia.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoraviaTests
{
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void ReadXmlTest()
        {
            // arrange
            var serializer = new XmlDocumentSerializer();

            string xml = File.ReadAllText(@"TestFiles\input.xml");

            // act
            var doc = serializer.Deserialize(xml);

            // assert
            doc.ShouldNotBeNull();
            doc.Title.ShouldBeEquivalentTo("Title");
            doc.Text.ShouldBeEquivalentTo("Text");
        }

        [TestMethod]
        public void WriteXmlTest()
        {
            // arrange
            var document = new Document { Text = "xmlText", Title = "xmlTitle" };
            var serializer = new XmlDocumentSerializer();

            // act
            var serialized = serializer.Serialize(document);

            // assert
            serialized.ShouldNotBeEmpty();
            serialized.ShouldContain("xmlText");
        }
    }
}