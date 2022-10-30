// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
#define CSHARP8_OR_GREATER
#endif
using System.IO;
using System.Threading.Tasks;

namespace Depra.Serialization.Domain.Serializers
{
    /// <summary>
    /// Interface for all serializers for saving/restoring data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the specified object to <see cref="byte"/> array.
        /// </summary>
        byte[] Serialize<TIn>(TIn input);

        /// <summary>
        /// Serialize the specified object to <see cref="Stream"/> with encoding.
        /// </summary>
        void Serialize<TIn>(Stream outputStream, TIn input);

        Task SerializeAsync<TIn>(Stream outputStream, TIn input);

        string SerializeToPrettyString<TIn>(TIn input);
        
        string SerializeToString<TIn>(TIn input);
        
        TOut Deserialize<TOut>(string input);

#if CSHARP8_OR_GREATER
        TOut Deserialize<TOut>(ReadOnlyMemory<byte> input);
#endif

        /// <summary>
        /// Deserialize the specified object from <see cref="Stream"/> using the encoding.
        /// </summary>
        TOut Deserialize<TOut>(Stream inputStream);

        Task<TOut> DeserializeAsync<TOut>(Stream inputStream);
    }
}