// SPDX-License-Identifier: Apache-2.0
// © 2022-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Serialization.Binary
{
	public sealed partial class BinarySerializer : ISerializer
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte[] Serialize<TIn>(TIn input)
		{
			using var memoryStream = new MemoryStream();
			Serialize(memoryStream, input);
			return memoryStream.ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte[] Serialize(object input, Type inputType)
		{
			using var memoryStream = new MemoryStream();
			Serialize(memoryStream, input, inputType);
			return memoryStream.ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TOut Deserialize<TOut>(byte[] input)
		{
			using var memoryStream = new MemoryStream(input);
			return Deserialize<TOut>(memoryStream);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object Deserialize(byte[] input, Type outputType)
		{
			using var memoryStream = new MemoryStream(input);
			return Deserialize(memoryStream, outputType);
		}

		// Just for tests and benchmarks. Returns the pretty name of the serializer.
		public override string ToString() => nameof(BinarySerializer);
	}
}