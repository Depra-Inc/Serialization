// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Json
{
	/// <summary>
	/// Serializer using <see cref="DataContractJsonSerializer"/>.
	/// </summary>
	public readonly struct DataContractJsonSerializerAdapter : ISerializer, IGenericSerializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.UTF8;

		public byte[] Serialize<TIn>(TIn input) =>
			SerializationHelper.SerializeToBytes(this, input);

		public byte[] Serialize(object input, Type inputType) =>
			SerializationHelper.SerializeToBytes(this, input, inputType);

		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			Serialize(outputStream, input, typeof(TIn));

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			new DataContractJsonSerializer(inputType).WriteObject(outputStream, input);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			SerializationHelper.SerializeAsync(this, outputStream, input);

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			SerializationHelper.SerializeAsync(this, outputStream, input, inputType);

		public string SerializeToString<TIn>(TIn input) =>
			SerializationHelper.SerializeToString(this, input, ENCODING_TYPE);

		public string SerializeToString(object input, Type inputType) =>
			SerializationHelper.SerializeToString(this, input, inputType, ENCODING_TYPE);

		string IGenericSerializer.SerializeToPrettyString<TIn>(TIn input) =>
			SerializeToString(input);

		string ISerializer.SerializeToPrettyString(object input, Type inputType) =>
			SerializeToString(input, inputType);

		public TOut Deserialize<TOut>(string input) =>
			SerializationHelper.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

		public object Deserialize(string input, Type outputType) =>
			SerializationHelper.DeserializeFromString(this, input, outputType, ENCODING_TYPE);

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
			SerializationHelper.DeserializeAsync<TOut>(this, inputStream, cancellationToken);

		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default) =>
			SerializationHelper.DeserializeAsync(this, inputStream, outputType, cancellationToken);

		/// <summary>
		/// Just for tests and benchmarks.
		/// </summary>
		/// <returns>Returns the pretty name of the serializer.</returns>
		public override string ToString() => "DC_JsonSerializer";
	}
}