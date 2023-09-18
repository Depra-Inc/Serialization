using System;
using Depra.Serialization.Interfaces;
using Newtonsoft.Json;

namespace Depra.Serialization.Json.Newtonsoft
{
	public sealed partial class NewtonsoftJsonSerializer : ITextSerializer
	{
		public string SerializeToString<TIn>(TIn input) =>
			JsonConvert.SerializeObject(input, _settings);

		public string SerializeToString(object input, Type inputType) =>
			JsonConvert.SerializeObject(input, inputType, _settings);

		public string SerializeToPrettyString<TIn>(TIn input) =>
			JsonConvert.SerializeObject(input, Formatting.Indented, _settings);

		public string SerializeToPrettyString(object input, Type inputType) =>
			JsonConvert.SerializeObject(input, inputType, Formatting.Indented, _settings);

		public TOut Deserialize<TOut>(string input) =>
			JsonConvert.DeserializeObject<TOut>(input, _settings);

		public object Deserialize(string input, Type outputType) =>
			JsonConvert.DeserializeObject(input, outputType, _settings);
	}
}