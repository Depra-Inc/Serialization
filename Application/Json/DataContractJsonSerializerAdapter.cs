// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.Json
{
    public sealed class DataContractJsonSerializerAdapter : ISerializer
    {
        public byte[] Serialize<TIn>(TIn input) =>
            SerializationHelper.SerializeToBytes(this, input);

        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var serializer = CreateSerializer(typeof(TIn));
            serializer.WriteObject(outputStream, input);
        }

        public async Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            await SerializationAsyncHelper.SerializeAsync(this, outputStream, input);

        public string SerializeToPrettyString<TIn>(TIn input) => SerializeToString(input);

        public string SerializeToString<TIn>(TIn input) =>
            SerializationHelper.SerializeToString(this, input, Encoding.UTF8);

        public TOut Deserialize<TOut>(string input) =>
            SerializationHelper.DeserializeFromString<TOut>(this, input, Encoding.UTF8);

        public TOut Deserialize<TOut>(Stream inputStream)
        {
            var serializer = CreateSerializer(typeof(TOut));
            inputStream.Seek(0, SeekOrigin.Begin);
            var deserializedObject = serializer.ReadObject(inputStream);

            return (TOut) deserializedObject;
        }

        public Task<TOut> DeserializeAsync<TOut>(Stream inputStream) =>
            SerializationAsyncHelper.DeserializeAsync<TOut>(this, inputStream);

        private static DataContractJsonSerializer CreateSerializer(Type objectType) =>
            new DataContractJsonSerializer(objectType);

        public override string ToString() => "DC_JsonSerializer";
    }
}