using System;
using System.Text.Json;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Microsoft
{
	public readonly partial struct MicrosoftJsonSerializer : IRawSerializer
	{
		public byte[] Serialize<TIn>(TIn input) =>
			JsonSerializer.SerializeToUtf8Bytes(input, _options);

		public byte[] Serialize(object input, Type inputType) =>
			JsonSerializer.SerializeToUtf8Bytes(input, inputType, _options);

		public TOut Deserialize<TOut>(byte[] input) => throw new NotImplementedException();

		public object Deserialize(byte[] input, Type outputType) => throw new NotImplementedException();
	}
}