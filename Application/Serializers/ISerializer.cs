// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace Depra.Serialization.Application.Serializers
{
    /// <summary>
    /// Interface for all serializers for saving/restoring data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the specified object to stream with encoding.
        /// </summary>
        void Serialize(object obj, Stream stream, Type objType);

        /// <summary>
        /// Deserialize the specified object from stream using the encoding.
        /// </summary>
        object Deserialize(Stream stream, Type objType);
    }
}