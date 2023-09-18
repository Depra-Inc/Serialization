// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Errors;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	/// <summary>
	/// Serializer using <see cref="JsonSerializer"/> and <see cref="JsonConvert"/>.
	/// </summary>
	public sealed partial class NewtonsoftJsonSerializer : IStreamSerializer, IMemoryOptimalDeserializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;
		private readonly JsonSerializer _serializer;
		private readonly JsonSerializerSettings _settings;

		public NewtonsoftJsonSerializer(JsonSerializer serializer = null, JsonSerializerSettings settings = null)
		{
			_serializer = serializer ?? JsonSerializer.CreateDefault(settings);
			_settings = settings;
		}

		public void Serialize<TIn>(Stream outputStream, TIn input)
		{
			var bytes = Serialize(input);
			outputStream.Write(bytes);
			outputStream.Seek(0, SeekOrigin.Begin);
		}

		public void Serialize(Stream outputStream, object input, Type inputType)
		{
			var bytes = Serialize(input, inputType);
			outputStream.Write(bytes);
			outputStream.Seek(0, SeekOrigin.Begin);
		}

		public async Task SerializeAsync<TIn>(Stream outputStream, TIn input)
		{
			await using var writer = CreateStreamWriter(outputStream);
			using var jsonWriter = new JsonTextWriter(writer);

			_serializer.Serialize(jsonWriter, input);
			await jsonWriter.FlushAsync();
		}

		public async Task SerializeAsync(Stream outputStream, object input, Type inputType)
		{
			await using var writer = CreateStreamWriter(outputStream);
			using var jsonWriter = new JsonTextWriter(writer);

			_serializer.Serialize(jsonWriter, input, inputType);
			await jsonWriter.FlushAsync();
		}

		public object Deserialize(Stream inputStream, Type outputType)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.SeekIfAtEnd();
			var buffer = new Span<byte>(new byte[inputStream.Length]);
			var totalBytes = inputStream.Read(buffer);

			Guard.Against(totalBytes == 0, () => throw new InvalidDataException());

			var bytesAsString = Encoding.UTF8.GetString(buffer);
			using var stringReader = new StringReader(bytesAsString);
			using var jsonReader = new JsonTextReader(stringReader);

			return _serializer.Deserialize(jsonReader, outputType);
		}

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
		{
			Guard.AgainstEmpty(input, nameof(input));

			var inputAsString = Encoding.UTF8.GetString(input.Span);
			return JsonConvert.DeserializeObject<TOut>(inputAsString, _settings);
		}

		public async ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream,
			CancellationToken cancellationToken = default)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.SeekIfAtEnd();
			var buffer = new Memory<byte>(new byte[inputStream.Length]);
			var totalBytes = await inputStream.ReadAsync(buffer, cancellationToken);

			Guard.Against(totalBytes == 0, () => throw new InvalidDataException());

			var bytesAsString = Encoding.UTF8.GetString(buffer.Span);
			using var stringReader = new StringReader(bytesAsString);
			using var jsonReader = new JsonTextReader(stringReader);

			return _serializer.Deserialize<TOut>(jsonReader);
		}

		public async ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.SeekIfAtEnd();
			var buffer = new Memory<byte>(new byte[inputStream.Length]);
			var totalBytes = await inputStream.ReadAsync(buffer, cancellationToken);

			Guard.Against(totalBytes == 0, () => throw new InvalidDataException());

			var bytesAsString = Encoding.UTF8.GetString(buffer.Span);
			using var stringReader = new StringReader(bytesAsString);
			using var jsonReader = new JsonTextReader(stringReader);

			return _serializer.Deserialize(jsonReader, outputType);
		}

		private static StreamWriter CreateStreamWriter(Stream stream)
		{
#if NET5_0_OR_GREATER
            return new StreamWriter(stream, leaveOpen: true);
#else
			return new StreamWriter(stream, Encoding.Default, 1024, true);
#endif
		}

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => typeof(JsonSerializer).Namespace;
	}
}