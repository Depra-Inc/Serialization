using System;
using System.Text.Json;
using Depra.Serialization.Errors;
using Depra.Serialization.Interfaces;

namespace Depra.Serialization.Json.Microsoft
{
	public readonly partial struct MicrosoftJsonSerializer : ITextSerializer
	{
		public string SerializeToString<TIn>(TIn input) =>
			JsonSerializer.Serialize(input, _options);

		public string SerializeToString(object input, Type inputType) =>
			JsonSerializer.Serialize(input, inputType, _options);

		public string SerializeToPrettyString<TIn>(TIn input) =>
			JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });

		public string SerializeToPrettyString(object input, Type inputType) =>
			JsonSerializer.Serialize(input, inputType, new JsonSerializerOptions { WriteIndented = true });

		public TOut Deserialize<TOut>(string input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			return JsonSerializer.Deserialize<TOut>(ENCODING_TYPE.GetBytes(input), _options);
		}

		public object Deserialize(string input, Type outputType)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));
			return JsonSerializer.Deserialize(ENCODING_TYPE.GetBytes(input), outputType, _options);
		}
	}
}