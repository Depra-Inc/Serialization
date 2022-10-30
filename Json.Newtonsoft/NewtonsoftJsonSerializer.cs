// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Depra.Serialization.Infrastructure.Adapter;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
    public sealed class NewtonsoftJsonSerializer : SerializerAdapter
    {
        private readonly JsonSerializer _serializer;

        public override byte[] Serialize<TIn>(TIn input)
        {
            var serializedString = JsonConvert.SerializeObject(input);
            var bytesFromString = Encoding.UTF8.GetBytes(serializedString);

            return bytesFromString;
        }

        public override void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var bytes = Serialize(input);
            outputStream.Write(bytes);
            outputStream.Seek(0, SeekOrigin.Begin);
        }

        public override async Task SerializeAsync<TIn>(Stream outputStream, TIn input)
        {
            using var writer = new StreamWriter(outputStream, leaveOpen: true);
            using var jsonWriter = new JsonTextWriter(writer);

            _serializer.Serialize(jsonWriter, input);
            await jsonWriter.FlushAsync();
        }

        public override string SerializeToPrettyString<TIn>(TIn input) =>
            JsonConvert.SerializeObject(input, Formatting.Indented);

        public override string SerializeToString<TIn>(TIn input) => JsonConvert.SerializeObject(input);

        public override TOut Deserialize<TOut>(string input) => JsonConvert.DeserializeObject<TOut>(input);

        public override TOut Deserialize<TOut>(Stream inputStream)
        {
            ThrowIfNullOrEmpty(inputStream, nameof(inputStream));

            if (inputStream.Position == inputStream.Length)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            var buffer = new Span<byte>(new byte[inputStream.Length]);
            var bytesRead = inputStream.Read(buffer);
            if (bytesRead == 0)
            {
                throw new InvalidDataException();
            }

            var bytesAsString = Encoding.UTF8.GetString(buffer);
            using var stringReader = new StringReader(bytesAsString);
            using var jsonReader = new JsonTextReader(stringReader);
            var deserializedObject = _serializer.Deserialize<TOut>(jsonReader);

            return deserializedObject;
        }

        public override TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
        {
            ThrowIfEmpty(input.ToArray(), nameof(input));
            
            var inputAsString = Encoding.UTF8.GetString(input.Span);
            var deserializedObject = JsonConvert.DeserializeObject<TOut>(inputAsString);

            return deserializedObject;
        }

        public override async Task<TOut> DeserializeAsync<TOut>(Stream inputStream)
        {
            if (inputStream.Position == inputStream.Length)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            var buffer = new Memory<byte>(new byte[inputStream.Length]);
            var bytesRead = await inputStream.ReadAsync(buffer);
            if (bytesRead == 0)
            {
                throw new InvalidDataException();
            }

            var bytesAsString = Encoding.UTF8.GetString(buffer.Span);
            using var stringReader = new StringReader(bytesAsString);
            using var jsonReader = new JsonTextReader(stringReader);
            var deserializedObject = _serializer.Deserialize<TOut>(jsonReader);

            return deserializedObject;
        }

        public NewtonsoftJsonSerializer(JsonSerializer serializer = null) =>
            _serializer = serializer ?? JsonSerializer.CreateDefault();

        public override string ToString() => typeof(JsonSerializer).Namespace;
    }
}