using FileConvertor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Storage
{
    /// <summary>
    /// HttpStorage - read-only IStorage implementation for accessing files on web
    /// </summary>
    public class HttpStorage : IStorage
    {
        private const string MatchExpressionConst = "^(http|https):";

        /// <inheritdoc/>
        public string MatchExpressionRegex => MatchExpressionConst;

        /// <inheritdoc/>
        public string ReadFileAsString(string path)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(path).Result;

                return response;
            }
        }

        /// <inheritdoc/>
        public void Write(string content, string path)
        {
            throw new NotImplementedException();
        }
    }
}