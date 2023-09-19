// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Text.Json;
using Depra.Serialization.Errors;

namespace Depra.Serialization.Json.Microsoft
{
	public readonly partial struct MicrosoftJsonSerializer : IRawSerializer
	{
		public byte[] Serialize<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return JsonSerializer.SerializeToUtf8Bytes(input, _options);
		}

		public byte[] Serialize(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return JsonSerializer.SerializeToUtf8Bytes(input, inputType, _options);
		}

		public TOut Deserialize<TOut>(byte[] input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));

			return JsonSerializer.Deserialize<TOut>(input, _options);
		}

		public object Deserialize(byte[] input, Type outputType)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			Guard.AgainstNull(outputType, nameof(outputType));

			return JsonSerializer.Deserialize(input, outputType, _options);
		}
	}
}