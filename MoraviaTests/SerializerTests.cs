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
        public void ReadJsonTest()
        {
            // arrange
            var serializer = new JsonDocumentSerializer();

            string xml = File.ReadAllText(@"TestFiles\input.json");

            // act
            var doc = serializer.Deserialize(xml);

            // assert
            doc.ShouldNotBeNull();
            doc.Title.ShouldBeEquivalentTo("jsonTitle");
            doc.Text.ShouldBeEquivalentTo("jsonText");
        }

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
            doc.Title.ShouldBeEquivalentTo("xmlTitle");
            doc.Text.ShouldBeEquivalentTo("xmlText");
        }

        [TestMethod]
        public void WriteJsonTest()
        {
            // arrange
            var document = new Document { Text = "jsonText", Title = "jsonTitle" };
            var serializer = new JsonDocumentSerializer();

            // act
            var serialized = serializer.Serialize(document);

            // assert
            serialized.ShouldNotBeEmpty();
            serialized.ShouldContain(document.Text);
            serialized.ShouldContain(document.Title);
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
            serialized.ShouldContain(document.Text);
            serialized.ShouldContain(document.Title);
        }
    }
}