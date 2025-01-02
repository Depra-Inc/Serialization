// SPDX-License-Identifier: Apache-2.0
// © 2022-2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Serialization.Errors;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : ISerializer
	{
		public byte[] Serialize<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return ENCODING_TYPE.GetBytes(JsonConvert.SerializeObject(input, _settings));
		}

		public byte[] Serialize(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return ENCODING_TYPE.GetBytes(JsonConvert.SerializeObject(input, inputType, _settings));
		}

		public TOut Deserialize<TOut>(byte[] input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));

			var bytes = ENCODING_TYPE.GetString(input);
			return JsonConvert.DeserializeObject<TOut>(bytes, _settings);
		}

		public object Deserialize(byte[] input, Type outputType)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			Guard.AgainstNull(outputType, nameof(outputType));

			var bytes = ENCODING_TYPE.GetString(input);
			return JsonConvert.DeserializeObject(bytes, outputType, _settings);
		}
	}
}