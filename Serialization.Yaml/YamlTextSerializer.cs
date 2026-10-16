using System;
using YamlDotNet.Serialization;
using IDeserializer = YamlDotNet.Serialization.IDeserializer;

namespace Depra.Serialization.Yaml
{
	public sealed class YamlTextSerializer : ITextSerializer
	{
		private readonly YamlDotNet.Serialization.ISerializer _serializer = new SerializerBuilder().Build();
		private readonly IDeserializer _deserializer = new DeserializerBuilder().Build();

		string ITextSerializer.SerializeToString<TIn>(TIn input) => _serializer.Serialize(input);

		string ITextSerializer.SerializeToString(object input, Type inputType) => _serializer.Serialize(input, inputType);

		TOut ITextSerializer.Deserialize<TOut>(string input) => _deserializer.Deserialize<TOut>(input);

		object ITextSerializer.Deserialize(string input, Type outputType) => _deserializer.Deserialize(input, outputType);
	}
}