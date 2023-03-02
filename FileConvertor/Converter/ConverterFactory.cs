using FileConvertor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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

        /// <summary>
        /// returns the file suffix from file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFileSuffix(string filePath)
        {
            return filePath.Split(".").LastOrDefault();
        }

        /// <summary>
        /// Returns a serializer based on the file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">thrown when the appropriate serializer was not found in registered serializers</exception>
        private IDocumentSerializer GetSerializerByPath(string filePath)
        {
            var fileSuffix = GetFileSuffix(filePath);

            if (!serializers.TryGetValue(fileSuffix, out var inputSerializer))
            {
                throw new ArgumentException($"Serializer not found for path {filePath}");
            }

            return inputSerializer;
        }

        /// <summary>
        /// Returns a storage based on the file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private IStorage GetStorageByPath(string filePath)
        {
            foreach (var storage in storages)
            {
                if (Regex.IsMatch(filePath, storage.Key))
                {
                    return storage.Value;
                }
            }

            throw new ArgumentException($"Could not find storage for path '{filePath}'");
        }

        /// <summary>
        /// Returns an converter configured according to file type and storage based on the input and output paths
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public IConverter Create(string inputPath, string outputPath)
        {
            if (string.IsNullOrEmpty(inputPath)) // todo: use something like https://github.com/ardalis/guardclauses ?
            {
                throw new ArgumentException("Input path can't be empty.");
            }

            if (string.IsNullOrEmpty(outputPath))
            {
                throw new ArgumentException("Output path can't be empty.");
            }

            var inputStorage = GetStorageByPath(inputPath);
            var outputStorage = GetStorageByPath(outputPath);

            var inputSerializer = GetSerializerByPath(inputPath);
            var outputSerializer = GetSerializerByPath(outputPath);

            var converter = new Converter(inputStorage, outputStorage, inputSerializer, outputSerializer, inputPath, outputPath);

            return converter;
        }

        /// <summary>
        /// Registers a serializer
        /// </summary>
        /// <param name="serializer"></param>
        public void RegisterSerializer(IDocumentSerializer serializer)
        {
            serializers.Add(serializer.MatchFileSuffix, serializer); // todo: check for duplicates
        }

        /// <summary>
        /// Registers a storage
        /// </summary>
        /// <param name="storage"></param>
        public void RegisterStorage(IStorage storage)
        {
            storages.Add(storage.MatchExpressionRegex, storage); // todo: check for duplicates
        }


        /// <summary>
        /// Automatically registers all the available serializers and storages
        /// </summary>
        public void RegisterAll()
        {
            var serializerTypes = Assembly.GetAssembly(typeof(IConverter)).GetTypes() // load all available serializer types
                .Where(type => typeof(IDocumentSerializer).IsAssignableFrom(type)
                        && !type.IsInterface
                        && !type.IsAbstract);

            foreach (var serializerType in serializerTypes)
            {
                var instance = (IDocumentSerializer)Activator.CreateInstance(serializerType);
                RegisterSerializer(instance);
            }

            var storageTypes = Assembly.GetAssembly(typeof(IConverter)).GetTypes() // load all available unit types
                .Where(type => typeof(IStorage).IsAssignableFrom(type)
                        && !type.IsInterface
                        && !type.IsAbstract);

            foreach (var storageType in storageTypes)
            {
                var instance = (IStorage)Activator.CreateInstance(storageType);
                RegisterStorage(instance);
            }
        }

    }
}