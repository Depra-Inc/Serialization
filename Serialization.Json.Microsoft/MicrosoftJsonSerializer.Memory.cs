using System;
using System.Text.Json;
using Depra.Serialization.Errors;

namespace Depra.Serialization.Json.Microsoft
{
	public readonly partial struct MicrosoftJsonSerializer : IMemoryDeserializer
	{
		public TOut Deserialize<TOut>(ReadOnlyMemory<byte> input)
		{
			Guard.AgainstEmpty(input, nameof(input));
			return JsonSerializer.Deserialize<TOut>(input.Span, _options);
		}

		public object Deserialize(Type outputType, ReadOnlyMemory<byte> input)
		{
			Guard.AgainstEmpty(input, nameof(input));
			return JsonSerializer.Deserialize(input.Span, outputType, _options);
		}

		ReadOnlyMemory<byte> IMemoryDeserializer.SerializeMemory<TIn>(TIn input) =>
			Serialize(input);

		ReadOnlyMemory<byte> IMemoryDeserializer.SerializeMemory(object input, Type inputType) =>
			Serialize(input, inputType);
	}
}