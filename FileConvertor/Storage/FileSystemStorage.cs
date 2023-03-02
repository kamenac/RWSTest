using FileConvertor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Storage
{
    /// <summary>
    /// FileSystemStorage - reads and writes to a file system
    /// </summary>
    public class FileSystemStorage : IStorage
    {
        private const string MatchExpressionConst = @"[a-zA-Z]:[\\\\\\]";

        /// <inheritdoc/>
        public string MatchExpressionRegex => MatchExpressionConst;

        /// <inheritdoc/>
        public string ReadFileAsString(string path)
        {
            using (FileStream sourceStream = File.Open(path, FileMode.Open))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <inheritdoc/>
        public void Write(string content, string path)
        {
            using (var targetStream = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(targetStream))
                {
                    sw.Write(content);
                }
            }
        }
    }
}