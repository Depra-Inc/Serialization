// SPDX-License-Identifier: Apache-2.0
// © 2022-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.IO;

namespace Depra.Serialization.Json
{
	public readonly partial struct DataContractJsonSerializerAdapter : ISerializer
	{
		public byte[] Serialize<TIn>(TIn input)
		{
			using var memoryStream = new MemoryStream();
			Serialize(memoryStream, input);
			return memoryStream.ToArray();
		}

		public byte[] Serialize(object input, Type inputType)
		{
			using var memoryStream = new MemoryStream();
			Serialize(memoryStream, input, inputType);
			return memoryStream.ToArray();
		}

		public TOut Deserialize<TOut>(byte[] input)
		{
			using var memoryStream = new MemoryStream(input);
			return Deserialize<TOut>(memoryStream);
		}

		public object Deserialize(byte[] input, Type outputType)
		{
			using var memoryStream = new MemoryStream(input);
			return Deserialize(memoryStream, outputType);
		}
	}
}