// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Binary
{
    /// <summary>
    /// Serializer using <see cref="BinaryFormatter"/>.
    /// </summary>
    [Obsolete]
    public sealed class BinarySerializer : ISerializer
    {
        private static readonly Encoding ENCODING_TYPE = Encoding.ASCII;

        private readonly BinaryFormatter _binaryFormatter;

        public BinarySerializer() =>
            _binaryFormatter = new BinaryFormatter();

        public BinarySerializer(ISurrogateSelector surrogateSelector) =>
            _binaryFormatter = new BinaryFormatter { SurrogateSelector = surrogateSelector };

        /// <inheritdoc />
        public byte[] Serialize<TIn>(TIn input) =>
            SerializerExtensions.SerializeToBytes(this, input);

        /// <inheritdoc />
        public void Serialize<TIn>(Stream outputStream, TIn input) =>
            _binaryFormatter.Serialize(outputStream, input);

        /// <inheritdoc />
        public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            SerializerExtensions.SerializeAsync(this, outputStream, input);

        /// <inheritdoc />
        public string SerializeToPrettyString<TIn>(TIn input) =>
            SerializeToString(input);

        /// <inheritdoc />
        public string SerializeToString<TIn>(TIn input) =>
            SerializerExtensions.SerializeToString(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(string input) =>
            SerializerExtensions.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(Stream inputStream)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            return (TOut) _binaryFormatter.Deserialize(inputStream);
        }

        /// <inheritdoc />
        public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, 
            CancellationToken cancellationToken = default) =>
            SerializerExtensions.DeserializeAsync<TOut>(this, inputStream, cancellationToken);

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="ISerializer"/>.</returns>
        public override string ToString() => nameof(BinarySerializer);
    }
}