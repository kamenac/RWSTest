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
        /// Converts files between formats according to the converter setup
        /// </summary>
        void Convert();
    }
}