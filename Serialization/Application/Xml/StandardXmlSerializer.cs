﻿// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.Xml
{
    /// <summary>
    /// Serializer using <see cref="XmlSerializer"/>.
    /// </summary>
    public sealed class StandardXmlSerializer : ISerializer
    {
        private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;

        /// <inheritdoc />
        public byte[] Serialize<TIn>(TIn input) =>
            MemorySerialization.SerializeToBytes(this, input);

        /// <inheritdoc />
        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var serializer = new XmlSerializer(typeof(TIn));
            serializer.Serialize(outputStream, input);
        }

        /// <inheritdoc />
        public string SerializeToPrettyString<TIn>(TIn input) =>
            SerializeToString(input);

        /// <inheritdoc />
        public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            FakeSerializerAsync.SerializeAsync(this, outputStream, input);

        /// <inheritdoc />
        public string SerializeToString<TIn>(TIn input) =>
            MemorySerialization.SerializeToString(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(string input) =>
            MemorySerialization.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(Stream inputStream)
        {
            var serializer = new XmlSerializer(typeof(TOut));
            inputStream.Seek(0, SeekOrigin.Begin);
            var deserialized = serializer.Deserialize(inputStream);

            return (TOut) deserialized;
        }

        /// <inheritdoc />
        public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, 
            CancellationToken cancellationToken = default) =>
            FakeSerializerAsync.DeserializeAsync<TOut>(this, inputStream, cancellationToken);

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="ISerializer"/>.</returns>
        public override string ToString() => nameof(XmlSerializer);
    }
}