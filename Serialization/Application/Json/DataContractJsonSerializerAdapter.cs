// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Application.Interfaces;

namespace Depra.Serialization.Application.Json
{
    /// <summary>
    /// Serializer using <see cref="DataContractJsonSerializer"/>.
    /// </summary>
    public sealed class DataContractJsonSerializerAdapter : ISerializationProvider
    {
        /// <inheritdoc />
        public byte[] Serialize<TIn>(TIn input) =>
            SerializationHelper.SerializeToBytes(this, input);

        /// <inheritdoc />
        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var serializer = new DataContractJsonSerializer(typeof(TIn));
            serializer.WriteObject(outputStream, input);
        }

        /// <inheritdoc />
        public async Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            await SerializationAsyncHelper.SerializeAsync(this, outputStream, input);

        /// <inheritdoc />
        public string SerializeToPrettyString<TIn>(TIn input) => SerializeToString(input);

        /// <inheritdoc />
        public string SerializeToString<TIn>(TIn input) =>
            SerializationHelper.SerializeToString(this, input, Encoding.UTF8);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(string input) =>
            SerializationHelper.DeserializeFromString<TOut>(this, input, Encoding.UTF8);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(Stream inputStream)
        {
            var serializer = new DataContractJsonSerializer(typeof(TOut));
            inputStream.Seek(0, SeekOrigin.Begin);
            var deserializedObject = serializer.ReadObject(inputStream);

            return (TOut) deserializedObject;
        }

        /// <inheritdoc />
        public Task<TOut> DeserializeAsync<TOut>(Stream inputStream) =>
            SerializationAsyncHelper.DeserializeAsync<TOut>(this, inputStream);

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="ISerializationProvider"/>.</returns>
        public override string ToString() => "DC_JsonSerializer";
    }
}