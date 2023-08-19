// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Errors;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Microsoft
{
    /// <summary>
    /// Serializer using <see cref="JsonSerializer"/>.
    /// </summary>
    public sealed class MicrosoftJsonSerializer : ISerializer, IMemoryOptimalDeserializer
    {
        private readonly JsonSerializerOptions _options;

        public MicrosoftJsonSerializer(JsonSerializerOptions options = null) =>
            _options = options;

        /// <inheritdoc />
        public byte[] Serialize<TIn>(TIn input) =>
            JsonSerializer.SerializeToUtf8Bytes(input, _options);

        /// <inheritdoc />
        public void Serialize<TIn>(Stream outputStream, TIn input)
        {
            JsonSerializer.Serialize(new Utf8JsonWriter(outputStream), input, _options);
            outputStream.Seek(0, SeekOrigin.Begin);
        }

        /// <inheritdoc />
        public async Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
            await JsonSerializer.SerializeAsync(outputStream, input, _options).ConfigureAwait(false);

        /// <inheritdoc />
        public string SerializeToPrettyString<TIn>(TIn input) =>
            JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });

        /// <inheritdoc />
        public string SerializeToString<TIn>(TIn input) =>
            JsonSerializer.Serialize(input, _options);

        /// <inheritdoc />
        public TOut Deserialize<TOut>(string input)
        {
            Guard.AgainstNullOrEmpty(input, nameof(input));
            var bytes = Encoding.UTF8.GetBytes(input);
            var deserializedObject = JsonSerializer.Deserialize<TOut>(bytes, _options);

            return deserializedObject;
        }

        /// <inheritdoc />
        public TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
        {
            Guard.AgainstEmpty(input, nameof(input));
            var deserializedObject = JsonSerializer.Deserialize<TOut>(input.Span, _options);

            return deserializedObject;
        }

        /// <inheritdoc />
        public TOut Deserialize<TOut>(Stream inputStream)
        {
            Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

            if (inputStream.Position == inputStream.Length)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            var length = (int)inputStream.Length;
            var buffer = new Span<byte>(new byte[length]);
            var bytesRead = inputStream.Read(buffer);
            if (bytesRead == 0)
            {
                throw new InvalidDataException();
            }

            var utf8Reader = new Utf8JsonReader(buffer);
            var deserializedObject = JsonSerializer.Deserialize<TOut>(ref utf8Reader, _options);

            return deserializedObject;
        }

        /// <inheritdoc />
        public async ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream,
            CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

            if (inputStream.Position == inputStream.Length)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            var deserializedObject = await JsonSerializer
                .DeserializeAsync<TOut>(inputStream, _options, cancellationToken)
                .ConfigureAwait(false);

            return deserializedObject;
        }

        /// <summary>
        /// Just for tests and benchmarks.
        /// </summary>
        /// <returns>Returns the pretty name of the <see cref="ISerializer"/>.</returns>
        public override string ToString() => typeof(JsonSerializer).Namespace;
    }
}