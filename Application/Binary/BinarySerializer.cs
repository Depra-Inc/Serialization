// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Application.Interfaces;

namespace Depra.Serialization.Application.Binary
{
    /// <summary>
    /// Serializer using <see cref="BinaryFormatter"/>.
    /// </summary>
    [Obsolete]
    public sealed class BinarySerializer : ISerializationProvider
    {
        private static readonly Encoding ENCODING_TYPE = Encoding.ASCII;

        private readonly BinaryFormatter _binaryFormatter;

        /// <inheritdoc />
        public byte[] Serialize<TIn>(TIn input) => SerializationHelper.SerializeToBytes(this, input);

        /// <inheritdoc />
        public void Serialize<TIn>(Stream outputStream, TIn input) => _binaryFormatter.Serialize(outputStream, input);

        /// <inheritdoc />
        public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            SerializationAsyncHelper.SerializeAsync(this, outputStream, input);

        /// <inheritdoc />
        public string SerializeToPrettyString<TIn>(TIn input) => SerializeToString(input);

        /// <inheritdoc />
        public string SerializeToString<TIn>(TIn input) =>
            SerializationHelper.SerializeToString(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(string input) =>
            SerializationHelper.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(Stream inputStream)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            return (TOut) _binaryFormatter.Deserialize(inputStream);
        }

        /// <inheritdoc />
        public Task<TOut> DeserializeAsync<TOut>(Stream inputStream) =>
            SerializationAsyncHelper.DeserializeAsync<TOut>(this, inputStream);

        public BinarySerializer() => _binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="ISerializationProvider"/>.</returns>
        public override string ToString() => nameof(BinarySerializer);
    }
}