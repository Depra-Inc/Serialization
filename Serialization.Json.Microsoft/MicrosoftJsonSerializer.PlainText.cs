// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Text.Json;
using Depra.Serialization.Errors;

namespace Depra.Serialization.Json.Microsoft
{
	public readonly partial struct MicrosoftJsonSerializer : ITextSerializer
	{
		public string SerializeToString<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return JsonSerializer.Serialize(input, _options);
		}

		public string SerializeToString(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return JsonSerializer.Serialize(input, inputType, _options);
		}

		public string SerializeToPrettyString<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });
		}

		public string SerializeToPrettyString(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return JsonSerializer.Serialize(input, inputType, new JsonSerializerOptions { WriteIndented = true });
		}

		public TOut Deserialize<TOut>(string input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));

			return JsonSerializer.Deserialize<TOut>(ENCODING_TYPE.GetBytes(input), _options);
		}

		public object Deserialize(string input, Type outputType)
		{
			Guard.AgainstNull(outputType, nameof(outputType));
			Guard.AgainstNullOrEmpty(input, nameof(input));

			return JsonSerializer.Deserialize(ENCODING_TYPE.GetBytes(input), outputType, _options);
		}
	}
}