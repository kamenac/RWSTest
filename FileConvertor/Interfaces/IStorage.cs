using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Interfaces
{
    public interface IStorage
    {
        /// <summary>
        /// regular expression to match in ConverterFactory
        /// </summary>
        string MatchExpressionRegex { get; }


        /// <summary>
        /// Reads the content of a file and returns it as a string.
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns>string</returns>
        public string ReadFileAsString(string path);

        /// <summary>
        /// Writes the string content to a file.
        /// If the file already exists, it is overwritten.
        /// </summary>
        /// <param name="content">content to be written</param>
        /// <param name="path">path to the file.</param>
        public void Write(string content, string path);
    }
}