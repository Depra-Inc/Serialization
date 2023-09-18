// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Binary
{
	/// <summary>
	/// Serializer using <see cref="BinaryFormatter"/>.
	/// </summary>
	[Obsolete]
	public sealed partial class BinarySerializer : IStreamSerializer
	{
		private readonly BinaryFormatter _binaryFormatter;

		public BinarySerializer() => _binaryFormatter = new BinaryFormatter();

		public BinarySerializer(ISurrogateSelector surrogateSelector) =>
			_binaryFormatter = new BinaryFormatter { SurrogateSelector = surrogateSelector };

		public void Serialize<TIn>(Stream outputStream, TIn input) =>
			_binaryFormatter.Serialize(outputStream, input);

		public void Serialize(Stream outputStream, object input, Type inputType) =>
			_binaryFormatter.Serialize(outputStream, input);

		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			Task.Run(() => Serialize(outputStream, input));

		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			Task.Run(() => Serialize(outputStream, input, inputType));

		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut)Deserialize(inputStream, typeof(TOut));

		public object Deserialize(Stream inputStream, Type outputType)
		{
			inputStream.Seek(0, SeekOrigin.Begin);
			return _binaryFormatter.Deserialize(inputStream);
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
		public override string ToString() => nameof(BinarySerializer);
	}
}