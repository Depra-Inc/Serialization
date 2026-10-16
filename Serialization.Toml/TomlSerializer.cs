using System;

namespace Depra.Serialization.Toml
{
	public sealed class TomlSerializer : ITextSerializer
	{
		string ITextSerializer.SerializeToString<TIn>(TIn input) =>
			Tomlyn.TomlSerializer.Serialize(input);

		string ITextSerializer.SerializeToString(object input, Type inputType) =>
			Tomlyn.TomlSerializer.Serialize(input, inputType);

		TOut ITextSerializer.Deserialize<TOut>(string input) =>
			Tomlyn.TomlSerializer.Deserialize<TOut>(input);

		object ITextSerializer.Deserialize(string input, Type outputType) =>
			Tomlyn.TomlSerializer.Deserialize(input, outputType);
	}
}