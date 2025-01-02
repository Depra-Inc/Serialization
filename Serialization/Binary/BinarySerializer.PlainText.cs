// SPDX-License-Identifier: Apache-2.0
// © 2022-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;
using System.Text;
using Depra.Serialization.Internal;

namespace Depra.Serialization.Binary
{
	public sealed partial class BinarySerializer : ITextSerializer, IPrettyTextSerializer
	{
		private static readonly Encoding ENCODING_TYPE = Encoding.ASCII;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string SerializeToString<TIn>(TIn input) =>
			ENCODING_TYPE.GetString(Serialize(input));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string SerializeToString(object input, Type inputType) =>
			ENCODING_TYPE.GetString(Serialize(input, inputType));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TOut Deserialize<TOut>(string input)
		{
			using var memoryStream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize<TOut>(memoryStream);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object Deserialize(string input, Type outputType)
		{
			using var memoryStream = new EncodedMemoryStream(ENCODING_TYPE, input);
			return Deserialize(memoryStream, outputType);
		}

		public string SerializeToPrettyString<TIn>(TIn input) => SerializeToString(input);

		public string SerializeToPrettyString(object input, Type inputType) => SerializeToString(input, inputType);
	}
}