// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Domain.Interfaces;
using Depra.Serialization.Infrastructure.Errors;

namespace Depra.Serialization.Infrastructure.Adapter
{
    /// <summary>
    /// Adapter class for third party serializers.
    /// </summary>
    public abstract class GuardedSerializer : ISerializer, IMemoryOptimalDeserializer
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
        public abstract ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream,
            CancellationToken cancellationToken = default);

        protected void ThrowIfNullOrEmpty(string input, string argumentName) =>
            Guard.AgainstNullOrEmpty(input, argumentName);

        protected void ThrowIfNullOrEmpty(Stream inputStream, string argumentName) =>
            Guard.AgainstNullOrEmpty(inputStream, argumentName);

        protected void ThrowIfEmpty(ReadOnlyMemory<byte> input, string argumentName) =>
            Guard.AgainstEmpty(input, argumentName);
    }
}