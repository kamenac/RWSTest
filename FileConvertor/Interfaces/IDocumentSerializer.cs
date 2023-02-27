﻿using Moravia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Interfaces
{
    public interface IDocumentSerializer
    {
        /// <summary>
        /// Deserializes an input string
        /// </summary>
        /// <param name="input">string to be deserialized</param>
        /// <returns>Document instance</returns>
        Document Deserialize(string input);

        /// <summary>
        /// Serializes a Document
        /// </summary>
        /// <param name="input">Document instance</param>
        /// <returns>serialized data</returns>
        string Serialize(Document input);
    }
}
