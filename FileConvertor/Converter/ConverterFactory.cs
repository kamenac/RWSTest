using FileConvertor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileConvertor.Converter
{
    public class ConverterFactory
    {
        private readonly Dictionary<string, IDocumentSerializer> serializers;

        private readonly Dictionary<string, IStorage> storages;

        public ConverterFactory()
        {
            serializers = new();
            storages = new();
        }

        private string GetFileSuffix(string filePath)
        {
            return filePath.Split(".").LastOrDefault();
        }

        private IStorage GetStorageByPath(string filePath)
        {
            foreach (var storage in storages)
            {
                if (Regex.IsMatch(filePath, storage.Key))
                {
                    return storage.Value;
                }
            }

            return null;
        }

        public IConverter Create(string inputPath, string outputPath)
        {
            var inputSuffix = GetFileSuffix(inputPath);
            var outputSuffix = GetFileSuffix(outputPath);
            var inputStorage = GetStorageByPath(inputPath);
            var outputStorage = GetStorageByPath(outputPath);

            if (inputStorage == null)
            {
                throw new ArgumentException($"Storage not found for path {inputPath}");
            }

            if (outputStorage == null)
            {
                throw new ArgumentException($"Storage not found for path {outputPath}");
            }

            if (!serializers.TryGetValue(inputSuffix, out var inputSerializer))
            {
                throw new ArgumentException($"Serializer not found for path {inputPath}");
            }
            if (!serializers.TryGetValue(outputSuffix, out var outputSerializer))
            {
                throw new ArgumentException($"Serializer not found for path {outputPath}");
            }

            var converter = new Converter(inputStorage, outputStorage, inputSerializer, outputSerializer);

            return converter;
        }

        public void Register(string fileSuffix, IDocumentSerializer serializer)
        {
            serializers.Add(fileSuffix, serializer); // todo: register automatically on start
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="storage"></param>
        public void Register(string regex, IStorage storage)
        {
            storages.Add(regex, storage); // todo: register automatically on start
        }
    }
}