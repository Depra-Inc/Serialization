// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Json
{
	/// <summary>
	/// Serializer using <see cref="DataContractJsonSerializer"/>.
	/// </summary>
	public readonly partial struct DataContractJsonSerializerAdapter : IStreamSerializer
	{
		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			Serialize(outputStream, input, typeof(TIn));

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			new DataContractJsonSerializer(inputType).WriteObject(outputStream, input);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input)
		{
			var serializer = this;
			return Task.Run(() => serializer.Serialize(outputStream, input));
		}

		public Task SerializeAsync(Stream outputStream, object input, Type inputType)
		{
			var serializer = this;
			return Task.Run(() => serializer.Serialize(outputStream, input, inputType));
		}

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public object Deserialize(Stream inputStream, Type outputType)
		{
			var serializer = new DataContractJsonSerializer(outputType);
			inputStream.Seek(0, SeekOrigin.Begin);

			return serializer.ReadObject(inputStream);
		}

		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<TOut>(Deserialize<TOut>(inputStream));
		}

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<object>(Deserialize(inputStream, outputType));
		}

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => "DC_JsonSerializer";
	}
}