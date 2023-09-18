using System;
using Depra.Serialization.Interfaces;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : IRawSerializer
	{
		public byte[] Serialize<TIn>(TIn input) => ENCODING_TYPE
			.GetBytes(JsonConvert
				.SerializeObject(input, _settings));

		public byte[] Serialize(object input, Type inputType) => ENCODING_TYPE
			.GetBytes(JsonConvert
				.SerializeObject(input, inputType, _settings));

		public TOut Deserialize<TOut>(byte[] input) => throw new NotImplementedException();

		public object Deserialize(byte[] input, Type outputType) => throw new NotImplementedException();
	}
}