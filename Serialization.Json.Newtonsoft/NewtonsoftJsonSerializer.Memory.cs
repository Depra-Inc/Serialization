using System;
using Depra.Serialization.Errors;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : IMemoryDeserializer
	{
		public TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
		{
			Guard.AgainstEmpty(input, nameof(input));

			var inputAsString = ENCODING_TYPE.GetString(input.Span);
			return JsonConvert.DeserializeObject<TOut>(inputAsString, _settings);
		}

		public object Deserialize(Type outputType, ReadOnlyMemory<byte> input)
		{
			Guard.AgainstNull(outputType, nameof(outputType));
			Guard.AgainstEmpty(input, nameof(input));

			var inputAsString = ENCODING_TYPE.GetString(input.Span);
			return JsonConvert.DeserializeObject(inputAsString, outputType, _settings);
		}

		ReadOnlyMemory<byte> IMemoryDeserializer.SerializeMemory<TIn>(TIn input) =>
			Serialize(input);

		ReadOnlyMemory<byte> IMemoryDeserializer.SerializeMemory(object input, Type inputType) =>
			Serialize(input, inputType);
	}
}