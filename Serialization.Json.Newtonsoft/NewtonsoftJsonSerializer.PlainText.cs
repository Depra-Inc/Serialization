using System;
using Depra.Serialization.Errors;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : ITextSerializer
	{
		public string SerializeToString<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return JsonConvert.SerializeObject(input, _settings);
		}

		public string SerializeToString(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return JsonConvert.SerializeObject(input, inputType, _settings);
		}

		public string SerializeToPrettyString<TIn>(TIn input)
		{
			Guard.AgainstNull(input, nameof(input));

			return JsonConvert.SerializeObject(input, Formatting.Indented, _settings);
		}

		public string SerializeToPrettyString(object input, Type inputType)
		{
			Guard.AgainstNull(input, nameof(input));
			Guard.AgainstNull(inputType, nameof(inputType));

			return JsonConvert.SerializeObject(input, inputType, Formatting.Indented, _settings);
		}

		public TOut Deserialize<TOut>(string input)
		{
			Guard.AgainstNullOrEmpty(input, nameof(input));

			return JsonConvert.DeserializeObject<TOut>(input, _settings);
		}

		public object Deserialize(string input, Type outputType)
		{
			Guard.AgainstNull(outputType, nameof(outputType));
			Guard.AgainstNullOrEmpty(input, nameof(input));

			return JsonConvert.DeserializeObject(input, outputType, _settings);
		}
	}
}