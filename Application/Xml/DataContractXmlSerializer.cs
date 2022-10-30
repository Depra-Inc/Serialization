// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.Xml
{
    public sealed class DataContractXmlSerializer : ISerializer
    {
        private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;

        public byte[] Serialize<TIn>(TIn input) =>
            SerializationHelper.SerializeToBytes(this, input);

        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var serializer = CreateSerializer(typeof(TIn));
            serializer.WriteObject(outputStream, input);
        }

        public string SerializeToPrettyString<TIn>(TIn input) => SerializeToString(input);

        public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            SerializationAsyncHelper.SerializeAsync(this, outputStream, input);

        public string SerializeToString<TIn>(TIn input) =>
            SerializationHelper.SerializeToString(this, input, ENCODING_TYPE);

        public TOut Deserialize<TOut>(string input) =>
            SerializationHelper.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

        public TOut Deserialize<TOut>(Stream inputStream)
        {
            var serializer = CreateSerializer(typeof(TOut));
            inputStream.Seek(0, SeekOrigin.Begin);
            var deserialized = serializer.ReadObject(inputStream);

            return (TOut) deserialized;
        }

        public Task<TOut> DeserializeAsync<TOut>(Stream inputStream) =>
            SerializationAsyncHelper.DeserializeAsync<TOut>(this, inputStream);

        private static DataContractSerializer CreateSerializer(Type type) => new DataContractSerializer(type);

        public override string ToString() => "DC_XMLSerializer";
    }
}