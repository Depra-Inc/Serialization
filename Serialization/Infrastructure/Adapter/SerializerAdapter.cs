// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading.Tasks;
using Depra.Serialization.Application.Errors;
using Depra.Serialization.Application.Interfaces;

namespace Depra.Serialization.Infrastructure.Adapter
{
    /// <summary>
    /// Adapter class for third party serializers.
    /// </summary>
    public abstract class SerializerAdapter : ISerializationProvider, IMemoryOptimalDeserializationProvider
    {
        /// <inheritdoc />
        public abstract byte[] Serialize<TIn>(TIn input);

        /// <inheritdoc />
        public abstract void Serialize<TIn>(Stream outputStream, TIn input);

        /// <inheritdoc />
        public abstract Task SerializeAsync<TIn>(Stream outputStream, TIn input);

        /// <inheritdoc />
        public abstract string SerializeToPrettyString<TIn>(TIn input);

        /// <inheritdoc />
        public abstract string SerializeToString<TIn>(TIn input);

        /// <inheritdoc />
        public abstract TOut Deserialize<TOut>(string input);

        /// <inheritdoc />
        public abstract TOut Deserialize<TOut>(Stream inputStream);

        /// <inheritdoc />
        public abstract TOut Deserialize<TOut>(ReadOnlyMemory<byte> input);

        /// <inheritdoc />
        public abstract Task<TOut> DeserializeAsync<TOut>(Stream inputStream);

        protected static void ThrowIfNullOrEmpty(string input, string argumentName) =>
            Guard.AgainstNullOrEmpty(input, argumentName);

        protected static void ThrowIfNullOrEmpty(Stream inputStream, string argumentName) =>
            Guard.AgainstNullOrEmpty(inputStream, argumentName);

        protected static void ThrowIfEmpty(ReadOnlyMemory<byte> input, string argumentName) =>
            Guard.AgainstEmpty(input, argumentName);
    }
}