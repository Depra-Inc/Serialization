// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Depra.Serialization.Application.Helpers;
using Depra.Serialization.Domain.Serializers;

namespace Depra.Serialization.Application.Xml
{
    public sealed class StandardXmlSerializer : ISerializer
    {
        private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;

        public byte[] Serialize<TIn>(TIn input) =>
            SerializationHelper.SerializeToBytes(this, input);

        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var serializer = new XmlSerializer(typeof(TIn));
            serializer.Serialize(outputStream, input);
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
            var serializer = new XmlSerializer(typeof(TOut));
            inputStream.Seek(0, SeekOrigin.Begin);
            var deserialized = serializer.Deserialize(inputStream);

            return (TOut) deserialized;
        }

        public Task<TOut> DeserializeAsync<TOut>(Stream inputStream) =>
            SerializationAsyncHelper.DeserializeAsync<TOut>(this, inputStream);

        public override string ToString() => nameof(XmlSerializer);
    }
}