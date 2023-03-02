using FileConvertor.Interfaces;
using Moravia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace FileConvertor.Serializers
{
    public class JsonDocumentSerializer : IDocumentSerializer
    {
        private const string MatchFileSuffixConst = "json";

        /// <inheritdoc/>
        public string MatchFileSuffix => MatchFileSuffixConst;

        /// <inheritdoc/>
        public Document Deserialize(string input)
        {
            return JsonConvert.DeserializeObject<Document>(input);
        }

        /// <inheritdoc/>
        public string Serialize(Document input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}