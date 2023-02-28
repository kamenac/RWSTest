using FileConvertor.Interfaces;
using Moravia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Converter
{
    public class Converter : IConverter
    {
        private IDocumentSerializer inputSerializer;

        private IStorage inputStorage;

        private IDocumentSerializer outputSerializer;

        private IStorage outputStorage;

        public Converter(
            IStorage inputStorage,
            IStorage outputStorage,
            IDocumentSerializer inputSerializer,
            IDocumentSerializer outputSerializer)
        {
            this.inputStorage = inputStorage;
            this.outputStorage = outputStorage;
            this.inputSerializer = inputSerializer;
            this.outputSerializer = outputSerializer;
        }

        /// <inheritdoc/>
        public void Convert(string inputPath, string outputPath)
        {
            var inputString = inputStorage.ReadFileAsString(inputPath);

            Document document = inputSerializer.Deserialize(inputString);

            var outputString = outputSerializer.Serialize(document);

            outputStorage.Write(outputString, outputPath);
        }
    }
}