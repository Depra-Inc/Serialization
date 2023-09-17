// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Extensions;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json
{
	/// <summary>
	/// Serializer using <see cref="DataContractJsonSerializer"/>.
	/// </summary>
	public readonly struct DataContractJsonSerializerAdapter : ISerializer, IGenericSerializer
	{
		public byte[] Serialize<TIn>(TIn input) =>
			GenericSerializerExtensions.SerializeToBytes(this, input);

		public byte[] Serialize(object input, Type inputType) =>
			Serialize(input);

		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			Serialize(outputStream, input, typeof(TIn));

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			new DataContractJsonSerializer(inputType).WriteObject(outputStream, input);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			GenericSerializerExtensions.SerializeAsync(this, outputStream, input);

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			SerializeAsync(outputStream, input);

		public string SerializeToString<TIn>(TIn input) =>
			GenericSerializerExtensions.SerializeToString(this, input, Encoding.UTF8);

		public string SerializeToString(object input, Type inputType) =>
			SerializeToString(input);

		string IGenericSerializer.SerializeToPrettyString<TIn>(TIn input) =>
			SerializeToString(input);

		string ISerializer.SerializeToPrettyString(object input, Type inputType) =>
			SerializeToString(input);

		public TOut Deserialize<TOut>(string input) =>
			GenericSerializerExtensions.DeserializeFromString<TOut>(this, input, Encoding.UTF8);

		public object Deserialize(string input, Type outputType) =>
			SerializerExtensions.DeserializeFromString(this, input, Encoding.UTF8, outputType);

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public object Deserialize(Stream inputStream, Type outputType)
		{
			var serializer = new DataContractJsonSerializer(outputType);
			inputStream.Seek(0, SeekOrigin.Begin);

			return serializer.ReadObject(inputStream);
		}

		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream,
			CancellationToken cancellationToken = default) =>
			GenericSerializerExtensions.DeserializeAsync<TOut>(this, inputStream, cancellationToken);

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default) =>
			SerializerExtensions.DeserializeAsync(this, inputStream, cancellationToken);

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => "DC_JsonSerializer";
	}
}