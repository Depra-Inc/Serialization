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
    /// <summary>
    /// Serializer using <see cref="JsonSerializer"/> and <see cref="JsonConvert"/>.
    /// </summary>
    public sealed class NewtonsoftJsonSerializer : SerializerAdapter
    {
        private readonly JsonSerializer _serializer;

        /// <inheritdoc />
        public override byte[] Serialize<TIn>(TIn input)
        {
            var serializedString = JsonConvert.SerializeObject(input);
            var bytesFromString = Encoding.UTF8.GetBytes(serializedString);

            return bytesFromString;
        }

        /// <inheritdoc />
        public override void Serialize<TIn>(Stream outputStream, TIn input)
        {
            var bytes = Serialize(input);
            outputStream.Write(bytes);
            outputStream.Seek(0, SeekOrigin.Begin);
        }

        /// <inheritdoc />
        public override async Task SerializeAsync<TIn>(Stream outputStream, TIn input)
        {
            await using var writer = CreateStreamWriter(outputStream);
            using var jsonWriter = new JsonTextWriter(writer);

            _serializer.Serialize(jsonWriter, input);
            await jsonWriter.FlushAsync();
        }

        /// <inheritdoc />
        public override string SerializeToPrettyString<TIn>(TIn input) =>
            JsonConvert.SerializeObject(input, Formatting.Indented);

        /// <inheritdoc />
        public override string SerializeToString<TIn>(TIn input) => JsonConvert.SerializeObject(input);

        /// <inheritdoc />
        public override TOut Deserialize<TOut>(string input) => JsonConvert.DeserializeObject<TOut>(input);

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
        {
            ThrowIfEmpty(input.ToArray(), nameof(input));

            var inputAsString = Encoding.UTF8.GetString(input.Span);
            var deserializedObject = JsonConvert.DeserializeObject<TOut>(inputAsString);

            return deserializedObject;
        }

        /// <inheritdoc />
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

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="SerializerAdapter"/>.</returns>
        public override string ToString() => typeof(JsonSerializer).Namespace;

        private static StreamWriter CreateStreamWriter(Stream stream)
        {
#if NET5_0_OR_GREATER
            return new StreamWriter(stream, leaveOpen: true);
#else
            return new StreamWriter(stream, Encoding.Default, 1024, true);
#endif
        }
    }
}