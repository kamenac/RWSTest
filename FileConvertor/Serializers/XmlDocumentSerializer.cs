using FileConvertor.Interfaces;
using Moravia.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FileConvertor.Serializers
{
    public class XmlDocumentSerializer : IDocumentSerializer
    {
        XmlSerializer serializer;
        public XmlDocumentSerializer()
        {
            serializer = new XmlSerializer(typeof(Document));
        }

        /// <inheritdoc/>
        public Document Deserialize(string input)
        {
            var xdoc = XDocument.Parse(input);

            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            return doc;
        }

        /// <inheritdoc/>
        public string Serialize(Document input)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, input);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
