// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Errors;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Microsoft
{
	/// <summary>
	/// Serializer using <see cref="JsonSerializer"/>.
	/// </summary>
	public readonly struct MicrosoftJsonSerializer : ISerializer, IGenericSerializer, IMemoryOptimalDeserializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;
		private readonly JsonSerializerOptions _options;

		public MicrosoftJsonSerializer(JsonSerializerOptions options) =>
			_options = options;

		public byte[] Serialize<TIn>(TIn input) =>
			JsonSerializer.SerializeToUtf8Bytes(input, _options);

		public byte[] Serialize(object input, Type inputType) =>
			JsonSerializer.SerializeToUtf8Bytes(input, inputType, _options);

		public void Serialize<TIn>(Stream outputStream, TIn input)
		{
			JsonSerializer.Serialize(new Utf8JsonWriter(outputStream), input, _options);
			outputStream.Seek(0, SeekOrigin.Begin);
		}

		public void Serialize(Stream outputStream, object input, Type inputType)
		{
			JsonSerializer.Serialize(new Utf8JsonWriter(outputStream), input, inputType, _options);
			outputStream.Seek(0, SeekOrigin.Begin);
		}

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			JsonSerializer.SerializeAsync(outputStream, input, _options);

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			JsonSerializer.SerializeAsync(outputStream, input, inputType, _options);

		public string SerializeToString<TIn>(TIn input) =>
			JsonSerializer.Serialize(input, _options);

		public string SerializeToString(object input, Type inputType) =>
			JsonSerializer.Serialize(input, inputType, _options);

		public string SerializeToPrettyString<TIn>(TIn input) =>
			JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });

		public string SerializeToPrettyString(object input, Type inputType) =>
			JsonSerializer.Serialize(input, inputType, new JsonSerializerOptions { WriteIndented = true });

		public TOut Deserialize<TOut>(string input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			return JsonSerializer.Deserialize<TOut>(ENCODING_TYPE.GetBytes(input), _options);
		}

		public TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
		{
			Guard.AgainstEmpty(input, nameof(input));
			return JsonSerializer.Deserialize<TOut>(input.Span, _options);
		}

		public object Deserialize(string input, Type outputType)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			return JsonSerializer.Deserialize(ENCODING_TYPE.GetBytes(input), outputType, _options);
		}

		public TOut Deserialize<TOut>(Stream inputStream)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.SeekIfAtEnd();
			var buffer = new Span<byte>(new byte[inputStream.Length]);
			var totalBytes = inputStream.Read(buffer);

			Guard.Against(totalBytes == 0, () => throw new InvalidDataException());

			var utf8Reader = new Utf8JsonReader(buffer);
			return JsonSerializer.Deserialize<TOut>(ref utf8Reader, _options);
		}

		public object Deserialize(Stream inputStream, Type outputType)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.SeekIfAtEnd();
			var buffer = new Span<byte>(new byte[inputStream.Length]);
			var totalBytes = inputStream.Read(buffer);

			Guard.Against(totalBytes == 0, () => throw new InvalidDataException());

			var utf8Reader = new Utf8JsonReader(buffer);
			return JsonSerializer.Deserialize(ref utf8Reader, outputType, _options);
		}

		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));
			inputStream.SeekIfAtEnd();

			return JsonSerializer.DeserializeAsync<TOut>(inputStream, _options, cancellationToken);
		}

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));
			inputStream.SeekIfAtEnd();

			return JsonSerializer.DeserializeAsync(inputStream, outputType, _options, cancellationToken);
		}

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => typeof(JsonSerializer).Namespace;
	}
}