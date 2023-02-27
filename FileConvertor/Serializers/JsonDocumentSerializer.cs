using FileConvertor.Interfaces;
using Moravia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FileConvertor.Serializers
{
    public class JsonDocumentSerializer : IDocumentSerializer
    {
        public Document Deserialize(string input)
        {
            return JsonConvert.DeserializeObject<Document>(input);
        }

        public string Serialize(Document input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
