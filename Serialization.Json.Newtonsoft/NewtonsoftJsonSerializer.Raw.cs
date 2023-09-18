using System;
using Depra.Serialization.Errors;
using Depra.Serialization.Interfaces;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : IRawSerializer
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

		public TOut Deserialize<TOut>(byte[] input) => throw new NotImplementedException();

		public object Deserialize(byte[] input, Type outputType) => throw new NotImplementedException();
	}
}