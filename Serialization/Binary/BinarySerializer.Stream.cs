﻿// SPDX-License-Identifier: Apache-2.0
// © 2022-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Depra.Serialization.Errors;

namespace Depra.Serialization.Binary
{
	/// <summary>
	/// Serializer using <see cref="BinaryFormatter"/>.
	/// </summary>
	public sealed partial class BinarySerializer : IStreamSerializer
	{
		private readonly BinaryFormatter _binaryFormatter;

		public BinarySerializer() => _binaryFormatter = new BinaryFormatter();

		public BinarySerializer(ISurrogateSelector surrogateSelector) =>
			_binaryFormatter = new BinaryFormatter { SurrogateSelector = surrogateSelector };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Serialize<TIn>(Stream outputStream, TIn input)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNullOrEmpty(outputStream, nameof(outputStream));

			_binaryFormatter.Serialize(outputStream, input);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Serialize(Stream outputStream, object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));
			Guard.AgainstNullOrEmpty(outputStream, nameof(outputStream));

			_binaryFormatter.Serialize(outputStream, input);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task SerializeAsync<TIn>(Stream outputStream, TIn input) =>
			Task.Run(() => Serialize(outputStream, input));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task SerializeAsync(Stream outputStream, object input, Type inputType) =>
			Task.Run(() => Serialize(outputStream, input, inputType));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TOut Deserialize<TOut>(Stream inputStream) =>
			(TOut) Deserialize(inputStream, typeof(TOut));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object Deserialize(Stream inputStream, Type outputType)
		{
			Guard.AgainstNull(outputType, nameof(outputType));
			Guard.AgainstNullOrEmpty(inputStream, nameof(inputStream));

			inputStream.Seek(0, SeekOrigin.Begin);
			return _binaryFormatter.Deserialize(inputStream);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask<TOut> DeserializeAsync<TOut>(Stream inputStream, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<TOut>(Deserialize<TOut>(inputStream));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask<object> DeserializeAsync(Stream inputStream, Type outputType,
			CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<object>(Deserialize(inputStream, outputType));
		}
	}
}