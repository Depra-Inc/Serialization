﻿// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Binary
{
	/// <summary>
	/// Serializer using <see cref="BinaryFormatter"/>.
	/// </summary>
	[Obsolete]
	public sealed class BinarySerializer : ISerializer, IGenericSerializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.ASCII;
		private readonly BinaryFormatter _binaryFormatter;

		public BinarySerializer() => _binaryFormatter = new BinaryFormatter();

		public BinarySerializer(ISurrogateSelector surrogateSelector) =>
			_binaryFormatter = new BinaryFormatter { SurrogateSelector = surrogateSelector };

		public byte[] Serialize<TIn>(TIn input) =>
			SerializationHelper.SerializeToBytes(this, input);

		public byte[] Serialize(object input, Type inputType) =>
			SerializationHelper.SerializeToBytes(this, input);

		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			_binaryFormatter.Serialize(outputStream, input);

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			Serialize(outputStream, input);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			SerializationHelper.SerializeAsync(this, outputStream, input);

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			SerializeAsync(outputStream, input);

		public string SerializeToString<TIn>(TIn input) =>
			SerializationHelper.SerializeToString(this, input, ENCODING_TYPE);

		public string SerializeToString(object input, Type inputType) =>
			SerializeToString(input);

		string IGenericSerializer.SerializeToPrettyString<TIn>(TIn input) =>
			SerializeToString(input);

		string ISerializer.SerializeToPrettyString(object input, Type inputType) =>
			SerializeToString(input);

		public TOut Deserialize<TOut>(string input) =>
			SerializationHelper.DeserializeFromString<TOut>(this, input, ENCODING_TYPE);

		public object Deserialize(string input, Type outputType) =>
			SerializationHelper.DeserializeFromString(this, input, ENCODING_TYPE, outputType);

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public object Deserialize(Stream inputStream, Type outputType)
		{
			inputStream.Seek(0, SeekOrigin.Begin);
			return _binaryFormatter.Deserialize(inputStream);
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
		public override string ToString() => nameof(BinarySerializer);
	}
}