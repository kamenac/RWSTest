using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Interfaces
{
    /// <summary>
    /// Document file converter
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Converts files between formats
        /// </summary>
        /// <param name="inputPath">input file path</param>
        /// <param name="outputPath">output file path</param>
        void Convert(string inputPath, string outputPath);
    }
}