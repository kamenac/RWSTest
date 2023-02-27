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
        public void WriteXmlTest()
        {
            var document = new Document { Text = "xmlText", Title = "xmlTitle" };

            var serializer = new XmlDocumentSerializer();

            var serialized = serializer.Serialize(document);

            serialized.ShouldNotBeEmpty();
        }


        [TestMethod]
        public void ReadXmlTest()
        {
            var serializer = new XmlDocumentSerializer();

            string xml = File.ReadAllText(@"TestFiles\input.xml");

            var doc = serializer.Deserialize(xml);

            doc.ShouldNotBeNull();

            doc.Title.ShouldBeEquivalentTo("Title");
            doc.Text.ShouldBeEquivalentTo("Text");
        }


    }
}
